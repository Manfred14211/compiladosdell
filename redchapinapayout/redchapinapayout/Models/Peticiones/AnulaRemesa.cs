using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Peticiones
{
    public class AnulaRemesa
    {
        [Required]
        public string usuarioWebService { get; set; }
        [Required]
        public string claveWebService { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public long? idCooitza { get; set; }
        [Required]
        public string usuarioTransaccion { get; set; }
        [Required]
        public string motivoAnulacion { get; set; }

    }
}