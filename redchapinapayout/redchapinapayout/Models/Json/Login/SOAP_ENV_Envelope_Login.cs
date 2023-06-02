using redchapinapayout.Models.Json.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json
{
    public class SOAP_ENV_Envelope_Login
    {
        public SOAP_ENV_Body_Login SOAP_ENV_Body { get; set; }
        public string xmlns_ns1 { get; set; }
        public string xmlns_SOAP_ENV { get; set; }
    }
}