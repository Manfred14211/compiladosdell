using redchapinapayout.Models.Json.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Libera
{
    public class SOAP_ENV_Envelope_Libera
    {
        public SOAP_ENV_Body_Libera SOAP_ENV_Body { get; set; }
        public string xmlns_ns1 { get; set; }
        public string xmlns_SOAP_ENV { get; set; }
    }
}