using redchapinapayout.Models.Json.Consulta.Consulta_item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos
{
    public class RespuestaAutenticar
    {
        public long? Codigo { get; set; }
        public long? IdCooitza { get; set; }
        public string Descripcion { get; set; }
    }
}