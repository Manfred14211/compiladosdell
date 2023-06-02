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
    
    public partial class TblAsistenciasAsambalea
    {
        public long idTblAsistenciasAsambaleas { get; set; }
        public string nombres { get; set; }
        public string numeroAsociado { get; set; }
        public string numeroDpi { get; set; }
        public string tipoAsociado { get; set; }
        public string estadoTarjetaCredito { get; set; }
        public string estadoCreditos { get; set; }
        public string telefono { get; set; }
        public Nullable<long> formTarjetaCredito { get; set; }
        public Nullable<long> formTarjetaDebito { get; set; }
        public Nullable<long> formAppMovil { get; set; }
        public Nullable<long> formCredito { get; set; }
        public Nullable<long> formZumaPagos { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public string genero { get; set; }
        public Nullable<long> idCatEstados { get; set; }
        public Nullable<long> idTblUsuarioRegistra { get; set; }
        public string valorBuscado { get; set; }
        public Nullable<long> idTblusuarioModifica { get; set; }
        public Nullable<long> idCatEstadosAsistencias { get; set; }
        public Nullable<long> repetido { get; set; }
    
        public virtual CatOpcionesAsamblea CatOpcionesAsamblea { get; set; }
        public virtual CatOpcionesAsamblea CatOpcionesAsamblea1 { get; set; }
        public virtual CatOpcionesAsamblea CatOpcionesAsamblea2 { get; set; }
        public virtual CatOpcionesAsamblea CatOpcionesAsamblea3 { get; set; }
        public virtual CatOpcionesAsamblea CatOpcionesAsamblea4 { get; set; }
        public virtual CatEstado CatEstado { get; set; }
        public virtual TblUsuario TblUsuario { get; set; }
        public virtual TblUsuario TblUsuario1 { get; set; }
    }
}
