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
    
    public partial class CatEstadoSolicitude
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatEstadoSolicitude()
        {
            this.TblSolicitudesMoviemientos = new HashSet<TblSolicitudesMoviemiento>();
        }
    
        public long idCatEstadoSolicitudes { get; set; }
        public string descripcion { get; set; }
        public Nullable<long> idTlUsuariosAlta { get; set; }
        public Nullable<System.DateTime> fechaAlta { get; set; }
        public Nullable<long> idTblUsuarioModifica { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<long> idCatEstados { get; set; }
    
        public virtual TblUsuario TblUsuario { get; set; }
        public virtual CatEstado CatEstado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSolicitudesMoviemiento> TblSolicitudesMoviemientos { get; set; }
    }
}
