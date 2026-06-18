using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.NoDatabase
{
    public class CustomException : Exception
    {
        public int httpCode = 0;
        public int errorCode = 500;
        public string tipoError = "";
        public string mensaje = "";

        public CustomException() : base()
        {

        }

        public CustomException(string message, int httpCode)
        {
            this.httpCode = httpCode;
            this.mensaje = message;
        }

        public CustomException(string message, int httpCode, int erroCode, string tipoError) : base(message)
        {
            this.httpCode = httpCode;
            this.errorCode = erroCode;
            this.mensaje = message;
            this.tipoError = tipoError;
        }
        public CustomException(string message, int httpCode, int erroCode, string tipoError, Exception inner) : base(message, inner)
        {
            this.httpCode = httpCode;
            this.errorCode = erroCode;
            this.mensaje = message;
            this.tipoError = tipoError;
        }
    }
}