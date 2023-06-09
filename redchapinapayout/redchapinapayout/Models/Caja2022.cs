//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace redchapinapayout.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Caja2022
    {
        public long Id { get; set; }
        public long CajaMovimientoId { get; set; }
        public long TipoMovimientoId { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Region { get; set; }
        public string Sucursal { get; set; }
        public string ClaseMovimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public string TipoEntrada { get; set; }
        public string TipoSalida { get; set; }
        public string Serie { get; set; }
        public Nullable<long> Correlativo { get; set; }
        public string Usuario { get; set; }
        public decimal TotalMovimiento { get; set; }
        public string Observacion { get; set; }
        public string TipoPago { get; set; }
        public double Monto { get; set; }
        public string Anulado { get; set; }
        public string DepositoBanco { get; set; }
        public string DepositoCuenta { get; set; }
        public decimal DepositoMonto { get; set; }
        public string DepositoDocumento { get; set; }
        public string DepositoFecha { get; set; }
        public string TarjetaCuota { get; set; }
        public decimal TarjetaRecargo { get; set; }
        public string TarjetaReferencia { get; set; }
        public string ServicioTipo { get; set; }
        public string ServicioCuenta { get; set; }
        public string ChequeBanco { get; set; }
        public string ChequeCuenta { get; set; }
        public string ChequeNo { get; set; }
        public string TransferenciaReferencia { get; set; }
        public Nullable<long> AsociadoId { get; set; }
        public string Asociado { get; set; }
        public string NoAsociado { get; set; }
        public string OtrosDescripcion { get; set; }
        public string TipoCredito { get; set; }
        public string TipoCreditoId { get; set; }
        public string CreditoId { get; set; }
        public string NoCredito { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Otros { get; set; }
        public decimal Mora { get; set; }
        public decimal GastosAdministrativos { get; set; }
        public decimal TotalPagoCredito { get; set; }
        public string TipoTarjeta { get; set; }
        public Nullable<short> AsociadoTarjetaId { get; set; }
        public Nullable<long> NoTarjeta { get; set; }
        public Nullable<short> AsociadoPagoId { get; set; }
        public string AsociadoPago { get; set; }
        public string NoAsociadoPago { get; set; }
        public string TipoServicio { get; set; }
        public string AsociadoServicio { get; set; }
        public string AsociadoPrimerNombre { get; set; }
        public string AsociadoSegundoNombre { get; set; }
        public string AsociadoTercerNombre { get; set; }
        public string AsociadoPrimerApellido { get; set; }
        public string AsociadoSegundoApellido { get; set; }
        public string AsociadoApellidoDeCasada { get; set; }
        public Nullable<long> AsociadoDpi { get; set; }
        public Nullable<System.DateTime> AsociadoFechaNacimiento { get; set; }
        public string Empleado { get; set; }
        public string Cartera { get; set; }
        public string Caja { get; set; }
        public string TipoMovimientoNombreAlterno { get; set; }
        public string AsociadoNacionalidad { get; set; }
        public string AsociadoTipoDocumentoIdentificacion { get; set; }
        public string AsociadoTipo { get; set; }
        public decimal TotalMovimientoDolares { get; set; }
        public string SeguroId { get; set; }
        public string SeguroPoliza { get; set; }
        public string SeguroTipo { get; set; }
        public string SeguroProducto { get; set; }
        public string SeguroModalidad { get; set; }
        public string SeguroFrecuencia { get; set; }
        public decimal SeguroPrimaCuota { get; set; }
        public decimal SeguroCuotasPagadas { get; set; }
        public decimal SeguroTotalPrima { get; set; }
    }
}
