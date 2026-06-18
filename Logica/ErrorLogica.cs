using Logica.Interface;
using Modelos;
using Modelos.NoDatabase;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ErrorLogica : ICRUDLogica<ErrorModel>
    {
        ErrorRepository repo = new ErrorRepository();
        public ErrorModel ActualizarRegistro(ErrorModel input)
        {
            input = repo.update(input);
            return input;
        }

        public ErrorModel CrearRegistro(ErrorModel input)
        {
            input = repo.create(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.delete(id);
            return id;
        }

        public List<ErrorModel> ListarTodo()
        {
            List<ErrorModel> lista = repo.getAll();
            return lista;
        }

        public ErrorModel ObtenerPorId(int id)
        {
            ErrorModel resultado = repo.getById(id);
            return resultado;
        }


        public ErrorResponse Register(CustomException ex, InfoRequest info)
        {
            ErrorResponse RespuestaGeneral = new ErrorResponse();
            ErrorModel oError = new ErrorModel();
            oError = ArmarRequest(ex, info);
            oError = repo.create(oError);
            RespuestaGeneral.code = "Err";
            RespuestaGeneral.mensaje = $"Ocurrio un error en el proceso, consultar el código: {oError.id}";
            RespuestaGeneral.mensajeSistema = ObtenerMensajeInnerException(ex);
            return RespuestaGeneral;
        }

        private ErrorModel ArmarRequest(CustomException ex, InfoRequest info)
        {
            StackTrace trace = new StackTrace(ex, true);
            ErrorModel oError = new ErrorModel();
            try
            {
                oError.class_component = trace.GetFrame(0).GetMethod().ReflectedType.FullName;
                oError.function_name = trace.GetFrame(0).GetMethod().Name;
                oError.line_number = trace.GetFrame(0).GetFileLineNumber();
            }
            catch (Exception)
            {
                oError.class_component = "";
                oError.function_name = "";
                oError.line_number = 0;
            }
            try
            {
                oError.StackTrace = ex.InnerException.StackTrace.ToString();
            }
            catch (Exception) { }
            oError.error = ObtenerMensajeInnerException(ex);
            oError.dateCreate = DateTime.Now;
            oError.id_user = info.Claims.user_id;
            oError.request = "";
            oError.url = info.RequestHttp.absoluteUri;
            oError.ip = info.RequestHttp.ip;
            oError.method = info.RequestHttp.method;
            oError.user_agent = info.RequestHttp.userAgent;
            oError.host = info.RequestHttp.host == null?"": info.RequestHttp.host;
            oError.controller = info.RequestHttp.controller;
            oError.error_code = ex.errorCode;
            oError.request = info.RequestHttp.bodyRequest;

            return oError;
        }

        private string ObtenerMensajeInnerException(Exception ex)
        {
            string err = "";
            if (ex.InnerException != null)
            {
                err = ObtenerMensajeInnerException(ex.InnerException);
            }
            else
            {
                err = ex.Message;
            }
            return err;
        }

    }
}
