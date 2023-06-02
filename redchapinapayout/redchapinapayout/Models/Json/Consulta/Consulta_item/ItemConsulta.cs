using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redchapinapayout.Models.Json.Consulta.Consulta_item
{
    public class ItemConsulta
    {
        public string BEN_PAIS { get; set; }
        public string BEN_PRIMER_APELLIDO { get; set; }
        public string BEN_PRIMER_NOMBRE { get; set; }
        public string BEN_SEGUNDO_APELLIDO { get; set; }
        public string BEN_SEGUNDO_NOMBRE { get; set; }
        public string BEN_TELEFONO { get; set; }
        public string CONSULTA_ONLINE { get; set; }
        public string CORRELATIVO_ID { get; set; }
        public string ESTADO_GIRO { get; set; }
        public string FEE_REM { get; set; }
        public string ID_INTEGRACION { get; set; }
        public string ID_INTERNO { get; set; }
        public string ID_OPERACION { get; set; }
        public string MENSAJE_DOS { get; set; }
        public string MENSAJE_UNO { get; set; }
        public string MONEDA_ORIGEN { get; set; }
        public string MONEDA_PAGO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string REM_CIUDAD { get; set; }
        public string REM_DIRECCION { get; set; }
        public string REM_ESTADO { get; set; }
        public string REM_PAIS { get; set; }
        public string REM_PRIMER_APELLIDO { get; set; }
        public string REM_PRIMER_NOMBRE { get; set; }
        public string REM_SEGUNDO_APELLIDO { get; set; }
        public string REM_SEGUNDO_NOMBRE { get; set; }
        public string REMESADOR { get; set; }
        public string TASA_CAMBIO { get; set; }
        public string VALOR_ENVIADO { get; set; }
        public string VALOR_PAGAR { get; set; }
    }
}