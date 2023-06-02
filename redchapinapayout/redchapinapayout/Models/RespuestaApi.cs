using redchapinapayout.Models.Json.Consulta.Consulta_item;
using redchapinapayout.Models.Json.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models
{
    public class RespuestaApi
    {
        public long? Codigo { get; set; }
        public long? IdCooitza { get; set; }
        public string Descripcion { get; set; }
        public RespuestaConsulta Resutado { get; set; }
    }
}