using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos.Reserva
{
    public class RespuestaRepositorioReserva
    {
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public long? idTblTransaccionesRch { get; set; }
        public string CodigoReserva { get; set; }
    }
}