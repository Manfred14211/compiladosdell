using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Libera
{
    public class RespuestaLiberaApi
    {
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CodigoLibera { get; set; }
        public string XmlRequest { get; set; }
        public string XmlResponse { get; set; }
    }
}