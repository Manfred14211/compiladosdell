using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.TiposRemesas
{
    public class ResponseTiposRemesas
    {
        public long? idTipoRemesa { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
    }
}