using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Consulta.Consulta_item
{
    public class ResultConsulta
    {
        public string CANTIDAD_REGISTROS { get; set; }
        public string CODIGO_MENSAJE { get; set; }
        public string ID_INTEGRACION { get; set; }
        public ListadoConsulta LISTADO { get; set; }
        public string TEXTO_MENSAJE { get; set; }
        public string USUARIO { get; set; }
    }
}