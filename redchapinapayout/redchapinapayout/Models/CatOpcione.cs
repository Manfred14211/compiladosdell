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
    
    public partial class CatOpcione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatOpcione()
        {
            this.TblUsuarios = new HashSet<TblUsuario>();
            this.TblBibliotecas = new HashSet<TblBiblioteca>();
            this.CatDestinatariosCorreos = new HashSet<CatDestinatariosCorreo>();
            this.CatDestinatariosCorreos1 = new HashSet<CatDestinatariosCorreo1>();
            this.TblAgencias = new HashSet<TblAgencia>();
            this.TblMonitorGrabacions = new HashSet<TblMonitorGrabacion>();
        }
    
        public long idCatOpciones { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<long> idCatUsuarios { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<long> idCatUsuarioModifica { get; set; }
        public Nullable<long> idCatEstados { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUsuario> TblUsuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblBiblioteca> TblBibliotecas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosCorreo> CatDestinatariosCorreos { get; set; }
        public virtual CatEstado CatEstado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosCorreo1> CatDestinatariosCorreos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblAgencia> TblAgencias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblMonitorGrabacion> TblMonitorGrabacions { get; set; }
    }
}
