using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.RespuestasMetodos
{
    public class ContenidoRemesa
    {
        public string benPais { get; set; }
        public string benPrimerApellido { get; set; }
        public string benPrimerNombre { get; set; }
        public string benSegundoApellido { get; set; }
        public string benSegundoNombre { get; set; }
        public string benTelefono { get; set; }
        public string correlativoId { get; set; }
        public string estadoRemesa { get; set; }
        public string numeroRemesa { get; set; }
        public string monedaOrigen { get; set; }
        public string monedaPago { get; set; }
        public string observaciones { get; set; }
        public string remCiudad { get; set; }
        public string remDireccion { get; set; }
        public string remEstado { get; set; }
        public string remPais { get; set; }
        public string remPrimerApellido { get; set; }
        public string remPrimerNombre { get; set; }
        public string remSegundoApellido { get; set; }
        public string remSegundoNombre { get; set; }
        public string remesador { get; set; }
        public string tasaCambio { get; set; }
        public string valorEnviado { get; set; }
        public string ValorPagar { get; set; }
    }
}