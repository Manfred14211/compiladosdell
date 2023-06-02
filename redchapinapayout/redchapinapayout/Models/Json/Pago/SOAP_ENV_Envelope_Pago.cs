using redchapinapayout.Models.Json.Libera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Pago
{
    public class SOAP_ENV_Envelope_Pago
    {
        public SOAP_ENV_Body_Pago SOAP_ENV_Body { get; set; }
        public string xmlns_ns1 { get; set; }
        public string xmlns_SOAP_ENV { get; set; }
    }
}