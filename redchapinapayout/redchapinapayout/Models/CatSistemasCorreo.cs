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
    
    public partial class CatSistemasCorreo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatSistemasCorreo()
        {
            this.TblSistemasCorreosDestinatariosCorreos = new HashSet<TblSistemasCorreosDestinatariosCorreo>();
        }
    
        public long idCatSistemasCorreos { get; set; }
        public string descripcion { get; set; }
        public Nullable<long> idCatEstados { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<long> idTblUsuarios { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<long> idTblUsuarioModifica { get; set; }
        public Nullable<long> idCatDepartamentosTrabajo { get; set; }
        public string asunto { get; set; }
        public string cuerpoMensaje { get; set; }
    
        public virtual TblUsuario TblUsuario { get; set; }
        public virtual TblUsuario TblUsuario1 { get; set; }
        public virtual CatDepartamentosTrabajo CatDepartamentosTrabajo { get; set; }
        public virtual CatEstado CatEstado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSistemasCorreosDestinatariosCorreo> TblSistemasCorreosDestinatariosCorreos { get; set; }
    }
}
