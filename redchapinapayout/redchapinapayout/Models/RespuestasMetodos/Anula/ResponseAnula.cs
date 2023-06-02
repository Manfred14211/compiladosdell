using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Anula
{
    public class ResponseAnula
    {
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public string IdAnulacion { get; set; }
        public long? IdCooitza { get; set; }
        public string XmlRequest { get; set; }
        public string XmlResponse { get; set; }
        public string CodigoAnulacion { get; set; }
    }
}