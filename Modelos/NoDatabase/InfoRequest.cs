using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.NoDatabase
{
    public class InfoRequest
    {
        public TokenClaims Claims { get; set; }
        public ApiRequestContext RequestHttp { get; set; }
    }

    public class TokenClaims
    {
        public int user_id { get; set; }
        public string nombre { get; set; }
        public string role { get; set; }
    }
    public class ApiRequestContext
    {
        public string ip { get; set; }
        public string absoluteUri { get; set; }
        public string absolutePath { get; set; }
        public string host { get; set; }
        /// <summary>
        /// get post put delete 
        /// </summary>
        public string method { get; set; }
        public string userAgent { get; set; }
        public string controller { get; set; }
        public string bodyRequest { get; set; }
    }
}
