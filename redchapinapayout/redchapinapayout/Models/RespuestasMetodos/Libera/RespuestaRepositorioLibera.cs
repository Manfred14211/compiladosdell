using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Libera
{
    public class RespuestaRepositorioLibera
    {
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CodigoLiberacion { get; set; }
        public long? idTblTransaccionesRch { get; set; }
    }
}