using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Libera
{
    public class ResultLibera
    {
        public string CODIGO_MENSAJE { get; set; }
        public string FECHA_LIBERACION { get; set; }
        public string HORA_LIBERACION { get; set; }
        public string ID_INTEGRACION { get; set; }
        public string ID_INTERNO { get; set; }
        public string ID_LIBERACION { get; set; }
        public string ID_PAGADOR { get; set; }
        public string TEXTO_MENSAJE { get; set; }
        public object USUARIO { get; set; }
    }
}