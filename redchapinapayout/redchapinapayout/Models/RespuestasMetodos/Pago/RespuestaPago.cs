using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Pago
{
    public class RespuestaPago
    {
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public long? IdCooitza { get; set; }
        public string IdPago { get; set; }
    }
}