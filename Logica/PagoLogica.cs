using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Interface;
using Modelos;
using Repositorio;

namespace Logica
{
    public class PagoLogica : ICRUDLogica<PagoModel>
    {
        PagoRepositorio repo = new PagoRepositorio();
        PaqueteLogica paqueteLogica = new PaqueteLogica();

        public PagoModel ActualizarRegistro(PagoModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public PagoModel CrearRegistro(PagoModel input)
        {
            input = repo.CrearRegistro(input);

            if (input.DetallePago != null && input.DetallePago.Any())
            {
                List<PaqueteModel> lstProductosAfectados = new List<PaqueteModel>();

                foreach (DetallePagoModel item in input.DetallePago)
                {
                    PaqueteModel pt = paqueteLogica.ObtenerPorId(item.ID_Pago);
                }
                paqueteLogica.updateMultipleRegistro(lstProductosAfectados);
            }
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.deleteRegistro(id);
            return id;
        }

        public List<PagoModel> ListarTodo()
        {
            List<PagoModel> lista = repo.ListarTodo();
            return lista;
        }

        public PagoModel ObtenerPorId(int id)
        {
            PagoModel resultado = repo.ObtenerPorId(id);
            return resultado;
        }
    }
}