using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Peticiones
{
    public class ConsultaRemesa
    {
        [Required(ErrorMessage = "El NumeroRemesa es obligatorio")]
        public string numeroRemesa { get; set; }
        [Required(ErrorMessage = "El UsuarioWebService es obligatorio")]
        public string usuarioWebService { get; set; }
        [Required(ErrorMessage = "La ClaveWebService es obligatorio")]
        public string claveWebService { get; set; }
        [Required(ErrorMessage = "El UsuarioTransaccion es obligatorio")]
        public string token { get; set; }
        [Required(ErrorMessage = "El UsuarioTransaccion es obligatorio")]
        public string usuarioTransaccion { get; set; }
        [Required(ErrorMessage = "La SucursalId es obligatorio")]
        public long? sucursalId { get; set; }
    }
}