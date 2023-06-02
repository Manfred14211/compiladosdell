using redchapinapayout.Models.Json.Consulta.Consulta_item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos
{
    public class RespuestaRch
    {
        public long? codigo { get; set; }
        public long? idCooitza { get; set; }
        public string descripcion { get; set; }
        public ContenidoRemesa resutado { get; set; }
    }
}