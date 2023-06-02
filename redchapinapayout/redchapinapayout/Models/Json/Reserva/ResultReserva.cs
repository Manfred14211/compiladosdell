using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Reserva
{
    public class ResultReserva
    {
        public string CODIGO_MENSAJE { get; set; }
        public string FECHA_VENCE_RESERVA { get; set; }
        public string HORA_VENCE_RESERVA { get; set; }
        public string ID_INTEGRACION { get; set; }
        public string ID_INTERNO { get; set; }
        public string ID_RESERVA { get; set; }
        public string TEXTO_MENSAJE { get; set; }
        public string USUARIO { get; set; }
    }
}