using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Peticiones
{
    public class PagoRemesa
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
        public string numeroRemesa { get; set; }
        [Required]
        public string  usuarioTransaccion { get; set; }

        [Required]
        public string benPrimerNombre { get; set; }
        public string benSegundoNombre { get; set; }
        [Required]
        public string benPrimerApellido { get; set; }
        public string benSegundoApellido { get; set; }
        [Required]
        public string benPaisTelefono { get; set; }
        [Required]
        public int benTelefono { get; set; }
        [Required]
        public string benTipoIdentificacion { get; set; }
        [Required]
        public long benNumeroIdentificacion { get; set; }
        [Required]
        public string benPaisIdentificacion { get; set; }
        [Required]
        public string benFechaEmisionIdentificacion { get; set; }
        [Required]
        public string benFechaVencimientoIdentificacion { get; set; }
        [Required]
        public string benGenero { get; set; }
        [Required]
        public string benEstadoCivil { get; set; }
        [Required]
        public string benNacionalidad { get; set; }
        [Required]
        public string benFechaNacimiento { get; set; }
        [Required]
        public string benPaisNacimiento { get; set; }
        [Required]
        public string benEstadoNacimiento { get; set; }
        [Required]
        public string benLugarNacimiento { get; set; }
        [Required]
        public string benDireccion { get; set; }
        [Required]
        public string benCorreoElectronico { get; set; }
        [Required]
        public string benOcupacion { get; set; }
        [Required]
        public string benPaisResidencia { get; set; }
        [Required]
        public string benMotivoRecepcion { get; set; }
        [Required]
        public string benActividadEconomica { get; set; }
        [Required]
        public int benIngresosMensuales { get; set; }
        [Required]
        public int benEgresosMensuales { get; set; }
        [Required]
        public int benCantidadRemesas { get; set; }
        [Required]
        public long? idRelacionRemitente { get; set; }
        [Required]
        public long? idTipoRemesa { get; set; }
        [Required]
        public int? asociado { get; set; }
        public string numeroCuentaCooitza { get; set; }
    }
}