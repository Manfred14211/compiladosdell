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
    
    public partial class TblDirectoriosAgencia
    {
        public long idTblDirectoriosAgencias { get; set; }
        public string nombreAgencia { get; set; }
        public string gerente { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string tefonoCooperativa { get; set; }
        public string numeroExtension { get; set; }
        public string correoInstitucional { get; set; }
        public Nullable<System.DateTime> fechadeActualizacion { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<long> idTblUsuarios { get; set; }
        public Nullable<long> idTblUsuarioModifica { get; set; }
        public Nullable<long> idCatRegiones { get; set; }
        public Nullable<long> idCatEstados { get; set; }
        public string codigo { get; set; }
    
        public virtual CatEstado CatEstado { get; set; }
        public virtual CatRegione CatRegione { get; set; }
    }
}
