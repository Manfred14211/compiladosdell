using Newtonsoft.Json;
using redchapinapayout.Models;
using redchapinapayout.Models.Json.Anula;
using redchapinapayout.Models.Json.Configuracion;
using redchapinapayout.Models.Json.Consulta.Consulta_item;
using redchapinapayout.Models.Json.Libera;
using redchapinapayout.Models.Json.Pago;
using redchapinapayout.Models.Json.Reserva;
using redchapinapayout.Models.RespuestasMetodos;
using redchapinapayout.Models.RespuestasMetodos.Anula;
using redchapinapayout.Models.RespuestasMetodos.Libera;
using redchapinapayout.Models.RespuestasMetodos.Pago;
using redchapinapayout.Models.RespuestasMetodos.Reserva;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace redchapinapayout.Repositorios
{
    public class Servicio : IServicio
    {
        private cooitzacoreEntities db = new cooitzacoreEntities();
      
        public async Task<bool> ExisteSucursal(long? idSucursal)
        {
            RespuestaRepositorioLogin oResponseApi = new RespuestaRepositorioLogin();
            var oSucursal = await db.CatUbicaciones.FirstOrDefaultAsync(x => x.idSucursal == idSucursal);

            if (oSucursal == null)
            {

                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<RespuestaRepositorioLogin> SetBitacoraLogin(string xmlRequest, string xmlResponse, RespuestaRepositorioLogin jsonResponse)
        {

            RespuestaRepositorioLogin oResponseApi = new RespuestaRepositorioLogin();
            try
            {
                TblTransaccionesRch oTblTransacciones = new TblTransaccionesRch();
                oTblTransacciones.loginRequestXml = xmlRequest;
                oTblTransacciones.loginResponseXml = xmlResponse;
                /* - - - - - - - - - - serealiza el objeto json response - - - - - - - */
                var _oRespuesta = new StringContent(JsonConvert.SerializeObject(jsonResponse), Encoding.UTF8, "application/json");
                string _jsonRespuestaStr = await _oRespuesta.ReadAsStringAsync();
                /* - - - - - - - - - - fin de la serealizacion - - - - - - - - - - - - */
                oTblTransacciones.loginResponseJson = _jsonRespuestaStr;//<--- json 
                oTblTransacciones.idCatEstadosLoginRch = jsonResponse.idCatEstadosLogin;
                oTblTransacciones.idCatEstadosTransacciones = 1;
                db.TblTransaccionesRches.Add(oTblTransacciones);
                db.SaveChanges();
                oResponseApi.CodigoBd = 000;
                oResponseApi.DescripcionBd = "Registro guardado con exito";

                oResponseApi.idCatEstadosLogin = jsonResponse.idCatEstadosLogin;
                oResponseApi.descripcionLogin = jsonResponse.descripcionLogin;
                oResponseApi.idTblTransacionesRch = oTblTransacciones.idTblTransaccionesRch;
            }
            catch (Exception ex)
            {
                oResponseApi.CodigoBd = 100;
                oResponseApi.DescripcionBd = "Ocurrio un problema al guardar el registro cws detalle: " + ex.Message;

            }
            return oResponseApi;
        }
        public RespuestaApi SetBitacoraBusqueda(TblTransaccionesRch tblBusquedasRch, long? idTblTransacciones)
        {

            RespuestaApi oResponseApi = new RespuestaApi();

            var _tblBitacora = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransacciones);

            try
            {
                _tblBitacora.fechaBusqueda = DateTime.Now;
                _tblBitacora.jsonRequest = tblBusquedasRch.jsonRequest;
                _tblBitacora.xmlRequest = tblBusquedasRch.xmlRequest;
                _tblBitacora.usuarioTransaccion = tblBusquedasRch.usuarioTransaccion;
                _tblBitacora.usuarioWebService = tblBusquedasRch.usuarioWebService;
                _tblBitacora.idCatEstados = 1;
                _tblBitacora.idSucursal = tblBusquedasRch.idSucursal;
                _tblBitacora.idCatEstadosBusquedasRch = 1;
                _tblBitacora.idCatEstadosBusquedasRch = 13;//<--- Id 13 indica transaccion procesada
                db.Entry(_tblBitacora).State = EntityState.Modified;
                db.SaveChangesAsync();

                //oResponseApi.Codigo = 10;
                //db.TblTransaccionesRchTests.Add(tblBusquedasRch);
                //db.SaveChanges();
                oResponseApi.Codigo = 000;
                oResponseApi.IdCooitza = _tblBitacora.idTblTransaccionesRch;
                oResponseApi.Descripcion = "Registro guardado con exito";
                //oResponseApi.IdCooitza = tblBusquedasRch.idTblTransaccionesRchTest;
            }
            catch (Exception ex)
            {
                oResponseApi.Codigo = 100;
                oResponseApi.IdCooitza = 0;
                oResponseApi.Descripcion = "Ocurrio un problema al guardar el registro cws detalle: " + ex.Message;

            }
            return oResponseApi;
        }
        /*este metodo no se usa*/
        //public RespuestaApi UpdateBitacoraBusqueda(TblTransaccionesRchTest tblBusquedasRch)
        //{

        //    RespuestaApi oResponseApi = new RespuestaApi();

        //    try
        //    {
        //        //oResponseApi.Codigo = 10;
        //        db.Entry(tblBusquedasRch).State = EntityState.Modified;
        //        db.SaveChangesAsync();
        //        oResponseApi.Codigo = 000;
        //        oResponseApi.IdCooitza = tblBusquedasRch.idTblTransaccionesRchTest;
        //        oResponseApi.Descripcion = "Registro actualizado con exito";

        //    }
        //    catch (Exception ex)
        //    {
        //        oResponseApi.Codigo = 100;
        //        oResponseApi.IdCooitza = 0;
        //        oResponseApi.Descripcion = "Ocurrio un problema al actualizar el registro interno cws detalle: " + ex.Message;

        //    }
        //    return oResponseApi;
        //}

        public async Task<RespuestaApi> UpdateBitacoraBusquedaError(RespuestaApi responseApi, long? id, string xml, long? idCatEstadosBusqueda)
        {
            var oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == id);
            var jsonRespuesta = new StringContent(JsonConvert.SerializeObject(responseApi), Encoding.UTF8, "application/json");
            string jsonResponsetStr = await jsonRespuesta.ReadAsStringAsync();
            RespuestaApi oResponseApi = new RespuestaApi();

            try
            {
                //oResponseApi.Codigo = 10;
                oTransaccion.jsonResponse = jsonResponsetStr;
                oTransaccion.xmlResponse = xml;
                oTransaccion.idCatEstadosBusquedasRch = idCatEstadosBusqueda;
                db.Entry(oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                oResponseApi.Codigo = responseApi.Codigo;
                oResponseApi.Descripcion = responseApi.Descripcion;
                oResponseApi.IdCooitza = oTransaccion.idTblTransaccionesRch;
            }
            catch (Exception ex)
            {
                oResponseApi.Codigo = 100;
                oResponseApi.IdCooitza = 0;
                oResponseApi.Descripcion = "Ocurrio un problema al actualizar el registro interno cws detalle: " + ex.Message;

            }
            return oResponseApi;
        }
        public async Task<RespuestaRch> UpdateBitacoraBusquedaOk(RespuestaApi responseApi, long? id, string xml, long? idCatEstadosBusquedas)
        {
            var oTblBusqueda = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == id);
            /*Objeto que recibe solo el listad-item*/
            ItemConsulta oItemObj = new ItemConsulta();
            oItemObj = responseApi.Resutado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item;
            /* - - - - - - - - - - - - - - - - - - - - */
            oTblBusqueda.remPrimerNombre = oItemObj.REM_PRIMER_NOMBRE;
            oTblBusqueda.remSegundoNombre = oItemObj.REM_SEGUNDO_NOMBRE;
            oTblBusqueda.remPrimerApellido = oItemObj.REM_PRIMER_APELLIDO;
            oTblBusqueda.remSegundoApellido = oItemObj.REM_SEGUNDO_APELLIDO;
            oTblBusqueda.remPais = oItemObj.REM_PAIS;
            oTblBusqueda.remEstado = oItemObj.REM_ESTADO;
            oTblBusqueda.remCiudad = oItemObj.REM_CIUDAD;
            oTblBusqueda.remDireccion = oItemObj.REM_DIRECCION;
            oTblBusqueda.benPrimerNombre = oItemObj.BEN_PRIMER_NOMBRE;
            oTblBusqueda.benSegundoNombre = oItemObj.BEN_SEGUNDO_NOMBRE;
            oTblBusqueda.benPrimerApellido = oItemObj.BEN_PRIMER_APELLIDO;
            oTblBusqueda.benSegundoApellido = oItemObj.BEN_SEGUNDO_APELLIDO;

            oTblBusqueda.fechaBusqueda = DateTime.Now;
            oTblBusqueda.remesador = oItemObj.REMESADOR;
            oTblBusqueda.benTelefono = oItemObj.BEN_TELEFONO;
            oTblBusqueda.benPais = oItemObj.BEN_PAIS;
            oTblBusqueda.monedaOrigen = oItemObj.MONEDA_ORIGEN;
            oTblBusqueda.monedaPago = oItemObj.MONEDA_PAGO;
            oTblBusqueda.valorEnviado = oItemObj.VALOR_ENVIADO;
            oTblBusqueda.valorPagar = oItemObj.VALOR_PAGAR;
            oTblBusqueda.idOperacion = oItemObj.ID_OPERACION;
            oTblBusqueda.idInterno = oItemObj.ID_INTERNO;
            oTblBusqueda.estadoGiro = oItemObj.ESTADO_GIRO;
            oTblBusqueda.tasaCambio = oItemObj.TASA_CAMBIO;

            /*json original de red chapina*/
            var jsonResultado = new StringContent(JsonConvert.SerializeObject(responseApi), Encoding.UTF8, "application/json");
            string jsonResultadotStr = await jsonResultado.ReadAsStringAsync();

            RespuestaRch oResponseApi = new RespuestaRch();
            try
            {
                /*se construye el json que se va retornar al usuario*/
                ContenidoRemesa contenido = new ContenidoRemesa();
                contenido.benPais = oTblBusqueda.benPais;
                contenido.benPrimerApellido = oTblBusqueda.benPrimerApellido;
                contenido.benPrimerNombre = oTblBusqueda.benPrimerNombre;
                contenido.benSegundoApellido = oTblBusqueda.benSegundoApellido;
                contenido.benSegundoNombre = oTblBusqueda.benSegundoNombre;
                contenido.benTelefono = oTblBusqueda.benTelefono;
                contenido.estadoRemesa = oTblBusqueda.estadoGiro;

                contenido.numeroRemesa = oTblBusqueda.idOperacion;
                contenido.monedaOrigen = oTblBusqueda.monedaOrigen;
                contenido.monedaPago = oTblBusqueda.monedaPago;
                contenido.observaciones = oTblBusqueda.observaciones;
                contenido.remCiudad = oTblBusqueda.remCiudad;
                contenido.remDireccion = oTblBusqueda.remDireccion;
                contenido.remEstado = oTblBusqueda.remEstado;
                contenido.remPais = oTblBusqueda.remPais;
                contenido.remPrimerApellido = oTblBusqueda.remPrimerApellido;
                contenido.remPrimerNombre = oTblBusqueda.remPrimerNombre;
                contenido.remSegundoApellido = oTblBusqueda.remSegundoApellido;
                contenido.remSegundoNombre = oTblBusqueda.remSegundoNombre;
                contenido.remesador = oTblBusqueda.remesador;
                contenido.tasaCambio = oTblBusqueda.tasaCambio;
                contenido.valorEnviado = oTblBusqueda.valorEnviado;
                contenido.ValorPagar = oTblBusqueda.valorPagar;
                /*finaliza la construccion del json que se retorna */


                oTblBusqueda.jsonResultado = jsonResultadotStr;                         //<--- se inserta el json  resultado que se obtiene al convertir el xml a json
                oResponseApi.resutado = contenido;                                      //<--- se inserta el objeti contenido que se retorna al usuario la diferencia entre este y el anterior es que este tiene menos propiedades y su estructura es clara
                oTblBusqueda.xmlResponse = xml;                                         //<--- se inserta el xml obtenido de Red Cahpina
                oTblBusqueda.idCatEstadosBusquedasRch = idCatEstadosBusquedas;//<--- se inserta el id de estado de la busqueda en esta caso si esta buscada, encontrada, o procesada
                db.Entry(oTblBusqueda).State = EntityState.Modified;
                await db.SaveChangesAsync();
                /* - - - - - - se crea el objeto a retornar -  - - - - - - - */
                oResponseApi.codigo = responseApi.Codigo;
                oResponseApi.descripcion = responseApi.Descripcion;
                oResponseApi.idCooitza = oTblBusqueda.idTblTransaccionesRch;


                /*Se serealiza el objeto para guardarlo en la base de datos */
                var _jsonRespuesta = new StringContent(JsonConvert.SerializeObject(oResponseApi), Encoding.UTF8, "application/json");
                string _jsonRespuestaStr = await _jsonRespuesta.ReadAsStringAsync();
                oTblBusqueda.jsonResponse = _jsonRespuestaStr;
                db.Entry(oTblBusqueda).State = EntityState.Modified;
                await db.SaveChangesAsync();
                /*- - - - - fin de la asignaciones - - - - - - - - - - - - -*/
            }
            catch (Exception ex)
            {
                oResponseApi.codigo = 100;
                oResponseApi.idCooitza = 0;
                oResponseApi.descripcion = "Ocurrio un problema al actualizar el registro interno cws detalle: " + ex.Message;

            }
            return oResponseApi;
        }
        public async Task<RespuestaRepositorioReserva> UpdateBitacoraReservaOk(ResponseReserva _resultadoJson, RespuestaReservaApi respuestaReservaApi, long? idTblTransaccionesRch, string usuarioTransaccion, bool reserva)
        {

            var oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
            RespuestaRepositorioReserva oRepositorioRespuesta = new RespuestaRepositorioReserva();
            string _jsonResultadoStr = string.Empty;
            if (_resultadoJson != null)
            {
                try
                {
                    /*Se serealiza el objeto para guardarlo en la base de datos */
                    var _jsonRespuesta = new StringContent(JsonConvert.SerializeObject(_resultadoJson), Encoding.UTF8, "application/json");
                    _jsonResultadoStr = await _jsonRespuesta.ReadAsStringAsync();
                    /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                }
                catch (JsonSerializationException ex)
                {
                    oRepositorioRespuesta.Codigo = 300;
                    oRepositorioRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN LA RESERVA DE LA TRASANCCION DETALLE {ex.Message}";
                    return oRepositorioRespuesta;
                }
            }


            try
            {
                oTransaccion.reservaRequestXml = respuestaReservaApi.XmlRequest;
                oTransaccion.reservaResponseXml = respuestaReservaApi.XmlResponse;
                oTransaccion.reservaResultadoJson = _jsonResultadoStr;
                /*si reserva es igual a true es porque es la primera vez que se reserva y se insertan los datos de lo contrario
                 *unicamente se insertan los xml y los json tanto de respuesta y request
                 */
                if (reserva)
                {
                    oTransaccion.idReserva = respuestaReservaApi.CodigoReserva;
                    oTransaccion.idCatEstadosTransacciones = 2;
                    oTransaccion.usuarioReserva = usuarioTransaccion;
                    oTransaccion.fechaHoraReserva = DateTime.Now;
                }
                db.Entry(oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                /*se construye el objeto que retorna la respuesta*/
                oRepositorioRespuesta.Codigo = respuestaReservaApi.Codigo;
                oRepositorioRespuesta.Descripcion = respuestaReservaApi.Descripcion;
                oRepositorioRespuesta.idTblTransaccionesRch = oTransaccion.idTblTransaccionesRch;
                oRepositorioRespuesta.CodigoReserva = oTransaccion.idReserva;
            }
            catch (Exception ex)
            {
                oRepositorioRespuesta.Codigo = 405;
                oRepositorioRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL GUARDAR EL REGISTRO EN LA BASE DE DATOS CWS DETALLE: {ex.Message}";
            }

            try
            {
                /*Se serealiza el objeto a retornar al usuario para guardarlo en la base de datos */
                var _reservaResponseJson = new StringContent(JsonConvert.SerializeObject(oRepositorioRespuesta), Encoding.UTF8, "application/json");
                string _reservaResponseJsonStr = await _reservaResponseJson.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                var _oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
                _oTransaccion.reservaResponseJson = _reservaResponseJsonStr;
                db.Entry(_oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //return oRepositorioRespuesta;
            }
            catch (JsonSerializationException ex)
            {
                oRepositorioRespuesta.Codigo = 300;
                oRepositorioRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN LA RESERVA DE LA TRASANCCION DETALLE {ex.Message}";
                

            }
            return oRepositorioRespuesta;
        }
        public async Task<RespuestaRepositorioLibera> UpdateBitacoraLiberaOk(ResponseLibera _resultadoJson, RespuestaLiberaApi respuestaLiberaApi, long? idTblTransaccionesRch, string usuarioTransaccion)
        {
            var oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
            RespuestaRepositorioLibera oRepositorioRespuesta = new RespuestaRepositorioLibera();
            string _jsonResultadoStr = string.Empty;
            if (_resultadoJson != null)
            {
                try
                {
                    /*Se serealiza el objeto para guardarlo en la base de datos */
                    var _jsonRespuesta = new StringContent(JsonConvert.SerializeObject(_resultadoJson), Encoding.UTF8, "application/json");
                    _jsonResultadoStr = await _jsonRespuesta.ReadAsStringAsync();
                    /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                }
                catch (JsonSerializationException ex)
                {

                    _jsonResultadoStr = $"OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN LA RESERVA DE LA TRASANCCION DETALLE {ex.Message}";

                }
            }


            try
            {
                oTransaccion.liberaRequestXml = respuestaLiberaApi.XmlRequest;
                oTransaccion.liberaResponseXml = respuestaLiberaApi.XmlResponse;
                oTransaccion.liberaResultadoJson = _jsonResultadoStr;
                oTransaccion.idCatEstadosTransacciones = 5;
                oTransaccion.usuarioLibera = usuarioTransaccion;
                oTransaccion.fechaHoraLibera = DateTime.Now;
                oTransaccion.idLiberacion = _resultadoJson.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_LiberaResponse.result.ID_LIBERACION;
                db.Entry(oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                /*se construye el objeto que retorna la respuesta*/
                oRepositorioRespuesta.Codigo = respuestaLiberaApi.Codigo;
                oRepositorioRespuesta.Descripcion = respuestaLiberaApi.Descripcion;
                oRepositorioRespuesta.idTblTransaccionesRch = oTransaccion.idTblTransaccionesRch;
                oRepositorioRespuesta.CodigoLiberacion = oTransaccion.idLiberacion;
            }
            catch (Exception ex)
            {
                oRepositorioRespuesta.Codigo = 405;
                oRepositorioRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL GUARDAR EL REGISTRO EN LA BASE DE DATOS CWS DETALLE: {ex.Message}";
                oRepositorioRespuesta.idTblTransaccionesRch = oTransaccion.idTblTransaccionesRch;
                oRepositorioRespuesta.CodigoLiberacion = oTransaccion.idLiberacion;
            }

            try
            {
                /*Se serealiza el objeto a retornar al usuario para guardarlo en la base de datos */
                var _liberaResponseJson = new StringContent(JsonConvert.SerializeObject(oRepositorioRespuesta), Encoding.UTF8, "application/json");
                string _liberaResponseJsonStr = await _liberaResponseJson.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                var _oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
                _oTransaccion.liberaResponseJson = _liberaResponseJsonStr;
                db.Entry(_oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return oRepositorioRespuesta;
            }
            catch (Exception ex)
            {
                oRepositorioRespuesta.Codigo = 300;
                oRepositorioRespuesta.Descripcion = $"SERVICIO WEB COOITZA OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN LIBERA REMESA {ex.Message}";


            }
            return oRepositorioRespuesta;
        }
                     
        public async Task<RespuestaRepositorioPago> UpdateBitacoraPagoOk(ResponsesPago _resultadoJson, RespuestaExecutePagoApi respuestaExecuteApi, long? idTblTransaccionesRch, string usuarioTransaccion, bool pago, int? asociado, string numeroCuenta)
        {
            var oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
            RespuestaRepositorioPago oRepositorioRespuesta = new RespuestaRepositorioPago();
            string _jsonResultadoStr = string.Empty;
            if (_resultadoJson != null)
            {
                try
                {
                    /*Se serealiza el objeto para guardarlo en la base de datos */
                    var _jsonRespuesta = new StringContent(JsonConvert.SerializeObject(_resultadoJson), Encoding.UTF8, "application/json");
                    _jsonResultadoStr = await _jsonRespuesta.ReadAsStringAsync();
                    /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                }
                catch (JsonSerializationException ex)
                {
                    oRepositorioRespuesta.Codigo = 300;
                    oRepositorioRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN EL PAGO DE LA TRASANCCION DETALLE {ex.Message}";
                    oRepositorioRespuesta.IdCooitza = idTblTransaccionesRch;
                    return oRepositorioRespuesta;
                }
            }

            try
            {
                oTransaccion.pagoRequestXml = respuestaExecuteApi.XmlRequest;
                oTransaccion.pagoResponseXml = respuestaExecuteApi.XmlResponse;
                oTransaccion.pagoResultadoJson = _jsonResultadoStr;
                /*si reserva es igual a true es porque es la primera vez que se reserva y se insertan los datos de lo contrario
                 *unicamente se insertan los xml y los json tanto de respuesta y request
                 */
                if (pago)
                {
                    oTransaccion.idPago = respuestaExecuteApi.CodigoPago;
                    oTransaccion.idCatEstadosTransacciones = 3;
                    oTransaccion.usuarioPago = usuarioTransaccion;
                    oTransaccion.fechaHoraPago = DateTime.Now;
                    oTransaccion.asociado = asociado;
                    oTransaccion.numeroCuentaCooitza = numeroCuenta;
                }
                db.Entry(oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                /*se construye el objeto que retorna la respuesta*/
                oRepositorioRespuesta.Codigo = respuestaExecuteApi.Codigo;
                oRepositorioRespuesta.Descripcion = respuestaExecuteApi.Descripcion;
                oRepositorioRespuesta.IdCooitza = oTransaccion.idTblTransaccionesRch;
                oRepositorioRespuesta.CodigoPago = oTransaccion.idPago;
            }
            catch (Exception ex)
            {
                oRepositorioRespuesta.Codigo = 405;
                oRepositorioRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL GUARDAR EL REGISTRO DEL PAGO EN LA BASE DE DATOS CWS DETALLE: {ex.Message}";
                oRepositorioRespuesta.IdCooitza = idTblTransaccionesRch;
            }

            try
            {
                /*Se serealiza el objeto a retornar al usuario para guardarlo en la base de datos */
                var _PAGOResponseJson = new StringContent(JsonConvert.SerializeObject(oRepositorioRespuesta), Encoding.UTF8, "application/json");
                string _reservaResponseJsonStr = await _PAGOResponseJson.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                var _oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
                _oTransaccion.pagoResponseJson = _reservaResponseJsonStr;
                db.Entry(_oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //return oRepositorioRespuesta;
            }
            catch (JsonSerializationException ex)
            {
                oRepositorioRespuesta.Codigo = 300;
                oRepositorioRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN EL PAGO DE LA TRASANCCION DETALLE {ex.Message}";
                oRepositorioRespuesta.IdCooitza = idTblTransaccionesRch;

            }
            return oRepositorioRespuesta;

        }
                    
        public async Task<RespuestaRepositorioAnula> UpdateBitacoraAnulacionOk(ResponsesAnula _resultadoJson, ResponseAnula respuestaAnulaApi, long? idTblTransaccionesRch, string usuarioTransaccion, bool anulacion, string motivo)
        {

            var oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
            RespuestaRepositorioAnula oRepositorioAnula = new RespuestaRepositorioAnula();
            string _jsonResultadoStr = string.Empty;
            if (_resultadoJson != null)
            {
                try
                {
                    /*Se serealiza el objeto para guardarlo en la base de datos */
                    var _jsonRespuesta = new StringContent(JsonConvert.SerializeObject(_resultadoJson), Encoding.UTF8, "application/json");
                    _jsonResultadoStr = await _jsonRespuesta.ReadAsStringAsync();
                    /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                }
                catch (JsonSerializationException ex)
                {
                    oRepositorioAnula.Codigo = 300;
                    oRepositorioAnula.Descripcion = $"OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN LA RESERVA DE LA TRASANCCION DETALLE {ex.Message}";
                    return oRepositorioAnula;
                }
            }
            try
            {
                oTransaccion.anulaRequestXml = respuestaAnulaApi.XmlRequest;
                oTransaccion.anulaResponseXml = respuestaAnulaApi.XmlResponse;
                oTransaccion.anulaResultadoJson = _jsonResultadoStr;
                /*si reserva es igual a true es porque es la primera vez que se reserva y se insertan los datos de lo contrario
                 *unicamente se insertan los xml y los json tanto de respuesta y request
                 */
                if (anulacion)
                {
                    oTransaccion.idAnulacion = respuestaAnulaApi.CodigoAnulacion;
                    oTransaccion.idCatEstadosTransacciones = 1;/**la remesa vuelve a estar disponible*/
                    oTransaccion.usuarioAnula = usuarioTransaccion;
                    oTransaccion.fechaHoraAnulacion = DateTime.Now;
                    oTransaccion.motivoAnulacion = motivo;
                }
                db.Entry(oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                /*se construye el objeto que retorna la respuesta*/
                oRepositorioAnula.Codigo = respuestaAnulaApi.Codigo;
                oRepositorioAnula.Descripcion = respuestaAnulaApi.Descripcion;
                oRepositorioAnula.IdCooitza = oTransaccion.idTblTransaccionesRch;
                oRepositorioAnula.CodigoAnulacion = oTransaccion.idAnulacion;
            }
            catch (Exception ex)
            {
                oRepositorioAnula.Codigo = 405;
                oRepositorioAnula.Descripcion = $"OCURRIO UN PROBLEMA AL GUARDAR EL REGISTRO EN LA BASE DE DATOS CWS DETALLE: {ex.Message}";
            }

            try
            {
                /*Se serealiza el objeto a retornar al usuario para guardarlo en la base de datos */
                var _reservaResponseJson = new StringContent(JsonConvert.SerializeObject(oRepositorioAnula), Encoding.UTF8, "application/json");
                string _reservaResponseJsonStr = await _reservaResponseJson.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                var _oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTransaccionesRch);
                _oTransaccion.anulaResponseJson = _reservaResponseJsonStr;
                db.Entry(_oTransaccion).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //return oRepositorioRespuesta;
            }
            catch (JsonSerializationException ex)
            {
                oRepositorioAnula.Codigo = 300;
                oRepositorioAnula.Descripcion = $"OCURRIO UN PROBLEMA AL RSOLVER EL RESULTADO JSON EN LA RESERVA DE LA TRASANCCION DETALLE {ex.Message}";


            }
            return oRepositorioAnula;
        }

        public Configuracion GetConfiguraciones()
        {
            /*Esta consulta es para obtener las credenciales de acceso 
             * sistema 2 es para test
             * sistema 1 es para produccion
             */
            TblConfiguracionesRch Configuraciones = db.TblConfiguracionesRches.FirstOrDefault(x => x.idCatEstados == 1 && x.sistema == 2);
            Configuracion _configuracion = new Configuracion();
            if (Configuraciones == null)
            {
                _configuracion.Codigo = false;
            }
            else
            {
                _configuracion.Codigo = true;
                _configuracion.IntegracionId = Configuraciones.integracionId;
                _configuracion.PagadorId = Configuraciones.pagadorId;

                _configuracion.VerificacionTercero = Configuraciones.verificacionTercero;
                _configuracion.VerificacionAgente = Configuraciones.verificacionAgente;
                _configuracion.TipoRemesa = Configuraciones.tipoRemesa;
            }

            return _configuracion;


        }


    }
}