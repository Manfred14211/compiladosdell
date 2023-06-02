using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Pago
{
    public class RespuestaExecutePagoApi
    {
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CodigoPago { get; set; }
        public string XmlRequest { get; set; }
        public string XmlResponse { get; set; }
    }
}