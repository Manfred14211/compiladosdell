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
    
    public partial class TblUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblUsuario()
        {
            this.TblAsistenciasAsambaleas = new HashSet<TblAsistenciasAsambalea>();
            this.TblAsistenciasAsambaleas1 = new HashSet<TblAsistenciasAsambalea>();
            this.Tbl_U_Roles = new HashSet<Tbl_U_Roles>();
            this.TblBitacoraBusquedaUsuarios = new HashSet<TblBitacoraBusquedaUsuario>();
            this.TblModulos = new HashSet<TblModulo>();
            this.TblModulos1 = new HashSet<TblModulo>();
            this.TblSubModulos = new HashSet<TblSubModulo>();
            this.CatBancos = new HashSet<CatBanco>();
            this.CatDestinatariosCorreos = new HashSet<CatDestinatariosCorreo1>();
            this.CatDestinatariosCorreos1 = new HashSet<CatDestinatariosCorreo1>();
            this.CatDestinatariosCorreos2 = new HashSet<CatDestinatariosCorreo>();
            this.CatDestinatariosCorreos3 = new HashSet<CatDestinatariosCorreo>();
            this.CatDestinatariosSms = new HashSet<CatDestinatariosSm>();
            this.CatDestinatariosSms1 = new HashSet<CatDestinatariosSm>();
            this.CatEstadosLoginRches = new HashSet<CatEstadosLoginRch>();
            this.CatEstadosLoginRches1 = new HashSet<CatEstadosLoginRch>();
            this.CatEstadoSolicitudes = new HashSet<CatEstadoSolicitude>();
            this.CatHorarioServicios = new HashSet<CatHorarioServicio>();
            this.CatHorarioServicios1 = new HashSet<CatHorarioServicio>();
            this.CatImagenesIntranets = new HashSet<CatImagenesIntranet>();
            this.CatProveedoresCooitzas = new HashSet<CatProveedoresCooitza>();
            this.CatProveedoresExternos = new HashSet<CatProveedoresExterno>();
            this.CatProveedoresExternos1 = new HashSet<CatProveedoresExterno>();
            this.CatPuestos = new HashSet<CatPuesto>();
            this.CatPuestos1 = new HashSet<CatPuesto>();
            this.CatServiciosMonitors = new HashSet<CatServiciosMonitor>();
            this.CatServiciosMonitors1 = new HashSet<CatServiciosMonitor>();
            this.CatSistemasCorreos = new HashSet<CatSistemasCorreo>();
            this.CatSistemasCorreos1 = new HashSet<CatSistemasCorreo>();
            this.CatTipificacionProductos = new HashSet<CatTipificacionProducto>();
            this.CatTipificacionProductos1 = new HashSet<CatTipificacionProducto>();
            this.CatTiposCuentas = new HashSet<CatTiposCuenta>();
            this.CatTiposCuentas1 = new HashSet<CatTiposCuenta>();
            this.CatTiposCuentas2 = new HashSet<CatTiposCuenta>();
            this.CatTiposEmpresasProveedores = new HashSet<CatTiposEmpresasProveedore>();
            this.CatTiposEmpresasProveedores1 = new HashSet<CatTiposEmpresasProveedore>();
            this.CatTiposKilometraje1 = new HashSet<CatTiposKilometraje1>();
            this.CatTiposKilometraje11 = new HashSet<CatTiposKilometraje1>();
            this.CatTiposMonedas = new HashSet<CatTiposMoneda>();
            this.CatTiposMovimientos = new HashSet<CatTiposMovimiento>();
            this.CatTiposProveedores = new HashSet<CatTiposProveedore>();
            this.CatTiposProveedores1 = new HashSet<CatTiposProveedore>();
            this.constanciasLaborales = new HashSet<constanciasLaborale>();
            this.TblAsignaciones = new HashSet<TblAsignacione>();
            this.TblAsignacionesEjecutivosSucursales = new HashSet<TblAsignacionesEjecutivosSucursale>();
            this.tblbitacorapremoras = new HashSet<tblbitacorapremora>();
            this.TblBitacoraServices = new HashSet<TblBitacoraService>();
            this.TblCarteraCreditoes = new HashSet<TblCarteraCredito>();
            this.TblCertificacionesTecnicos = new HashSet<TblCertificacionesTecnico>();
            this.TblCertificacionesTecnicos1 = new HashSet<TblCertificacionesTecnico>();
            this.TblCierresProductos = new HashSet<TblCierresProducto>();
            this.TblCierresProductos1 = new HashSet<TblCierresProducto>();
            this.TblConfiguracionesRches = new HashSet<TblConfiguracionesRch>();
            this.TblConfiguracionesRches1 = new HashSet<TblConfiguracionesRch>();
            this.TblDeclaracionSaluds = new HashSet<TblDeclaracionSalud>();
            this.TblDeclaracionSaludchnlgs = new HashSet<TblDeclaracionSaludchnlg>();
            this.TblDirectorios = new HashSet<TblDirectorio>();
            this.TblDirectorios1 = new HashSet<TblDirectorio>();
            this.TblEmpleadosMetas = new HashSet<TblEmpleadosMeta>();
            this.TblEmpleadosMetas1 = new HashSet<TblEmpleadosMeta>();
            this.TblEmpleadosMetas2 = new HashSet<TblEmpleadosMeta>();
            this.TblEntregas = new HashSet<TblEntrega>();
            this.TblEstado_Solicitudes = new HashSet<TblEstado_Solicitudes>();
            this.TblEstado_Solicitudes1 = new HashSet<TblEstado_Solicitudes>();
            this.TblGirasTecnicos = new HashSet<TblGirasTecnico>();
            this.TblGirasTecnicos1 = new HashSet<TblGirasTecnico>();
            this.TblGirasTecnicos2 = new HashSet<TblGirasTecnico>();
            this.TblGirasTecnicos3 = new HashSet<TblGirasTecnico>();
            this.TblGruposAgencias = new HashSet<TblGruposAgencia>();
            this.TblGruposAgencias1 = new HashSet<TblGruposAgencia>();
            this.TblHistoriales1 = new HashSet<TblHistoriales1>();
            this.TblMigracions = new HashSet<TblMigracion>();
            this.TblMigracions1 = new HashSet<TblMigracion>();
            this.TblMonitorGrabacions = new HashSet<TblMonitorGrabacion>();
            this.tblpremoras = new HashSet<tblpremora>();
            this.tblpremoras1 = new HashSet<tblpremora>();
            this.TblProductosCooitzas = new HashSet<TblProductosCooitza>();
            this.TblProductosCooitzas1 = new HashSet<TblProductosCooitza>();
            this.TblSistemasCorreosDestinatariosCorreos = new HashSet<TblSistemasCorreosDestinatariosCorreo>();
            this.TblSistemasCorreosDestinatariosCorreos1 = new HashSet<TblSistemasCorreosDestinatariosCorreo>();
            this.TblSolicitudes = new HashSet<TblSolicitude>();
            this.TblSolicitudes1 = new HashSet<TblSolicitude>();
            this.TblSolicitudesCooitzas = new HashSet<TblSolicitudesCooitza>();
            this.TblSolicitudesMoviemientos = new HashSet<TblSolicitudesMoviemiento>();
            this.TblSolicitudesMovimientosDetalles = new HashSet<TblSolicitudesMovimientosDetalle>();
            this.TblSucrusalesProductosCooitzas = new HashSet<TblSucrusalesProductosCooitza>();
            this.TblSucrusalesProductosCooitzas1 = new HashSet<TblSucrusalesProductosCooitza>();
            this.TblUsuarios_Coordinador = new HashSet<TblUsuarios_Coordinador>();
            this.TblUsuarios_Coordinador1 = new HashSet<TblUsuarios_Coordinador>();
            this.TblUsuarios_Coordinador2 = new HashSet<TblUsuarios_Coordinador>();
            this.TblUsuarios_Coordinador3 = new HashSet<TblUsuarios_Coordinador>();
        }
    
        public long idTblUsuarios { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public Nullable<long> idCatEstados { get; set; }
        public Nullable<System.TimeSpan> horaInicio { get; set; }
        public Nullable<System.TimeSpan> horaFinal { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<long> idTblPersonas { get; set; }
        public Nullable<long> idCatOpciones { get; set; }
        public Nullable<long> idCatDepartamentosTrabajo { get; set; }
        public Nullable<long> idCodigoJefe { get; set; }
        public Nullable<long> idCodigoGerenete { get; set; }
        public Nullable<long> idCatUbicaciones { get; set; }
        public Nullable<long> IdCore { get; set; }
        public Nullable<long> IdEmpleadoCore { get; set; }
        public string imagenCore { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblAsistenciasAsambalea> TblAsistenciasAsambaleas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblAsistenciasAsambalea> TblAsistenciasAsambaleas1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_U_Roles> Tbl_U_Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblBitacoraBusquedaUsuario> TblBitacoraBusquedaUsuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblModulo> TblModulos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblModulo> TblModulos1 { get; set; }
        public virtual TblPersona TblPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSubModulo> TblSubModulos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatBanco> CatBancos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosCorreo1> CatDestinatariosCorreos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosCorreo1> CatDestinatariosCorreos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosCorreo> CatDestinatariosCorreos2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosCorreo> CatDestinatariosCorreos3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosSm> CatDestinatariosSms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatDestinatariosSm> CatDestinatariosSms1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatEstadosLoginRch> CatEstadosLoginRches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatEstadosLoginRch> CatEstadosLoginRches1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatEstadoSolicitude> CatEstadoSolicitudes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatHorarioServicio> CatHorarioServicios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatHorarioServicio> CatHorarioServicios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatImagenesIntranet> CatImagenesIntranets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatProveedoresCooitza> CatProveedoresCooitzas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatProveedoresExterno> CatProveedoresExternos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatProveedoresExterno> CatProveedoresExternos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatPuesto> CatPuestos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatPuesto> CatPuestos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatServiciosMonitor> CatServiciosMonitors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatServiciosMonitor> CatServiciosMonitors1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatSistemasCorreo> CatSistemasCorreos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatSistemasCorreo> CatSistemasCorreos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTipificacionProducto> CatTipificacionProductos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTipificacionProducto> CatTipificacionProductos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposCuenta> CatTiposCuentas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposCuenta> CatTiposCuentas1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposCuenta> CatTiposCuentas2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposEmpresasProveedore> CatTiposEmpresasProveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposEmpresasProveedore> CatTiposEmpresasProveedores1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposKilometraje1> CatTiposKilometraje1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposKilometraje1> CatTiposKilometraje11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposMoneda> CatTiposMonedas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposMovimiento> CatTiposMovimientos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposProveedore> CatTiposProveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatTiposProveedore> CatTiposProveedores1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<constanciasLaborale> constanciasLaborales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblAsignacione> TblAsignaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblAsignacionesEjecutivosSucursale> TblAsignacionesEjecutivosSucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbitacorapremora> tblbitacorapremoras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblBitacoraService> TblBitacoraServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCarteraCredito> TblCarteraCreditoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCertificacionesTecnico> TblCertificacionesTecnicos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCertificacionesTecnico> TblCertificacionesTecnicos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCierresProducto> TblCierresProductos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblCierresProducto> TblCierresProductos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblConfiguracionesRch> TblConfiguracionesRches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblConfiguracionesRch> TblConfiguracionesRches1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblDeclaracionSalud> TblDeclaracionSaluds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblDeclaracionSaludchnlg> TblDeclaracionSaludchnlgs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblDirectorio> TblDirectorios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblDirectorio> TblDirectorios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEmpleadosMeta> TblEmpleadosMetas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEmpleadosMeta> TblEmpleadosMetas1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEmpleadosMeta> TblEmpleadosMetas2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEntrega> TblEntregas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEstado_Solicitudes> TblEstado_Solicitudes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblEstado_Solicitudes> TblEstado_Solicitudes1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblGirasTecnico> TblGirasTecnicos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblGirasTecnico> TblGirasTecnicos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblGirasTecnico> TblGirasTecnicos2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblGirasTecnico> TblGirasTecnicos3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblGruposAgencia> TblGruposAgencias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblGruposAgencia> TblGruposAgencias1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblHistoriales1> TblHistoriales1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblMigracion> TblMigracions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblMigracion> TblMigracions1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblMonitorGrabacion> TblMonitorGrabacions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpremora> tblpremoras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpremora> tblpremoras1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProductosCooitza> TblProductosCooitzas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProductosCooitza> TblProductosCooitzas1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSistemasCorreosDestinatariosCorreo> TblSistemasCorreosDestinatariosCorreos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSistemasCorreosDestinatariosCorreo> TblSistemasCorreosDestinatariosCorreos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSolicitude> TblSolicitudes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSolicitude> TblSolicitudes1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSolicitudesCooitza> TblSolicitudesCooitzas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSolicitudesMoviemiento> TblSolicitudesMoviemientos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSolicitudesMovimientosDetalle> TblSolicitudesMovimientosDetalles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSucrusalesProductosCooitza> TblSucrusalesProductosCooitzas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblSucrusalesProductosCooitza> TblSucrusalesProductosCooitzas1 { get; set; }
        public virtual CatDepartamentosTrabajo CatDepartamentosTrabajo { get; set; }
        public virtual CatEstado CatEstado { get; set; }
        public virtual CatOpcione CatOpcione { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUsuarios_Coordinador> TblUsuarios_Coordinador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUsuarios_Coordinador> TblUsuarios_Coordinador1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUsuarios_Coordinador> TblUsuarios_Coordinador2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUsuarios_Coordinador> TblUsuarios_Coordinador3 { get; set; }
    }
}
