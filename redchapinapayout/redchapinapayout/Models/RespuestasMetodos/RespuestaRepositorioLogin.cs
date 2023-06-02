using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos
{
    public class RespuestaRepositorioLogin
    {
        public long? CodigoBd { get; set; }
        public string DescripcionBd { get; set; }
        public long? idCatEstadosLogin { get; set; }
        public string descripcionLogin { get; set; }
        public long? idTblTransacionesRch { get; set; }
    }
}