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
    
    public partial class CatOpcionesVehiculo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatOpcionesVehiculo()
        {
            this.TblVehiculos = new HashSet<TblVehiculo>();
        }
    
        public long idCatOpcionesVehiculos { get; set; }
        public string descripcion { get; set; }
        public Nullable<long> idTblUsuarios { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<long> idCatUsuarioModifica { get; set; }
        public Nullable<System.DateTime> fechaActualizacion { get; set; }
        public Nullable<long> idCatEstados { get; set; }
    
        public virtual CatEstado CatEstado { get; set; }
        public virtual TblUsuarios1 TblUsuarios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblVehiculo> TblVehiculos { get; set; }
    }
}
