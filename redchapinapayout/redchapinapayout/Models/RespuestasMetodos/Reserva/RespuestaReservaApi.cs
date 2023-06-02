using redchapinapayout.Models.Json.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Reserva
{
    public class RespuestaReservaApi
    {
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CodigoReserva { get; set; }
        public string XmlRequest { get; set; }
        public string XmlResponse { get; set; }

    }
}