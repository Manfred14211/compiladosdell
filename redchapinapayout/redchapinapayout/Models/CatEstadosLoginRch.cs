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
    
    public partial class CatEstadosLoginRch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatEstadosLoginRch()
        {
            this.TblLoginRches = new HashSet<TblLoginRch>();
        }
    
        public long idCatEstadosLoginRch { get; set; }
        public string descripcion { get; set; }
        public Nullable<long> idCatEstados { get; set; }
        public Nullable<System.DateTime> fechaHoraAlta { get; set; }
        public Nullable<long> idTblUsuarioAlta { get; set; }
        public Nullable<System.DateTime> fechaHoraModificacion { get; set; }
        public Nullable<long> idTblUsuarioModifica { get; set; }
    
        public virtual TblUsuario TblUsuario { get; set; }
        public virtual TblUsuario TblUsuario1 { get; set; }
        public virtual CatEstado CatEstado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblLoginRch> TblLoginRches { get; set; }
    }
}
