using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Consulta.Consulta_item
{
    public class SOAP_ENV_BODY_CONSULTA
    {
        public Ns1_ConsultaResponse ns1_ConsultaGiroResponse { get; set; }
        public string xmlns_ns1 { get; set; }
        public string xmlns_SOAP_ENV { get; set; }
    }
}