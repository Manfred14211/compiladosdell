using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Pago
{
    public class ResultPago
    {
        public string ID_INTEGRACION { get; set; }
        public string USUARIO { get; set; }
        public string ID_INTERNO { get; set; }
        public string ID_PAGO { get; set; }
        public string FECHA_PAGO { get; set; }
        public string HORA_PAGO { get; set; }
        public string CODIGO_MENSAJE { get; set; }
        public string TEXTO_MENSAJE { get; set; }
        public string RESERVA_PAGO1 { get; set; }
        public string RESERVA_PAGO2 { get; set; }
        public string RESERVA_PAGO3 { get; set; }
        public string RESERVA_PAGO4 { get; set; }
    }
}