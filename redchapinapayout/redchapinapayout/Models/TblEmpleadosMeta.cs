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
    
    public partial class TblEmpleadosMeta
    {
        public long idTblMetasEmpleados { get; set; }
        public Nullable<long> idCore { get; set; }
        public Nullable<long> idEmpleadoCore { get; set; }
        public Nullable<long> idSucursalCore { get; set; }
        public Nullable<long> idCatAnios { get; set; }
        public Nullable<long> idCatMeses { get; set; }
        public Nullable<long> idCatEstados { get; set; }
        public Nullable<long> idCatEstadosCore { get; set; }
        public Nullable<long> idTblProductosCooitza { get; set; }
        public Nullable<decimal> metaAnual { get; set; }
        public Nullable<long> cantidadAnual { get; set; }
        public string idCorePuesto { get; set; }
        public string correoElectronico { get; set; }
        public string numeroTelefono { get; set; }
        public Nullable<System.DateTime> fechaCreaccion { get; set; }
        public Nullable<long> idTblUsuariosAlta { get; set; }
        public Nullable<long> idTblUsuarioModifica { get; set; }
        public Nullable<long> idTblUsuarios { get; set; }
        public Nullable<decimal> metaMensual { get; set; }
        public Nullable<long> cantidadMensual { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<long> idTblSucursalesProductosCooitza { get; set; }
        public Nullable<decimal> montoCierreAnio { get; set; }
        public Nullable<long> cantidadCierreAnio { get; set; }
    
        public virtual TblUsuario TblUsuario { get; set; }
        public virtual TblUsuario TblUsuario1 { get; set; }
        public virtual TblUsuario TblUsuario2 { get; set; }
        public virtual CatAnio CatAnio { get; set; }
        public virtual CatMes CatMes { get; set; }
        public virtual CatEstado CatEstado { get; set; }
        public virtual TblProductosCooitza TblProductosCooitza { get; set; }
        public virtual TblSucrusalesProductosCooitza TblSucrusalesProductosCooitza { get; set; }
    }
}
