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
    
    public partial class TblSistemasCorreosDestinatariosCorreo
    {
        public long idTblSistemasCorreosDestinatariosCorreos { get; set; }
        public Nullable<long> idCatSistemasCorreos { get; set; }
        public Nullable<long> idCatDestinatariosCorreos { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<long> idTblUsuarios { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<long> idTblUsuarioModifica { get; set; }
        public Nullable<long> idCatEstados { get; set; }
    
        public virtual TblUsuario TblUsuario { get; set; }
        public virtual TblUsuario TblUsuario1 { get; set; }
        public virtual CatDestinatariosCorreo CatDestinatariosCorreo { get; set; }
        public virtual CatSistemasCorreo CatSistemasCorreo { get; set; }
        public virtual CatEstado CatEstado { get; set; }
    }
}
