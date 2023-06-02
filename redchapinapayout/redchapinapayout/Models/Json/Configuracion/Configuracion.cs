using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Configuracion
{
    public class Configuracion
    {
        public bool Codigo { get; set; }
        public string PagadorId { get; set; }
        public string IntegracionId { get; set; }
        public string VerificacionTercero { get; set; }
        public string VerificacionAgente { get; set; }
        public string TipoRemesa { get; set; }
    }
}