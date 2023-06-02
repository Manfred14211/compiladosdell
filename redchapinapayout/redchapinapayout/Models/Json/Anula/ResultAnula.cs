using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Anula
{
    public class ResultAnula
    {
        public string ID_INTEGRACION { get; set; }
        public string USUARIO { get; set; }
        public string ID_INTERNO { get; set; }
        public string ID_ANULACION { get; set; }
        public string FECHA_ANULACION { get; set; }
        public string HORA_ANULACION { get; set; }
        public string CODIGO_MENSAJE { get; set; }
        public string TEXTO_MENSAJE { get; set; }
    }
}