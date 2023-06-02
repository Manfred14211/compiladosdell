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
    
    public partial class TblFactura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblFactura()
        {
            this.TblFacturasXcatRubros = new HashSet<TblFacturasXcatRubro>();
        }
    
        public long idTblFacturas { get; set; }
        public Nullable<long> idTblSolicitudes { get; set; }
        public Nullable<System.DateTime> fechaFactura { get; set; }
        public string noFactura { get; set; }
        public Nullable<long> idTblProveedores { get; set; }
        public Nullable<System.DateTime> fechaVencimiento { get; set; }
        public Nullable<decimal> monto { get; set; }
        public string imagenFactura { get; set; }
        public Nullable<long> idTblUsuarios { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<long> idCatUsuarioModifica { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<long> idCatEstados { get; set; }
        public string serie { get; set; }
        public string razon { get; set; }
    
        public virtual CatEstado CatEstado { get; set; }
        public virtual TblProveedore TblProveedore { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblFacturasXcatRubro> TblFacturasXcatRubros { get; set; }
    }
}
