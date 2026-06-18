using Logica;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modelos.NoDatabase;
using System.Net;

namespace PersonalApi.Middleware
{
    public class ErrorMidleware
    {
        private readonly RequestDelegate next;

        public ErrorMidleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                await next(context);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (SqlException ex)
            {
                CustomException exx = new CustomException("Error no controlado", (int)HttpStatusCode.InternalServerError, ex.Number, "Data base", ex);
                await HandleExceptionAsync(context, exx);
            }
            catch (DbUpdateException ex)
            {
                var err = ex.GetBaseException() as SqlException;
                //ex.InnerException.
                CustomException exx = new CustomException("Error no controlado", (int)HttpStatusCode.InternalServerError, err.Number, "Data base", ex);
                await HandleExceptionAsync(context, exx);
            }
            catch (Exception ex)
            {
                CustomException exx = new CustomException("Error no controlado", (int)HttpStatusCode.InternalServerError, 500, "", ex);
                await HandleExceptionAsync(context, exx);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, CustomException ex)
        {
            var controllerActionDescriptor = context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>();
            var controllerName = controllerActionDescriptor.ControllerName;
            var actionName = controllerActionDescriptor.ActionName;


            InfoRequest info = new InfoRequest();
            info = HelperHttpContext.GetInfoRequest(context);
            ErrorResponse error = new ErrorResponse();
            ErrorLogica errorDominio = new ErrorLogica();
            error = errorDominio.Register(ex, info);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.httpCode;

            //return context.Response.WriteAsync(JsonConvert.SerializeObject(error));

            return context.Response.WriteAsJsonAsync(error);

        }

    }
}
