using Newtonsoft.Json;
using redchapinapayout.Models;
using redchapinapayout.Models.Json;
using redchapinapayout.Models.Json.Anula;
using redchapinapayout.Models.Json.Consulta.Consulta_item;
using redchapinapayout.Models.Json.Libera;
using redchapinapayout.Models.Json.Login;
using redchapinapayout.Models.Json.Pago;
using redchapinapayout.Models.Json.Reserva;
using redchapinapayout.Models.Peticiones;
using redchapinapayout.Models.RespuestasMetodos;
using redchapinapayout.Models.RespuestasMetodos.Anula;
using redchapinapayout.Models.RespuestasMetodos.Libera;
using redchapinapayout.Models.RespuestasMetodos.Pago;
using redchapinapayout.Models.RespuestasMetodos.Relationship;
using redchapinapayout.Models.RespuestasMetodos.Reserva;
using redchapinapayout.Models.RespuestasMetodos.TiposRemesas;
using redchapinapayout.Models.Seguridad;
using redchapinapayout.Repositorios;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Xml;
using System.Xml.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace redchapinapayout.Controllers
{
    [RoutePrefix("redchapinapayout")]
    public class redchapinapayoutController : ApiController
    {
        private cooitzacoreEntities db = new cooitzacoreEntities();
        long? idTransaccion = 0;
        private IServicio servicio;
        public redchapinapayoutController()
        {
            this.servicio = new Servicio();
        }

        [Route("Autenticar")]
        [HttpGet]
        public async Task<RespuestaRepositorioLogin> Autenticar()
        {
            RespuestaRepositorioLogin oRespuestaApi = new RespuestaRepositorioLogin();//obj de respuestas
            var _configuracion = servicio.GetConfiguraciones();
            if (!_configuracion.Codigo)
            {
                oRespuestaApi.idCatEstadosLogin = 2;
                oRespuestaApi.descripcionLogin = "Login: No existen credenciales de acceso para el sistema";

                return oRespuestaApi;
            }

            var client = new RestClient("https://portal.redchapina.com");

            var request = new RestRequest("/Pagador.php", Method.Post);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://tempuri.org/ITransactionService/ExecuteTransaction");
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:por=""https://portal.redchapina.com/"">
                        " + "\n" +
                        @"   <soapenv:Header/>
                        " + "\n" +
                        @"   <soapenv:Body>
                        " + "\n" +
                        @"      <por:Sesion>
                        " + "\n" +
                        @"         <registro>
                        " + "\n" +
                        $"            <ID_INTEGRACION>{_configuracion.IntegracionId}</ID_INTEGRACION> " + @"
                        " + "\n" +
                        $"            <ID_PAGADOR>{_configuracion.PagadorId}</ID_PAGADOR>" + @"
                        " + "\n" +
                        @"         </registro>
                        " + "\n" +
                        @"      </por:Sesion>
                        " + "\n" +
                        @"   </soapenv:Body>
                        " + "\n" +
                        @"</soapenv:Envelope>";

            request.AddParameter("text/xml", body, ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            //request.AddParameter("text/xml", body, ParameterType.RequestBody);
            //RestResponse response = await clien.ExecuteAsync(request);
            var json_respuesta = await client.ExecuteAsync(request);

            if (json_respuesta.StatusCode == HttpStatusCode.OK)
            {
                string output = json_respuesta.Content;//<--- si es exitoso accedemos al contenido del obheto json_respuesta
                //si el contenido es vacio
                if (!string.IsNullOrEmpty(output))
                {
                    //long? idCatEstadosLogin = 0; 
                    if (output.Contains("Internal") || output.Contains("internal"))
                    {
                        oRespuestaApi.idCatEstadosLogin = 2;
                        //oRespuestaApi.IdTblLoginRch = 0;
                        oRespuestaApi.descripcionLogin = "Login: Error de Comunicación con Red Chapina Status Internal Server Error";
                        //idCatEstadosLogin = 2;
                        //var oRepositorioRespuesta = await servicio.SetBitacoraLogin(body, output, oRespuestaApi, "");
                        //return oRepositorioRespuesta;
                    }
                    else if (output.Contains("<ns1:SesionResponse>"))
                    {
                        var salida = output.Replace(":", "_").Replace("-", "_");
                        var json = JSon.XmlToJSON(salida);
                        try
                        {
                            var resultado = JsonConvert.DeserializeObject<RespuestaLogin>(json);

                            if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_SesionResponse.result == "0000")
                            {
                                oRespuestaApi.idCatEstadosLogin = 1;
                                //oRespuestaApi.IdTblLoginRch = 0;
                                oRespuestaApi.descripcionLogin = "Success Login";
                                //idCatEstadosLogin = 1;
                                //oRespuestaApi.Resutado = resultado;
                                //var oRepositorioRespuesta = await servicio.SetBitacoraLogin(body, output, oRespuestaApi, "");
                            }
                            else if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_SesionResponse.result == "0001")
                            {
                                oRespuestaApi.idCatEstadosLogin = 2;
                                //oRespuestaApi.IdTblLoginRch = 0;
                                oRespuestaApi.descripcionLogin = "Login: Error de Autenticacion con el servicio Red Chapina";
                                //idCatEstadosLogin = 2;
                                //oRespuestaApi.Resutado = resultado;

                            }
                        }
                        catch (JsonException ex)
                        {

                            oRespuestaApi.idCatEstadosLogin = 2;
                            oRespuestaApi.descripcionLogin = "Login: Ocurrio un problema al resolver la respuesta del servicio Red Chapina detalle:" + ex.Message;

                        }


                    }
                    else
                    {
                        oRespuestaApi.idCatEstadosLogin = 2;
                        oRespuestaApi.descripcionLogin = "Login: El servicio Red Chapina dovolvio un coidgo desconocido";

                    }
                    var oRepositorioRespuesta = await servicio.SetBitacoraLogin(body, output, oRespuestaApi);
                    return oRepositorioRespuesta;
                }
                else
                {
                    oRespuestaApi.idCatEstadosLogin = 2;
                    oRespuestaApi.descripcionLogin = "El servicio Red Chapina respondio pero no devolvio contenido en la respuesta";
                    var oRepositorioRespuesta = await servicio.SetBitacoraLogin(body, output, oRespuestaApi);
                    return oRepositorioRespuesta;
                }
            }
            else
            {
                oRespuestaApi.idCatEstadosLogin = 2;
                oRespuestaApi.descripcionLogin = "Ocurrio un problema al obtener respuesta del servicio Red Cahpina en el Login";
                var oRepositorioRespuesta = await servicio.SetBitacoraLogin(body, "", oRespuestaApi);
                return oRepositorioRespuesta;
            }

        }
        [Route("GetTiposRemesas")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTiposRemesas(GetRelationship getRelationship)
        {
            RespuestaRelationsip oRespuestaApi = new RespuestaRelationsip();
            long? idTblLogs = 0;

            if (!ModelState.IsValid)
            {
                var entradasErroneas = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => x.Key.Split('.').Last())
                    .ToList();

                oRespuestaApi.Codigo = 101;
                oRespuestaApi.Descripcion = $"EXISTEN PARAMETROS NULOS O VACIOS: {string.Join(",", entradasErroneas)}";

                return Content(HttpStatusCode.OK, oRespuestaApi);
            }
            using (var db = new cooitzacoreEntities())
            {
                /*Se serealiza el objeto para guardarlo en la base de datos */
                var _jsonRequest = new StringContent(JsonConvert.SerializeObject(getRelationship), Encoding.UTF8, "application/json");
                string _jsonRequestStr = await _jsonRequest.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                TblLogCatalogosRch oLog = new TblLogCatalogosRch();
                oLog.jsonRequest = _jsonRequestStr;
                oLog.fechaHoraConsulta = DateTime.Now;
                oLog.idCooperativa = getRelationship.idCooperativa;
                oLog.idCatEstados = 1;
                db.TblLogCatalogosRches.Add(oLog);
                await db.SaveChangesAsync();
                idTblLogs = oLog.idTblLogCatalogosRch;
            }




            var login = await Login(getRelationship.usuarioWebService, getRelationship.claveWebService, getRelationship.token);

            if (!login)
            {
                /*objeto a retornar*/
                oRespuestaApi.Codigo = 500;
                oRespuestaApi.Descripcion = $"CREDENCIALES NO VALIDAS";

                using (var db = new cooitzacoreEntities())
                {
                    var _jsonRespuestaLogin = new StringContent(JsonConvert.SerializeObject(oRespuestaApi), Encoding.UTF8, "application/json");
                    string _jsonResultadoLoginStr = await _jsonRespuestaLogin.ReadAsStringAsync();
                    TblLogCatalogosRch log = await db.TblLogCatalogosRches.FirstOrDefaultAsync(x => x.idTblLogCatalogosRch == idTblLogs);
                    log.jsonResponse = _jsonResultadoLoginStr;
                    db.Entry(log).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return Content(HttpStatusCode.OK, oRespuestaApi);
            }


            var listado = (from d in db.CatTiposRemesas
                           where d.idCatEstados == 1
                           select new ResponseTiposRemesas
                           {
                               idTipoRemesa = d.idCatTiposRemesas,
                               descripcion = d.descripcion,
                               codigo = d.codigo
                           }).ToList();
            oRespuestaApi.Codigo = 200;
            oRespuestaApi.Descripcion = $"RESPUESTA CORRECTA";
            oRespuestaApi.resultado = listado;

            using (var db = new cooitzacoreEntities())
            {
                /*Se serealiza el objeto para guardarlo en la base de datos */
                var _jsonRespuestaOk = new StringContent(JsonConvert.SerializeObject(oRespuestaApi), Encoding.UTF8, "application/json");
                string _jsonResultadoOkStr = await _jsonRespuestaOk.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                TblLogCatalogosRch log = await db.TblLogCatalogosRches.FirstOrDefaultAsync(x => x.idTblLogCatalogosRch == idTblLogs);
                log.jsonResponse = _jsonResultadoOkStr;
                db.Entry(log).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Content(HttpStatusCode.OK, oRespuestaApi);
        }

        [Route("GetRelationship")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRelationship(GetRelationship getRelationship)
        {
            RespuestaRelationsip oRespuestaApi = new RespuestaRelationsip();
            long? idTblLogs = 0;
            if (!ModelState.IsValid)
            {
                var entradasErroneas = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => x.Key.Split('.').Last())
                    .ToList();

                oRespuestaApi.Codigo = 101;
                oRespuestaApi.Descripcion = $"EXISTEN PARAMETROS NULOS O VACIOS: {string.Join(",", entradasErroneas)}";

                return Content(HttpStatusCode.OK, oRespuestaApi);
            }
            using (var db = new cooitzacoreEntities())
            {
                /*Se serealiza el objeto para guardarlo en la base de datos */
                var _jsonRequest = new StringContent(JsonConvert.SerializeObject(getRelationship), Encoding.UTF8, "application/json");
                string _jsonRequestStr = await _jsonRequest.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                TblLogCatalogosRch oLog = new TblLogCatalogosRch();
                oLog.jsonRequest = _jsonRequestStr;
                oLog.fechaHoraConsulta = DateTime.Now;
                oLog.idCooperativa = getRelationship.idCooperativa;
                oLog.idCatEstados = 1;
                db.TblLogCatalogosRches.Add(oLog);
                await db.SaveChangesAsync();
                idTblLogs = oLog.idTblLogCatalogosRch;
            }




            var login = await Login(getRelationship.usuarioWebService, getRelationship.claveWebService, getRelationship.token);

            if (!login)
            {
                /*objeto a retornar*/
                oRespuestaApi.Codigo = 500;
                oRespuestaApi.Descripcion = $"CREDENCIALES NO VALIDAS";

                using (var db = new cooitzacoreEntities())
                {
                    var _jsonRespuestaLogin = new StringContent(JsonConvert.SerializeObject(oRespuestaApi), Encoding.UTF8, "application/json");
                    string _jsonResultadoLoginStr = await _jsonRespuestaLogin.ReadAsStringAsync();
                    TblLogCatalogosRch log = await db.TblLogCatalogosRches.FirstOrDefaultAsync(x => x.idTblLogCatalogosRch == idTblLogs);
                    log.jsonResponse = _jsonResultadoLoginStr;
                    db.Entry(log).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return Content(HttpStatusCode.OK, oRespuestaApi);
            }


            var listado = (from d in db.CatRelationshipRemitentes
                           where d.idCatEstados == 1
                           select new ResponseRelationShip
                           {
                               idRelationShip = d.idCatRelationshipRemitentes,
                               descripcion = d.descripcion,
                               idTipoRemesa = d.idCatTiposRemesas
                           }).ToList();
            oRespuestaApi.Codigo = 200;
            oRespuestaApi.Descripcion = $"RESPUESTA CORRECTA";
            oRespuestaApi.resultado = listado;

            using (var db = new cooitzacoreEntities())
            {
                /*Se serealiza el objeto para guardarlo en la base de datos */
                var _jsonRespuestaOk = new StringContent(JsonConvert.SerializeObject(oRespuestaApi), Encoding.UTF8, "application/json");
                string _jsonResultadoOkStr = await _jsonRespuestaOk.ReadAsStringAsync();
                /* - - - - - - - - -fin de la serealizacion - - - - - - - -*/
                TblLogCatalogosRch log = await db.TblLogCatalogosRches.FirstOrDefaultAsync(x => x.idTblLogCatalogosRch == idTblLogs);
                log.jsonResponse = _jsonResultadoOkStr;
                db.Entry(log).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Content(HttpStatusCode.OK, oRespuestaApi);
        }
        [Route("AnularPagoRemesa")]
        [HttpPost]
        public async Task<IHttpActionResult> AnulaRemesa(AnulaRemesa anulaRemesa)
        {
            try
            {
                var _configuracion = servicio.GetConfiguraciones();


                ResponseAnula oRespuestaApi = new ResponseAnula();
                RespuestaRepositorioAnula respuestaRepositorioAnula = new RespuestaRepositorioAnula();
                bool anulada = false;

                if (!ModelState.IsValid)
                {
                    var entradasErroneas = ModelState
                        .Where(x => x.Value.Errors.Any())
                        .Select(x => x.Key.Split('.').Last())
                        .ToList();

                    /*objeto a retornar*/
                    respuestaRepositorioAnula.Codigo = 101;
                    respuestaRepositorioAnula.Descripcion = $"EXISTEN PARAMETROS VACIOS O NULOS: {string.Join(",", entradasErroneas)}";
                    respuestaRepositorioAnula.IdCooitza = 0;
                    anulada = false;
                    //respuestaRepositorioAnula = await servicio.UpdateBitacoraAnulacionOk(null, oRespuestaApi, oTransaccion.idTblTransaccionesRch, anulaRemesa.usuarioTransaccion, anulada);
                    return Content(HttpStatusCode.OK, respuestaRepositorioAnula);
                }

                using (var db = new cooitzacoreEntities())
                {
                    if (await Login(anulaRemesa.usuarioWebService, anulaRemesa.claveWebService, anulaRemesa.token))
                    {
                        /*aqui se genera el token*/
                    }
                    else
                    {
                        respuestaRepositorioAnula.Codigo = 500;
                        respuestaRepositorioAnula.IdCooitza = 0;
                        respuestaRepositorioAnula.Descripcion = "CREDENCIALES NO VALIDAS";
                        return Content(HttpStatusCode.OK, respuestaRepositorioAnula);
                    }
                }
                /**
                * procede a buscar en la tabla de transacciones el id que brinda el usuario 
                */
                var oTransaccion = new TblTransaccionesRch();
                using (var db = new cooitzacoreEntities())
                {
                    oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == anulaRemesa.idCooitza);

                    if (oTransaccion == null)
                    {
                        oRespuestaApi.Codigo = 102;
                        oRespuestaApi.Descripcion = $"OCURRIO UN PROBLEMA AL INTENTAR RECUPERAR LOS DATOS DE LA REMESA IdCooitza {anulaRemesa.idCooitza} NO ES VALIDO";
                        return Content(HttpStatusCode.OK, oRespuestaApi);
                    }


                    var _anulacionRequest = new StringContent(JsonConvert.SerializeObject(anulaRemesa), Encoding.UTF8, "application/json");
                    string _anulacionRequestJsonStr = await _anulacionRequest.ReadAsStringAsync();
                    oTransaccion.anulaRequestJson = _anulacionRequestJsonStr;
                    db.Entry(oTransaccion).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                /**A continuacion se procede a buscar el id de pago dentro de los registros donde su idPago no se nulo 
                 esto debido a que cuando se busca la transaccion con otro id distinto al que tiene el id de pago 
                no tiene el id de pago entonces es necesario buscar el ultimo id de pago de esa transaccion*/
                var result = (from r in db.TblTransaccionesRches
                              where r.idOperacion == oTransaccion.idOperacion
                                    && r.idInterno == oTransaccion.idInterno
                                    && r.idPago != null
                              orderby r.idTblTransaccionesRch descending
                              select r).FirstOrDefault();

                if (result == null)
                {
                    oRespuestaApi.Codigo = 8851;
                    oRespuestaApi.Descripcion = $"NO EXISTE REFERENCIA DE PAGO PARA ESTA REMESA";
                    anulada = false;
                    respuestaRepositorioAnula = await servicio.UpdateBitacoraAnulacionOk(null, oRespuestaApi, oTransaccion.idTblTransaccionesRch, anulaRemesa.usuarioTransaccion, anulada, null);
                    return Content(HttpStatusCode.OK, respuestaRepositorioAnula);
                }


                var client = new RestClient("https://portal.redchapina.com");
                var request = new RestRequest("/Pagador.php", Method.Post);
                request.AddHeader("Content-Type", "text/xml; charset=utf-8");
                request.AddHeader("SOAPAction", "http://tempuri.org/ITransactionService/ExecuteTransaction");
                var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:por=""https://portal.redchapina.com/"">
                        " + "\n" +
                            @"   <soapenv:Header/>
                        " + "\n" +
                            @"   <soapenv:Body>
                        " + "\n" +
                            @"      <por:AnulaPago>
                        " + "\n" +
                            @"         <registro>
                        " + "\n" +
                            $"            <ID_INTEGRACION>{_configuracion.IntegracionId}</ID_INTEGRACION>" + @"
                        " + "\n" +
                            $"            <USUARIO>{anulaRemesa.usuarioTransaccion}</USUARIO>" + @"
                        " + "\n" +
                            $"            <ID_INTERNO>{oTransaccion.idInterno}</ID_INTERNO>" + @"
                        " + "\n" +
                            $"            <ID_PAGADOR>{_configuracion.PagadorId}</ID_PAGADOR>" + @"
                        " + "\n" +
                            $"            <ID_PAGO>{result.idPago}</ID_PAGO>" + @"
                        " + "\n" +
                            $"            <ID_OPERACION>{oTransaccion.idOperacion}</ID_OPERACION>" + @"
                        " + "\n" +
                            @"            <CORRELATIVO_ID>0</CORRELATIVO_ID>
                        " + "\n" +
                            $"            <MOTIVO_ANULACION>{anulaRemesa.motivoAnulacion}</MOTIVO_ANULACION>" + @"
                        " + "\n" +
                            $"            <DETALLE_ANULACION>{anulaRemesa.motivoAnulacion}</DETALLE_ANULACION>" + @"
                        " + "\n" +
                            @"         </registro>
                        " + "\n" +
                            @"      </por:AnulaPago>
                        " + "\n" +
                            @"   </soapenv:Body>
                        " + "\n" +
                            @"</soapenv:Envelope>";
                request.AddParameter("text/xml", body, ParameterType.RequestBody);
                /*se procesa la respuesta*/
                var json_respuesta = await client.ExecuteAsync(request);            //<---se ejecuta el cliente api y se alamcena en json_respuesta

                string output = json_respuesta.Content;

                if (string.IsNullOrEmpty(output))
                {
                    oRespuestaApi.Codigo = -3;
                    oRespuestaApi.Descripcion = $"CWS: IMPSIBLE RESOLVER EL CONTENIDO DE LA RESPUESTA DEL SERVICIO RED CHAPINA";
                    anulada = false;
                    respuestaRepositorioAnula = await servicio.UpdateBitacoraAnulacionOk(null, oRespuestaApi, oTransaccion.idTblTransaccionesRch, anulaRemesa.usuarioTransaccion, anulada, null);
                    return Content(HttpStatusCode.OK, respuestaRepositorioAnula);
                }
                /**
                 * asignacion de los xml request y response del servicio*/
                oRespuestaApi.XmlRequest = body;
                oRespuestaApi.XmlResponse = output;

                if (!json_respuesta.IsSuccessStatusCode)
                {
                    oRespuestaApi.Codigo = 751;
                    oRespuestaApi.Descripcion = $"ERROR RESPUESTA NO EXITOSA {json_respuesta.ErrorMessage}";
                    anulada = false;
                    respuestaRepositorioAnula = await servicio.UpdateBitacoraAnulacionOk(null, oRespuestaApi, oTransaccion.idTblTransaccionesRch, anulaRemesa.usuarioTransaccion, anulada, null);

                    return Content(HttpStatusCode.OK, respuestaRepositorioAnula);
                }

                var salida = output.Replace(":", "_").Replace("-", "_");    //<--- reemplazamos los : o sean bien - por guines bajos
                var json = JSon.XmlToJSON(salida);
                var resultado = JsonConvert.DeserializeObject<ResponsesAnula>(json);

                if (string.IsNullOrEmpty(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_AnulaPagoResponse.result.CODIGO_MENSAJE))
                {
                    oRespuestaApi.Codigo = 751;
                    oRespuestaApi.Descripcion = $"OCURRIO UN PROBLEMA EL SERVICIO RED CHAPINA NO DEVOLVIO UN CODIGO DE REFERENCIA";
                    anulada = false;
                    respuestaRepositorioAnula = await servicio.UpdateBitacoraAnulacionOk(resultado, oRespuestaApi, oTransaccion.idTblTransaccionesRch, anulaRemesa.usuarioTransaccion, anulada, null);
                    //oRepoRespuestaPago = await servicio.UpdateBitacoraPagoOk(null, oRespuestaApi, idTblTrnsaccionesRch, _usuarioTransaccion, pago);
                    //return oRepoRespuestaPago;
                }
                if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_AnulaPagoResponse.result.CODIGO_MENSAJE == "1900")
                {
                    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_AnulaPagoResponse.result.CODIGO_MENSAJE);
                    oRespuestaApi.Descripcion = $"TRANSACCION EXITOSA {(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_AnulaPagoResponse.result.TEXTO_MENSAJE).ToUpper()}";
                    anulada = true;
                    oRespuestaApi.CodigoAnulacion = resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_AnulaPagoResponse.result.ID_ANULACION;
                    respuestaRepositorioAnula = await servicio.UpdateBitacoraAnulacionOk(resultado, oRespuestaApi, oTransaccion.idTblTransaccionesRch, anulaRemesa.usuarioTransaccion, anulada, anulaRemesa.motivoAnulacion);
                }
                else
                {
                    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_AnulaPagoResponse.result.CODIGO_MENSAJE);
                    oRespuestaApi.Descripcion = $"OCURRIO UN PROBLEMA {(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_AnulaPagoResponse.result.TEXTO_MENSAJE).ToUpper()}";
                    anulada = false;
                    respuestaRepositorioAnula = await servicio.UpdateBitacoraAnulacionOk(resultado, oRespuestaApi, oTransaccion.idTblTransaccionesRch, anulaRemesa.usuarioTransaccion, anulada, null);
                }

                return Content(HttpStatusCode.OK, respuestaRepositorioAnula);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.OK, ex.Message);
            }

        }
        public async Task<RespuestaConsulta> UpdateRemesa(string numeroRemesa, string usuarioTransaccion)
        {
            var _configuracion = servicio.GetConfiguraciones();


            var client = new RestClient("https://portal.redchapina.com");
            var request = new RestRequest("/Pagador.php", Method.Post);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://tempuri.org/ITransactionService/ExecuteTransaction");
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:por=""https://portal.redchapina.com/"">
                        " + "\n" +
                        @"   <soapenv:Header/>
                        " + "\n" +
                        @"   <soapenv:Body>
                        " + "\n" +
                        @"      <por:ConsultaGiro>
                        " + "\n" +
                        @"         <registro>
                        " + "\n" +
                        $"            <ID_INTEGRACION>{_configuracion.IntegracionId}</ID_INTEGRACION>" + @"
                        " + "\n" +
                        $"            <USUARIO>{usuarioTransaccion}</USUARIO>" + @"
                        " + "\n" +
                        $"            <ID_OPERACION>{numeroRemesa}</ID_OPERACION>" + @"
                        " + "\n" +
                        $"            <ID_PAGADOR>{_configuracion.PagadorId}</ID_PAGADOR>" + @"
                        " + "\n" +
                        @"            <CORRELATIVO_ID>0</CORRELATIVO_ID>
                        " + "\n" +
                        @"            <MONTO_PAGAR>0</MONTO_PAGAR>
                        " + "\n" +
                        @"            <AGENT_ID></AGENT_ID>
                        " + "\n" +
                        @"         </registro>
                        " + "\n" +
                        @"      </por:ConsultaGiro>
                        " + "\n" +
                        @"   </soapenv:Body>
                        " + "\n" +
                        @"</soapenv:Envelope>";

            request.AddParameter("text/xml", body, ParameterType.RequestBody);
            /*se procesa la respuesta*/
            var json_respuesta = await client.ExecuteAsync(request);            //<---se ejecuta el cliente api y se alamcena en json_respuesta
            string output = json_respuesta.Content;
            var salida = output.Replace(":", "_").Replace("-", "_");    //<--- reemplazamos los : o sean bien - por guines bajos
            var json = JSon.XmlToJSON(salida);
            var resultado = JsonConvert.DeserializeObject<RespuestaConsulta>(json);

            return resultado;
        }

        [Route("ConsultaRemesa")]
        [HttpPost]
        public async Task<IHttpActionResult> ConsultaRemesa(ConsultaRemesa onsulta)
        {
            RespuestaApi oRespuestaApi = new RespuestaApi();//obj de respuestas



            /*
             * A continuacion busca en la base de datos cws el id de integracion y el id de pagador 
             */
            var _configuracion = servicio.GetConfiguraciones();
            //if (!_configuracion.Codigo)
            //{
            //    oRespuestaApi.Codigo = -2;
            //    oRespuestaApi.IdCooitza = 0;
            //    oRespuestaApi.Descripcion = "Login: NO EXISTEN CREDENCIALES DE ACCESO AL SERVICIO WEB";
            //    return Content(HttpStatusCode.OK, oRespuestaApi);
            //}
            /* realiza el proceso de autenticar para comprobar que se tenga accesos validos */
            var login = await Autenticar();     //<--- esperamos la respuesta del login 
            if (login.CodigoBd != 0)            //<--- se valida el resultado del login con red chapina
            {
                oRespuestaApi.Codigo = login.CodigoBd; //<--- el id ya lo retorna el metodo autenticar
                oRespuestaApi.IdCooitza = 0;           //<--- como no se logueo correctamente no hay inserciones validas
                oRespuestaApi.Descripcion = login.descripcionLogin; //<--- descripcion del metodo autenticas
                return Content(HttpStatusCode.OK, oRespuestaApi);
            }

            if (!ModelState.IsValid)
            {
                var entradasErroneas = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => x.Key.Split('.').Last())
                    .ToList();
                //return BadRequest($"Los siguientes parámetros están vacíos o nulos: {string.Join(",", entradasErroneas)}");

                /*objeto a retornar*/
                oRespuestaApi.Codigo = 101;
                oRespuestaApi.Descripcion = $"EXISTEN PARAMETROS NULOS O VACIOS: {string.Join(",", entradasErroneas)}";
                var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, login.idTblTransacionesRch, null, 12);
                return Content(HttpStatusCode.OK, oRServicio);
            }
            using (var db = new cooitzacoreEntities())
            {
                if (await Login(onsulta.usuarioWebService, onsulta.claveWebService, onsulta.token))
                {
                    /*aqui se genera el token*/
                }
                else
                {
                    oRespuestaApi.Codigo = 500;
                    oRespuestaApi.IdCooitza = 0;
                    oRespuestaApi.Descripcion = "CREDENCIALES NO VALIDAS";

                    var transaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == login.idTblTransacionesRch);
                    var _jsonRequest = new StringContent(JsonConvert.SerializeObject(onsulta), Encoding.UTF8, "application/json");
                    string _resultadoRequestStr = await _jsonRequest.ReadAsStringAsync();

                    var _jsonResponse = new StringContent(JsonConvert.SerializeObject(oRespuestaApi), Encoding.UTF8, "application/json");
                    string _resultadoResponsetStr = await _jsonResponse.ReadAsStringAsync();

                    transaccion.jsonRequest = _resultadoRequestStr;
                    transaccion.jsonResponse = _resultadoResponsetStr;
                    db.Entry(transaccion).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return Content(HttpStatusCode.OK, oRespuestaApi);
                }
            }




            var sucursalExiste = await servicio.ExisteSucursal(onsulta.sucursalId);
            if (!sucursalExiste)
            {
                oRespuestaApi.Codigo = 103;
                oRespuestaApi.Descripcion = $"CWS: NO ENCONTRO UNA SUCURSAL VALIDA CON EL EL CODIGO SucursalId {onsulta.sucursalId} ENVIADO";
                var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, login.idTblTransacionesRch, null, 12);
                return Content(HttpStatusCode.OK, oRServicio);
            }

            var client = new RestClient("https://portal.redchapina.com");
            var request = new RestRequest("/Pagador.php", Method.Post);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://tempuri.org/ITransactionService/ExecuteTransaction");
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:por=""https://portal.redchapina.com/"">
                        " + "\n" +
                        @"   <soapenv:Header/>
                        " + "\n" +
                        @"   <soapenv:Body>
                        " + "\n" +
                        @"      <por:ConsultaGiro>
                        " + "\n" +
                        @"         <registro>
                        " + "\n" +
                        $"            <ID_INTEGRACION>{_configuracion.IntegracionId}</ID_INTEGRACION>" + @"
                        " + "\n" +
                        $"            <USUARIO>{onsulta.usuarioTransaccion}</USUARIO>" + @"
                        " + "\n" +
                        $"            <ID_OPERACION>{onsulta.numeroRemesa}</ID_OPERACION>" + @"
                        " + "\n" +
                        $"            <ID_PAGADOR>{_configuracion.PagadorId}</ID_PAGADOR>" + @"
                        " + "\n" +
                        @"            <CORRELATIVO_ID>0</CORRELATIVO_ID>
                        " + "\n" +
                        @"            <MONTO_PAGAR>0</MONTO_PAGAR>
                        " + "\n" +
                        @"            <AGENT_ID></AGENT_ID>
                        " + "\n" +
                        @"         </registro>
                        " + "\n" +
                        @"      </por:ConsultaGiro>
                        " + "\n" +
                        @"   </soapenv:Body>
                        " + "\n" +
                        @"</soapenv:Envelope>";

            request.AddParameter("text/xml", body, ParameterType.RequestBody);

            /* * * * * * * * * * bitacora * * * */

            var jsonEntrada = new StringContent(JsonConvert.SerializeObject(onsulta), Encoding.UTF8, "application/json");
            string jsonRequestStr = await jsonEntrada.ReadAsStringAsync();
            TblTransaccionesRch oTblBusquedas = new TblTransaccionesRch();
            oTblBusquedas.fechaBusqueda = DateTime.Now;
            oTblBusquedas.jsonRequest = jsonRequestStr;
            oTblBusquedas.xmlRequest = body;
            oTblBusquedas.usuarioTransaccion = onsulta.usuarioTransaccion;
            oTblBusquedas.usuarioWebService = onsulta.usuarioWebService;
            oTblBusquedas.idCatEstados = 1;
            oTblBusquedas.idSucursal = onsulta.sucursalId;
            oTblBusquedas.idCatEstadosBusquedasRch = 1;

            /* - - - - - - - - - - - - -inicio de la bitacora - - - - - - - - - - - - - 
             *
                                                                                        */
            var exito = servicio.SetBitacoraBusqueda(oTblBusquedas, login.idTblTransacionesRch);//<--- primer llamda al repositorio
            if (exito.Codigo == 0000)                   //<--- si el codigo que retorna el servicio de bitacora es 0 es porque inserto todo correcto la bd
            {
                idTransaccion = exito.IdCooitza;        //<--- se carga la variable idTransaccion con el id que retorno el servicio
            }
            else
            {
                return Content(HttpStatusCode.OK, exito);//<--- de lo contrario el servicio ya retorna un objeto cargado con la infromacion de la falla
            }
            /* - - - - - - - - - - - - -fin de la bitacora - - - - - - - - - - - - - -*/


            /*se procesa la respuesta*/
            var json_respuesta = await client.ExecuteAsync(request);            //<---se ejecuta el cliente api y se alamcena en json_respuesta
            string output = json_respuesta.Content;                             //<---se accede a la respuesta con .content  y se almacena en output
            if (json_respuesta.StatusCode == HttpStatusCode.InternalServerError)//<---se valida si el estado de la respuesta es error
            {
                long? idCatEstadosBusquedas = 0;
                if (string.IsNullOrEmpty(output))                              //<---se valida que la respuesta no este vacia 
                {
                    oRespuestaApi.Codigo = 8870;
                    oRespuestaApi.IdCooitza = 0;
                    oRespuestaApi.Descripcion = $"IMPSIBLE RESOLVER EL CONTENIDO DE LA RESPUESTA DEL SERVICIO RED CHAPINA";
                    idCatEstadosBusquedas = 4;
                    var oRService = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, idCatEstadosBusquedas);//<--- se inserta esta respuesta en la base de dats
                    return Content(HttpStatusCode.OK, oRService);//<--- se retorna al usuario
                }


                if (output.Contains("Call to a member function getPrefijo() on string"))//<--- se valida que la respuesta no responda esa cadena si la contiene es porque ese numero no existe
                {
                    oRespuestaApi.Codigo = 8871;
                    oRespuestaApi.IdCooitza = 0;
                    oRespuestaApi.Descripcion = $"EL NUMERO DE REMESA {onsulta.numeroRemesa} NO ES VALIDO";
                    idCatEstadosBusquedas = 4;

                }
                else
                {
                    oRespuestaApi.Codigo = 8872;
                    oRespuestaApi.IdCooitza = 0;
                    oRespuestaApi.Descripcion = "OCURRIO UN PROBLEMA AL INTENTAR RESOLVER EL CONTENDO DE LA RESPUESTA DE RED CHAPINA INTERNAL SERVER ERROR";//<--- se descnoce el codigo de respuesta
                    idCatEstadosBusquedas = 12;
                }
                var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, idCatEstadosBusquedas);//<--- se inserta esta respuesta en la base de dats

                return Content(HttpStatusCode.OK, oRServicio);//<--- se retorna al usuario
            }
            if (json_respuesta.StatusCode == HttpStatusCode.OK)                     //<--- se valida que el estado de la respuesta sea valido 200 
            {
                if (!string.IsNullOrEmpty(output))                                  //<--- se valida que la respuesta no este vacia
                {
                    if (output.Contains("Internal") || output.Contains("internal")) //<--- se valida que no contenga internal server error esto porque el servicio comumnete retorna status ok para todos los casos 
                    {
                        oRespuestaApi.Codigo = 8872;
                        oRespuestaApi.IdCooitza = 0;
                        oRespuestaApi.Descripcion = "OCURRIO UN PROBLEMA AL INTENTAR RESOLVER EL CONTENDO DE RED CHAPINA INTERNAL SERVER ERROR";

                        var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 12);
                        return Content(HttpStatusCode.OK, oRServicio);
                        //return Content(HttpStatusCode.OK, oRespuestaApi);
                    }
                    if (output.Contains("<ns1:ConsultaGiroResponse>"))              //<--- si la respuesta contiene este texto es poque trae respuesta
                    {
                        var salida = output.Replace(":", "_").Replace("-", "_");    //<--- reemplazamos los : o sean bien - por guines bajos
                        var json = JSon.XmlToJSON(salida);                          //<--- con las anteriores validaciones pasamos la cadena salida a la funcion que convierte en json el string de xml
                                                                                    //return Content(HttpStatusCode.OK, json);
                        /*
                         * para esta parte si el proceso salio bien la variable json estara cargada con el json que corresponde a la respuesta
                         */
                        var resultado = JsonConvert.DeserializeObject<RespuestaConsulta>(json);                                 //<--- deserealizamos el json de tipo respuestaConsulta

                        if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.CANTIDAD_REGISTROS == "1")//<--- condicionamos si el objeto trae cantidad de registros 1 si no trae es porque respondio bien pero no hay listados
                        {
                            if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO != null)      //<--- simple se valido que la propiedad listado este vacia
                            {
                                RespuestaApi oRServicio = new RespuestaApi();
                                if (!string.IsNullOrEmpty(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO))
                                {
                                    var oCatEstadosBusquedas = db.CatEstadosBusquedasRches.FirstOrDefault(x => x.descripcion.ToLower() == resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO.ToLower());

                                    if (oCatEstadosBusquedas == null)
                                    {
                                        oRespuestaApi.Codigo = 8873;                         //<--- se esablece el codigo 11 no disponible
                                        oRespuestaApi.IdCooitza = 0;                       //<--- se establce el id con 0, el servicio lo constuyr
                                        oRespuestaApi.Descripcion = $"EL SERVICIO WEB COOITZA NO ENCONCTRO UN CODIGO DE REFERENCIA PARA EL CODIGO RED CHAPINA ESTADO DEL GIRO {resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO.ToUpper()}";//<--- se concatena la descripcion con la que retorna el servicio
                                        oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                                        return Content(HttpStatusCode.OK, oRServicio);
                                    }


                                    if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO == "Disponible" ||
                                        resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO == "Reservada" ||
                                        resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO == "Pagada"
                                        )//<--- se valida que contenga algun estado donde sea psobile seralizar la respuesta
                                    {
                                        oRespuestaApi.Codigo = oCatEstadosBusquedas.codigoEstado;                   //<--- codigoEstado del la base de datos segun el first or deault que se hizo arriba
                                        oRespuestaApi.IdCooitza = 0;                                                //<--- se setea el 0 porque el repositorio constuye el id
                                        oRespuestaApi.Descripcion = $"REMESA {oCatEstadosBusquedas.descripcion}";   //<--- se establece el emnsaje con la descripcion del estado segun el  first or deault que se hizo arriba
                                        oRespuestaApi.Resutado = resultado;                                         //<--- seta  la varaible resultado esta variabes contiene el json del xml original
                                        /*dentro del servicio se realiza la insercion a la bd de cada parametro 
                                         *oRespuestaApi = el esl objeto que se va retornar al usuario se envia al respositorio porque ahi dentro se va seralizar y convertir en string para guardar el el campo jsonresponse de la bd
                                         *idTransaccion = es el id de la tabla a la que se le hara udpdate en el servicio 
                                         *output = es el xml que respondio el servico rech chapina
                                         *13 = es el estado 
                                         */
                                        RespuestaRch oRepositorioRespuesta = await servicio.UpdateBitacoraBusquedaOk(oRespuestaApi, idTransaccion, output, oCatEstadosBusquedas.idCatEstadosBusquedasRch);//<--- el respositorio UpdateBitacoraBusquedaOk recibe el objeto respuesta, el id de transaccion  y el xml original 
                                        return Content(HttpStatusCode.OK, oRepositorioRespuesta);                              //<--- se retorna la respuesta procesada
                                    }
                                    else
                                    {
                                        oRespuestaApi.Codigo = oCatEstadosBusquedas.codigoEstado;//<--- se esablece el codigo 11 no disponible
                                        oRespuestaApi.IdCooitza = 0;                       //<--- se establce el id con 0, el servicio lo constuyr
                                        oRespuestaApi.Descripcion = "REMESA NO DISPONIBLE PARA PAGO: " + oCatEstadosBusquedas.descripcion;//<--- se concatena la descripcion con la que retorna el servicio
                                        oRespuestaApi.Resutado = resultado;
                                        oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, oCatEstadosBusquedas.idCatEstadosBusquedasRch);
                                        return Content(HttpStatusCode.OK, oRServicio);
                                    }
                                }
                                else
                                {
                                    oRespuestaApi.Codigo = 8875;                         //<--- se esablece el codigo 11 no disponible
                                    oRespuestaApi.IdCooitza = 0;                       //<--- se establce el id con 0, el servicio lo constuyr
                                    oRespuestaApi.Descripcion = "IMPOSIBLE RESOLVER LOS DATOS, EL ESTADO DEL GIRO  ES DESCONOCIDO";//<--- se concatena la descripcion con la que retorna el servicio
                                    oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                                    return Content(HttpStatusCode.OK, oRServicio);
                                }
                            }
                            else
                            {
                                oRespuestaApi.Codigo = 8876;
                                oRespuestaApi.IdCooitza = 0;
                                oRespuestaApi.Descripcion = "IMPOSIBLE RESOLVER LA REMESA EL LISTADO ES NULL";//<-- el listado esta vacio
                                var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                                return Content(HttpStatusCode.OK, oRServicio);
                            }

                        }
                        else if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.CANTIDAD_REGISTROS == "0")//<--- si el objeto no contienen cantidad de registros es porque biene vacio
                        {
                            if (!string.IsNullOrEmpty(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.CODIGO_MENSAJE))
                            {
                                if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.CODIGO_MENSAJE == "8888")
                                {
                                    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.CODIGO_MENSAJE);
                                    oRespuestaApi.Descripcion = (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.TEXTO_MENSAJE).ToUpper();
                                }
                                else if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.CODIGO_MENSAJE == "0000")
                                {
                                    oRespuestaApi.Codigo = 8884;
                                    oRespuestaApi.Descripcion = "ERROR SIN REFERENCIAS PARA ESTE NUMERO DE REMESA";
                                }
                                else
                                {
                                    oRespuestaApi.Codigo = 8883;
                                    oRespuestaApi.Descripcion = $"NUMERO DE REMEA NO CUMPLE CON LA ESTRUCTURA CORRECTA";
                                }
                            }
                            else
                            {
                                oRespuestaApi.IdCooitza = 8881;
                                oRespuestaApi.Descripcion = "IMPOSIBLE RESOLVER LOS DATOS PARA EL NUMERO DE REMESA ENVIADO";
                            }


                            var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                            return Content(HttpStatusCode.OK, oRServicio);
                        }
                        else
                        {
                            oRespuestaApi.Codigo = 8882;
                            oRespuestaApi.IdCooitza = 0;
                            oRespuestaApi.Descripcion = "OCURRIO UN PROBLEMA PARA RESOLVER EL DETALLE CANTIDAD_REGISTROS = 0";

                            var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                            return Content(HttpStatusCode.OK, oRServicio);
                        }


                    }
                    else
                    {
                        oRespuestaApi.Codigo = 8885;
                        oRespuestaApi.IdCooitza = 0;
                        oRespuestaApi.Descripcion = "ERROR DE INTERPRETACION PARA EL CONTENIDO DE LA PRESPUESTA CODIGO DESCONOCIDO";
                        var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                        return Content(HttpStatusCode.OK, oRServicio);
                    }
                }
                else
                {
                    oRespuestaApi.Codigo = 8885;
                    oRespuestaApi.IdCooitza = 0;
                    oRespuestaApi.Descripcion = "ERROR DE INTERPRETACION PARA EL CONTENIDO DE LA PRESPUESTA";
                    var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                    return Content(HttpStatusCode.OK, oRServicio);
                }
            }
            else
            {
                oRespuestaApi.Codigo = 8886;
                oRespuestaApi.IdCooitza = 0;
                oRespuestaApi.Descripcion = $"ERROR DE COMUNICACION CON EL SERVICIO RED CHAPINA ESTADO {json_respuesta.StatusDescription}";

                var oRServicio = await servicio.UpdateBitacoraBusquedaError(oRespuestaApi, idTransaccion, output, 4);
                return Content(HttpStatusCode.OK, oRServicio);
            }


        }
        [Route("PagarRemesa")]
        [HttpPost]
        public async Task<IHttpActionResult> PagarRemesa(PagoRemesa pagoRemesa)
        {

            RespuestaPago oRespuesta = new RespuestaPago();

            /**
             * a continuacion se valida que el modelo rescibido cumpla con los requerimientos establecidos en el modelo PagoRemesa
             * a este modelo se le colocaron validaciones en data anotation para obligar que todos sean requeridos
             */

            if (!ModelState.IsValid)
            {
                var entradasErroneas = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => x.Key.Split('.').Last())
                    .ToList();
                //return BadRequest($"Los siguientes parámetros están vacíos o nulos: {string.Join(",", entradasErroneas)}");

                /*objeto a retornar*/
                oRespuesta.Codigo = 101;
                oRespuesta.Descripcion = $"EXISTEN PARAMETROS VACIOS O NULOS: {string.Join(",", entradasErroneas)}";
                oRespuesta.IdCooitza = 0;
                return Content(HttpStatusCode.OK, oRespuesta);
            }
            using (var db = new cooitzacoreEntities())
            {
                if (await Login(pagoRemesa.usuarioWebService, pagoRemesa.claveWebService, pagoRemesa.token))
                {
                    /*aqui se genera el token*/
                }
                else
                {
                    oRespuesta.Codigo = 500;
                    oRespuesta.IdCooitza = 0;
                    oRespuesta.Descripcion = "CREDENCIALES NO VALIDAS";
                    return Content(HttpStatusCode.OK, oRespuesta);
                }
            }
            /**
            * procede a validar que el idrelationship sea valido y si es valido lo asigana a la variable relationship
            */
            CatRelationshipRemitente oCatRelationshipRemitentes = new CatRelationshipRemitente();
            using (var db = new cooitzacoreEntities())
            {
                oCatRelationshipRemitentes = await db.CatRelationshipRemitentes.FirstOrDefaultAsync(x => x.idCatRelationshipRemitentes == pagoRemesa.idRelacionRemitente);
                if (oCatRelationshipRemitentes == null)
                {
                    oRespuesta.Codigo = 8850;
                    oRespuesta.IdCooitza = 0;
                    oRespuesta.Descripcion = "EL idRelacionRemitente NO ES VALIDO CONSULTE EL CATOLOGO DE RELATIONSHIP";
                    return Content(HttpStatusCode.OK, oRespuesta);
                }
            }
            string relationship = oCatRelationshipRemitentes.descripcion;
            /**
            * procede a validar que el idTipoRemesa sea valido y si es valido lo asigana a la variable idTipoRemesa
            */
            CatTiposRemesa oCatTiposRemesas = new CatTiposRemesa();
            using (var db = new cooitzacoreEntities())
            {
                oCatTiposRemesas = await db.CatTiposRemesas.FirstOrDefaultAsync(x => x.idCatTiposRemesas == pagoRemesa.idTipoRemesa);
                if (oCatTiposRemesas == null)
                {
                    oRespuesta.Codigo = 8851;
                    oRespuesta.IdCooitza = 0;
                    oRespuesta.Descripcion = "EL idTipoRemesa NO ES VALIDO CONSULTE EL CATOLOGO DE TIPOS DE REMESAS";
                    return Content(HttpStatusCode.OK, oRespuesta);
                }
            }
            string idTipoRemesa = oCatTiposRemesas.codigo;
            /**
             * procede a buscar en la tabla de transacciones el id que brinda el usuario 
             */
            var oTransaccion = new TblTransaccionesRch();
            using (var db = new cooitzacoreEntities())
            {
                oTransaccion = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == pagoRemesa.idCooitza);
            }
            /**
             *si el objeto que se retorna de la busqueda es null es porque no existe registros para ese id
             */
            if (oTransaccion == null)
            {
                oRespuesta.Codigo = 8860;
                oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL INTENTAR RECUPERAR LOS DATOS DE LA REMESA IdCooitza {pagoRemesa.idCooitza} NO ES VALIDO";

                return Content(HttpStatusCode.OK, oRespuesta);
            }
            /**A continuacion insertamos el json que genera el usuario y lo guardamos en la base de datos 
             * se utilizo un using para evitar que ocacione problemas en el flujo de la transaccion 
             */
            using (var db = new cooitzacoreEntities())
            {
                var _oTRequest = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == pagoRemesa.idCooitza);

                if (_oTRequest == null)
                {
                    oRespuesta.Codigo = 8860;
                    oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL INTENTAR RECUPERAR LOS DATOS DE LA REMESA IdCooitza {pagoRemesa.idCooitza} NO ES VALIDO";
                    return Content(HttpStatusCode.OK, oRespuesta);
                }
                var _anulacionRequest = new StringContent(JsonConvert.SerializeObject(pagoRemesa), Encoding.UTF8, "application/json");
                string _anulacionRequestJsonStr = await _anulacionRequest.ReadAsStringAsync();
                _oTRequest.pagoRequestJson = _anulacionRequestJsonStr;
                db.Entry(_oTRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            /**
             * A continuacion se valida que 
             */

            var coincidenNombresYApellidos = pagoRemesa.benPrimerNombre.ToUpper() == oTransaccion.benPrimerNombre.ToUpper() &&
                                 pagoRemesa.benPrimerApellido.ToUpper() == oTransaccion.benPrimerApellido.ToUpper();

            if (!coincidenNombresYApellidos)
            {
                RespuestaExecutePagoApi result = new RespuestaExecutePagoApi();
                result.Codigo = 700;
                result.Descripcion = "EL NOMBRE Y APELLIDO NO COICIDEN CON LA REMESA";

                var oRespuestaRepo = await servicio.UpdateBitacoraPagoOk(null, result, pagoRemesa.idCooitza, pagoRemesa.usuarioTransaccion, false, null,null);//<--- se obtiene la respuesta del respositorio
                return Content(HttpStatusCode.OK, oRespuestaRepo);//<--- se obtiene la respuesta del respositorio

            }

            /**
             * para liberar la remesa es necesario pasar a ala funcion LiberaRemsa dentro de los paraemtros el id de reserva
             * pero debe ser el id de reserva mas reciente es decir  el ultimo id de reserva generado para ese numero de remesa
             * para ello se realiza la consulta que se mestra a continuacion
             */
            var resultado = (from r in db.TblTransaccionesRches
                             where r.idOperacion == pagoRemesa.numeroRemesa
                                   && r.idInterno == oTransaccion.idInterno
                                   && r.idReserva != null
                             orderby r.idTblTransaccionesRch descending
                             select r).FirstOrDefault();

            if (resultado == null)
            {
                RespuestaExecutePagoApi resp = new RespuestaExecutePagoApi();
                resp.Codigo = 8859;
                resp.Descripcion = "NO EXISTE REFERENCIA PARA REALIZAR EL PAGO CON LOS DATOS PROPORCIONADOS VERIFIQUE QUE LOS DATOS SEAN CORRECTOS";

                var oRespuestaRepo = await servicio.UpdateBitacoraPagoOk(null, resp, pagoRemesa.idCooitza, pagoRemesa.usuarioTransaccion, false, null,null);//<--- se obtiene la respuesta del respositorio
                return Content(HttpStatusCode.OK, oRespuestaRepo);//<--- se obtiene la respuesta del respositorio
            }

            /**
             * A continuacion validamos la propiedad oTransaccion.idCatEstadosTransacciones que nos indica 
             * el estado de la transaccion 
             * cuando una transaccion es buscada ingresa a la base de datos con valor 1 pero conforme se va 
             * avanzando en la transaccion este estado ira cambiando 
             * 
             * se valida en el caso 1 si el esta procesada esto quiere decir que no ha sufrido alguna modificacion 
             * si el caso 1 es exitoso entonces se valida el estado de la trnasaccion obtenido del metodo busqcar 
             * si el estado es 5 indica que la remesa esta reservada entonces se procede a liberar
             * si el estado es 13 indica que la remesa esta disponible entonces se procede a reservar
             */
            var oLibera = new RespuestaRepositorioLibera();
            var oReserva = new RespuestaRepositorioReserva();
            var respuestaRepositorioPago = new RespuestaRepositorioPago();
            var oResultUpdateRemesa = new RespuestaConsulta();
            switch (oTransaccion.idCatEstadosTransacciones)
            {
                case 1:
                    if (oTransaccion.idCatEstadosBusquedasRch == 5)//<--- reservada
                    {
                        oLibera = await LiberaRemesa(oTransaccion.idTblTransaccionesRch, oTransaccion.idInterno, resultado.idReserva, oTransaccion.idOperacion, pagoRemesa.usuarioTransaccion);

                        if (oLibera.Codigo == 1700)                     //<--- indica que la remesa se libero con exito
                        {
                            /**
                             * si el codigo el 1700 se libero la remesa con exito 
                             * entonce se procede a reservar nuevamente para despues ejecutar la funcion 
                             * executePago 
                             * 
                             */
                            oReserva = await ReservaRemesa(oTransaccion.idInterno, oTransaccion.idOperacion, oTransaccion.valorPagar, oTransaccion.idTblTransaccionesRch, oTransaccion.usuarioTransaccion);

                            if (oReserva.Codigo == 1600)/** si el bojeto retorna 1600 es porque la remesa se reservo correctamente */
                            {
                                /**Llamada a la funcion ExecutePago*/
                                respuestaRepositorioPago = await ExecutePago(pagoRemesa.usuarioTransaccion, oTransaccion.idTblTransaccionesRch, pagoRemesa, relationship, idTipoRemesa);
                                /**si el codigo ses distinto a 1800 se obtiene el codigo de error y la descripcion */
                                if (respuestaRepositorioPago.Codigo != 1800)
                                {
                                    oRespuesta.Codigo = 1890;
                                    oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL EJECUTAR EL PAGO {respuestaRepositorioPago.Descripcion.ToUpper()}";
                                    oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                                }
                                /**si el codigo ses correcto  1800 se obtiene el codigo de error y la descripcion */
                                oRespuesta.Codigo = respuestaRepositorioPago.Codigo;
                                oRespuesta.Descripcion = respuestaRepositorioPago.Descripcion.ToUpper();
                                oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                                oRespuesta.IdPago = respuestaRepositorioPago.CodigoPago;
                                return Content(HttpStatusCode.OK, oRespuesta);
                            }
                            else
                            {
                                oRespuesta.Codigo = 1650;
                                oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RESERVAR LA REMESA: {oReserva.Descripcion}";
                                oRespuesta.IdCooitza = oReserva.idTblTransaccionesRch;
                                return Content(HttpStatusCode.OK, oRespuesta);
                            }

                        }
                        else
                        {
                            oRespuesta.Codigo = 1701;
                            oRespuesta.Descripcion = $"LA REMESA SE ENCUENTRA LIBERADA NO SE LIBERO: {oLibera.Descripcion}";
                            oRespuesta.IdCooitza = oLibera.idTblTransaccionesRch;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                    }                       /**disponible*/
                    else if (oTransaccion.idCatEstadosBusquedasRch == 13)/**Si la remesa se encuentra disponible unicamente se reserva */
                    {
                        oReserva = await ReservaRemesa(oTransaccion.idInterno, oTransaccion.idOperacion, oTransaccion.valorPagar, oTransaccion.idTblTransaccionesRch, oTransaccion.usuarioTransaccion);
                        if (oReserva.Codigo == 1600)/**si el objeto retorna 1600 es proque se reservo correctamente*/
                        {

                            /**Llamada a la funcion ExecutePago*/
                            respuestaRepositorioPago = await ExecutePago(pagoRemesa.usuarioTransaccion, oTransaccion.idTblTransaccionesRch, pagoRemesa, relationship, idTipoRemesa);
                            /**si el codigo ses distinto a 1800 se obtiene el codigo de error y la descripcion */
                            if (respuestaRepositorioPago.Codigo != 1800)
                            {
                                oRespuesta.Codigo = 1890;
                                oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL EJECUTAR EL PAGO {respuestaRepositorioPago.Descripcion.ToUpper()}";
                                oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                            }
                            /**si el codigo ses correcto  1800 se obtiene el codigo de error y la descripcion */
                            oRespuesta.Codigo = respuestaRepositorioPago.Codigo;
                            oRespuesta.Descripcion = respuestaRepositorioPago.Descripcion.ToUpper();
                            oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                            oRespuesta.IdPago = respuestaRepositorioPago.CodigoPago;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                        else
                        {
                            oRespuesta.Codigo = 1650;
                            oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RESERVAR LA REMESA: {oReserva.Descripcion}";
                            oRespuesta.IdCooitza = oReserva.idTblTransaccionesRch;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }

                    }
                    else
                    {
                        /**
                         * A continuacion se valida que el id idCatEstadosBusquedasRch sea distinto de 0 
                         * porque se descoce el estado de la busqueda
                         * entonces se busca en la base de datos el estado 
                         * y como no se reservo no se libero, se le establece el idCooitza como el el id que el mismo usuario envia en el request
                         */
                        if (oTransaccion.idCatEstadosBusquedasRch != 0)
                        {
                            /**se llama a la funcion update remesa pasandole el numero de remesa y el usuario de la transaccion para actualizar la remesa*/
                            oResultUpdateRemesa = await UpdateRemesa(pagoRemesa.numeroRemesa, pagoRemesa.usuarioTransaccion);
                            /**segun el nuevo estado obtenido se busca de nuevo el estado en la base de datos*/
                            var oResult = await db.CatEstadosBusquedasRches.FirstOrDefaultAsync(x => x.descripcion.ToLower() == oResultUpdateRemesa.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO.ToLower());

                            if (oResult == null)
                            {
                                oRespuesta.Codigo = 1891;
                                oRespuesta.Descripcion = $"IMPOSIBLE PAGAR LA REMESA ESTADO DESCONOCIDO";
                                oRespuesta.IdCooitza = pagoRemesa.idCooitza;
                                return Content(HttpStatusCode.OK, oRespuesta);
                            }
                            oRespuesta.Codigo = 1892;
                            oRespuesta.Descripcion = $"LA REMESA NO DISPONIBLE PARA PAGO {oResult.descripcion}";
                            oRespuesta.IdCooitza = pagoRemesa.idCooitza;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                        else
                        {
                            oRespuesta.Codigo = 802;
                            oRespuesta.Descripcion = $"IMPOSIBLE RESOLVER EL ESTADO DE LA REMESA";
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                    }
                case 2:
                    /**
                     * validamos para el csaso 2 si el estado de la transaccion se enceuntra en reservada
                     * (esto puede suceder en caso de que la remesa si se reservo pero no se cobro)
                     * entonces procede a liberarse la remesa
                     */
                    //var resultado = (from r in db.TblTransaccionesRchTests
                    //                 where r.idOperacion == pagoRemesa.numeroRemesa
                    //                       && r.idInterno == oTransaccion.idInterno
                    //                       && r.idReserva != null
                    //                 orderby r.idTblTransaccionesRchTest descending
                    //                 select r).FirstOrDefault();

                    oLibera = await LiberaRemesa(oTransaccion.idTblTransaccionesRch, oTransaccion.idInterno, resultado.idReserva, oTransaccion.idOperacion, pagoRemesa.usuarioTransaccion);

                    if (oLibera.Codigo == 1700)/**indica que la remesa se libero con exito procedemos a reservar nuvemente*/
                    {
                        oReserva = await ReservaRemesa(oTransaccion.idInterno, oTransaccion.idOperacion, oTransaccion.valorPagar, oTransaccion.idTblTransaccionesRch, oTransaccion.usuarioTransaccion);
                        if (oReserva.Codigo == 1600)/**si el objeto retorna 1600 es proque se reservo correctamente*/
                        {
                            /**Llamada a la funcion ExecutePago*/
                            respuestaRepositorioPago = await ExecutePago(pagoRemesa.usuarioTransaccion, oTransaccion.idTblTransaccionesRch, pagoRemesa, relationship, idTipoRemesa);
                            /**si el codigo ses distinto a 1800 se obtiene el codigo de error y la descripcion */
                            if (respuestaRepositorioPago.Codigo != 1800)
                            {
                                oRespuesta.Codigo = 1890;
                                oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL EJECUTAR EL PAGO {respuestaRepositorioPago.Descripcion.ToUpper()}";
                                oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                            }
                            /**si el codigo ses correcto  1800 se obtiene el codigo de error y la descripcion */
                            oRespuesta.Codigo = respuestaRepositorioPago.Codigo;
                            oRespuesta.Descripcion = respuestaRepositorioPago.Descripcion.ToUpper();
                            oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                            oRespuesta.IdPago = respuestaRepositorioPago.CodigoPago;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                        else
                        {
                            oRespuesta.Codigo = 1650;
                            oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RESERVAR LA REMESA: {oReserva.Descripcion}";
                            oRespuesta.IdCooitza = oReserva.idTblTransaccionesRch;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                    }
                    else
                    {
                        oRespuesta.Codigo = 1701;
                        oRespuesta.Descripcion = $"LA REMESA NO SE LIBERO: {oLibera.Descripcion}";
                        oRespuesta.IdCooitza = oLibera.idTblTransaccionesRch;
                        return Content(HttpStatusCode.OK, oRespuesta);
                    }

                case 5:
                    /**
                    * validamos para el csaso  si el estado de la transaccion se encuentra liberada
                    * (esto puede suceder en caso de que la remesa si se reservo y luego se libero pero no se cobro)
                    * entonces prcede a reservarse  de nuevo
                    */
                    oReserva = await ReservaRemesa(oTransaccion.idInterno, oTransaccion.idOperacion, oTransaccion.valorPagar, oTransaccion.idTblTransaccionesRch, oTransaccion.usuarioTransaccion);

                    if (oReserva.Codigo == 1600)
                    {
                        /**Llamada a la funcion ExecutePago*/
                        respuestaRepositorioPago = await ExecutePago(pagoRemesa.usuarioTransaccion, oTransaccion.idTblTransaccionesRch, pagoRemesa, relationship, idTipoRemesa);
                        /**si el codigo ses distinto a 1800 se obtiene el codigo de error y la descripcion */
                        if (respuestaRepositorioPago.Codigo != 1800)
                        {
                            oRespuesta.Codigo = 1890;
                            oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL EJECUTAR EL PAGO {respuestaRepositorioPago.Descripcion.ToUpper()}";
                            oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                        }
                        /**si el codigo ses correcto  1800 se obtiene el codigo de error y la descripcion */
                        oRespuesta.Codigo = respuestaRepositorioPago.Codigo;
                        oRespuesta.Descripcion = respuestaRepositorioPago.Descripcion.ToUpper();
                        oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                        oRespuesta.IdPago = respuestaRepositorioPago.CodigoPago;
                        return Content(HttpStatusCode.OK, oRespuesta);
                    }
                    else
                    {
                        oRespuesta.Codigo = 1650;
                        oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RESERVAR LA REMESA: {oReserva.Descripcion}";
                        oRespuesta.IdCooitza = oReserva.idTblTransaccionesRch;
                        return Content(HttpStatusCode.OK, oRespuesta);
                    }
                case 4:
                    /**
                    * validamos el caso 4 en case de que la remesa se encuentre anulada 
                    * (si la remesa se anulo indica que la remesa vuelve a estar disponible)
                    * entonces tratamos a la remesa como si fuera el estado 1 que indica que la remesa esta procesada
                    * por lo mismo necesitamos validar el estado de la busqueda 
                    */
                    /**reservada*/
                    if (oTransaccion.idCatEstadosBusquedasRch == 5)//<---si  reservada
                    {
                        /**
                         * para liberar la remesa es necesario pasar a ala funcion LiberaRemsa dentro de los paraemtros el id de reserva
                         * pero debe ser el id de reserva mas reciente es decir  el ultimo id de reserva generado para ese numero de remesa
                         * para ello se realiza la consulta que se mestra a continuacion
                         */
                        //var resultado = (from r in db.TblTransaccionesRchTests
                        //                 where r.idOperacion == pagoRemesa.numeroRemesa
                        //                       && r.idInterno == oTransaccion.idInterno
                        //                       && r.idReserva != null
                        //                 orderby r.idTblTransaccionesRchTest descending
                        //                 select r).FirstOrDefault();

                        oLibera = await LiberaRemesa(oTransaccion.idTblTransaccionesRch, oTransaccion.idInterno, resultado.idReserva, oTransaccion.idOperacion, pagoRemesa.usuarioTransaccion);

                        if (oLibera.Codigo == 1700)                     //<--- indica que la remesa se libero con exito
                        {

                            /**
                             * si el codigo el 1700 se libero la remesa con exito 
                             * entonce se procede a reservar nuevamente para despues ejecutar la funcion 
                             * executePago 
                             * 
                             */
                            oReserva = await ReservaRemesa(oTransaccion.idInterno, oTransaccion.idOperacion, oTransaccion.valorPagar, oTransaccion.idTblTransaccionesRch, oTransaccion.usuarioTransaccion);

                            if (oReserva.Codigo == 1600)/** si el bojeto retorna 1600 es porque la remesa se reservo correctamente */
                            {
                                /**Llamada a la funcion ExecutePago*/
                                respuestaRepositorioPago = await ExecutePago(pagoRemesa.usuarioTransaccion, oTransaccion.idTblTransaccionesRch, pagoRemesa, relationship, idTipoRemesa);
                                /**si el codigo ses distinto a 1800 se obtiene el codigo de error y la descripcion */
                                if (respuestaRepositorioPago.Codigo != 1800)
                                {
                                    oRespuesta.Codigo = 1890;
                                    oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL EJECUTAR EL PAGO {respuestaRepositorioPago.Descripcion.ToUpper()}";
                                    oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                                }
                                /**si el codigo ses correcto  1800 se obtiene el codigo de error y la descripcion */
                                oRespuesta.Codigo = respuestaRepositorioPago.Codigo;
                                oRespuesta.Descripcion = respuestaRepositorioPago.Descripcion.ToUpper();
                                oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                                oRespuesta.IdPago = respuestaRepositorioPago.CodigoPago;
                                return Content(HttpStatusCode.OK, oRespuesta);
                            }
                            else
                            {
                                oRespuesta.Codigo = 1650;
                                oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RESERVAR LA REMESA: {oReserva.Descripcion}";
                                oRespuesta.IdCooitza = oReserva.idTblTransaccionesRch;
                                return Content(HttpStatusCode.OK, oRespuesta);
                            }
                        }
                        else
                        {
                            oRespuesta.Codigo = 1701;
                            oRespuesta.Descripcion = $"LA REMESA NO SE LIBERO: {oLibera.Descripcion}";
                            oRespuesta.IdCooitza = oLibera.idTblTransaccionesRch;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                    }                       /**disponible*/
                    else if (oTransaccion.idCatEstadosBusquedasRch == 13)
                    {
                        oReserva = await ReservaRemesa(oTransaccion.idInterno, oTransaccion.idOperacion, oTransaccion.valorPagar, oTransaccion.idTblTransaccionesRch, oTransaccion.usuarioTransaccion);

                        if (oReserva.Codigo == 1600)
                        {

                            /**Llamada a la funcion ExecutePago*/
                            respuestaRepositorioPago = await ExecutePago(pagoRemesa.usuarioTransaccion, oTransaccion.idTblTransaccionesRch, pagoRemesa, relationship, idTipoRemesa);
                            /**si el codigo ses distinto a 1800 se obtiene el codigo de error y la descripcion */
                            if (respuestaRepositorioPago.Codigo != 1800)
                            {
                                oRespuesta.Codigo = 1890;
                                oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL EJECUTAR EL PAGO {respuestaRepositorioPago.Descripcion.ToUpper()}";
                                oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                            }
                            /**si el codigo ses correcto  1800 se obtiene el codigo de error y la descripcion */
                            oRespuesta.Codigo = respuestaRepositorioPago.Codigo;
                            oRespuesta.Descripcion = respuestaRepositorioPago.Descripcion.ToUpper();
                            oRespuesta.IdCooitza = respuestaRepositorioPago.IdCooitza;
                            oRespuesta.IdPago = respuestaRepositorioPago.CodigoPago;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                        else
                        {
                            oRespuesta.Codigo = 1650;
                            oRespuesta.Descripcion = $"OCURRIO UN PROBLEMA AL RESERVAR LA REMESA: {oReserva.Descripcion}";
                            oRespuesta.IdCooitza = oReserva.idTblTransaccionesRch;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }

                    }
                    else
                    {
                        /**
                         * A continuacion se valida que el id idCatEstadosBusquedasRch sea distinto de 0 
                         * porque se descoce el estado de la busqueda
                         * entonces se busca en la base de datos el estado 
                         * y como no se reservo ni se libero se le establece el idCooitza como el el id que el mismo usuario envia en el request
                         */
                        if (oTransaccion.idCatEstadosBusquedasRch != 0)
                        {
                            oResultUpdateRemesa = await UpdateRemesa(pagoRemesa.numeroRemesa, pagoRemesa.usuarioTransaccion);
                            var oResult = await db.CatEstadosBusquedasRches.FirstOrDefaultAsync(x => x.descripcion.ToLower() == oResultUpdateRemesa.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO.ToLower());
                            if (oResult == null)
                            {
                                oRespuesta.Codigo = 1891;
                                oRespuesta.Descripcion = $"IMPOSIBLE PAGAR LA REMESA ESTADO DESCONOCIDO";
                                oRespuesta.IdCooitza = pagoRemesa.idCooitza;
                                return Content(HttpStatusCode.OK, oRespuesta);
                            }
                            oRespuesta.Codigo = 1892;
                            oRespuesta.Descripcion = $"LA REMESA NO DISPONIBLE PARA PAGO {oResult.descripcion}";
                            oRespuesta.IdCooitza = pagoRemesa.idCooitza;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                        else
                        {
                            oRespuesta.Codigo = 802;
                            oRespuesta.Descripcion = $"CWS NO LOGRO RESOLVER EL ESTADO DE LA REMESA";
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                    }
                default:
                    /**
                    * para este punto no sabes el estado de la transaccion que obiamente ya habremos descartado que este liberada o reservada o en su defecto anulada
                    * entonces cosnultamos el estado de la busqueda que de igual manera desconocemos
                    * y hacemos que se retorne en case de tener el codigo en la base de datos 
                    * su no se cuenta con el codigo entonces i se dispara el el erro no se pudo resolver el estado de la remesa
                    */
                    if (oTransaccion.idCatEstadosBusquedasRch != 0)
                    {
                        oResultUpdateRemesa = await UpdateRemesa(pagoRemesa.numeroRemesa, pagoRemesa.usuarioTransaccion);
                        var oResult = await db.CatEstadosBusquedasRches.FirstOrDefaultAsync(x => x.descripcion.ToLower() == oResultUpdateRemesa.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ConsultaGiroResponse.result.LISTADO.item.ESTADO_GIRO.ToLower());
                        if (oResult == null)
                        {
                            oRespuesta.Codigo = 1891;
                            oRespuesta.Descripcion = $"IMPOSIBLE PAGAR LA REMESA ESTADO DESCONOCIDO";
                            oRespuesta.IdCooitza = pagoRemesa.idCooitza;
                            return Content(HttpStatusCode.OK, oRespuesta);
                        }
                        oRespuesta.Codigo = 1892;
                        oRespuesta.Descripcion = $"LA REMESA NO DISPONIBLE PARA PAGO {oResult.descripcion}";
                        oRespuesta.IdCooitza = pagoRemesa.idCooitza;
                        return Content(HttpStatusCode.OK, oRespuesta);
                    }
                    else
                    {
                        oRespuesta.Codigo = 802;
                        oRespuesta.Descripcion = $"IMPOSIBLE RESOLVER EL ESTADO DE LA REMESA";
                        return Content(HttpStatusCode.OK, oRespuesta);
                    }
            }

            //if (oTransaccion.idCatEstadosTransacciones == 1)
            //{

            //}
            //else if (oTransaccion.idCatEstadosTransacciones == 2)/**si se encuentra reservada*/
            //{

            //}
            //else if (oTransaccion.idCatEstadosTransacciones == 5)/**si se encuentra liberada*/
            //{

            //}
            //else if (oTransaccion.idCatEstadosTransacciones == 4)/**si se encuentra anualda se aplical la metodologia como si estuiera libre y procesada*/
            //{

            //}
            //else
            //{

            //}
            //var s = await ReservaRemesa(oTransaccion.idInterno, oTransaccion.idOperacion, oTransaccion.valorPagar, oTransaccion.idTblTransaccionesRchTest, oTransaccion.usuarioTransaccion);


        }
        public async Task<RespuestaRepositorioPago> ExecutePago(string _usuarioTransaccion, long? idTblTrnsaccionesRch, PagoRemesa pagoRemesa, string relationship, string idTipoRemesa)
        {
            var tblTransacciones = db.TblTransaccionesRches.FirstOrDefault(x => x.idTblTransaccionesRch == idTblTrnsaccionesRch);
            RespuestaExecutePagoApi oRespuestaApi = new RespuestaExecutePagoApi();//obj de respuestas
            RespuestaRepositorioPago oRepoRespuestaPago = new RespuestaRepositorioPago();
            bool pago = false;
            var _configuracion = servicio.GetConfiguraciones();
            if (!_configuracion.Codigo)
            {
                oRepoRespuestaPago.Codigo = 750;
                oRepoRespuestaPago.Descripcion = "Login: No existen credenciales de acceso para el sistema";
                return oRepoRespuestaPago;
            }


            /**
             * A continuacion se obtiene la infromacion de la sucursal con la siguiente consulta
             */
            var sucursal = db.CatUbicaciones.FirstOrDefault(x => x.idSucursal == tblTransacciones.idSucursal);
            string codigoNombreSucursal = sucursal == null ? string.Empty : sucursal.codigoNombre;
            /**
            * A continuacion se generan las variables para la hora y fecha de la transaccion
            */
            DateTime fechaHoraActual = DateTime.Now;
            string fechaTransaccion = fechaHoraActual.ToString("yyyy-MM-dd");
            string horaTransaccion = fechaHoraActual.ToString("HH:mm:ss");
            /**
            * El siguiente codigo maneja la peticion para el pago de remesas
            */
            /**dentro de la peticion se envia el numero de telefono obtenido con el metodo buscar transaccion*/
            var client = new RestClient("https://portal.redchapina.com");
            var request = new RestRequest("/Pagador.php", Method.Post);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://tempuri.org/ITransactionService/ExecuteTransaction");
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:por=""https://portal.redchapina.com/"">
            " + "\n" +
                        @"   <soapenv:Header/>
            " + "\n" +
                        @"   <soapenv:Body>
            " + "\n" +
                        @"      <por:PagoVersionTres>
            " + "\n" +
                        @"         <registro>
            " + "\n" +
                        $"            <ID_INTEGRACION>{_configuracion.IntegracionId}</ID_INTEGRACION>" + @"
            " + "\n" +
                        $"            <ID_PAGADOR>{_configuracion.PagadorId}</ID_PAGADOR>" + @"
            " + "\n" +
                        @"            <USUARIO></USUARIO>
            " + "\n" +
                        $"            <ID_INTERNO>{tblTransacciones.idInterno}</ID_INTERNO>" + @"
            " + "\n" +
                        $"            <ID_OPERACION>{tblTransacciones.idOperacion}</ID_OPERACION>" + @"
            " + "\n" +
                        $"            <ID_RESERVA>{tblTransacciones.idReserva}</ID_RESERVA>" + @"
            " + "\n" +
                        $"            <MONEDA_PAGO>{tblTransacciones.monedaPago}</MONEDA_PAGO>" + @"
            " + "\n" +
                        $"            <VALOR_PAGAR>{tblTransacciones.valorPagar}</VALOR_PAGAR>" + @"
            " + "\n" +
                        $"            <BEN_PRIMER_NOMBRE>{pagoRemesa.benPrimerNombre}</BEN_PRIMER_NOMBRE>" + @"
            " + "\n" +
                        $"            <BEN_SEGUNDO_NOMBRE>{pagoRemesa.benSegundoNombre}</BEN_SEGUNDO_NOMBRE>" + @"
            " + "\n" +
                        $"            <BEN_PRIMER_APELLIDO>{pagoRemesa.benPrimerApellido}</BEN_PRIMER_APELLIDO>" + @"
            " + "\n" +
                        $"            <BEN_SEGUNDO_APELLIDO>{pagoRemesa.benSegundoApellido}</BEN_SEGUNDO_APELLIDO>" + @"
            " + "\n" +
                        $"            <BEN_PAIS_TELEFONO>{pagoRemesa.benPaisTelefono}</BEN_PAIS_TELEFONO>" + @"
            " + "\n" +
                        $"            <BEN_TELEFONO>{tblTransacciones.benTelefono}</BEN_TELEFONO>" + @"
            " + "\n" +
                        $"            <BEN_TIPO_IDENTIFICACION>{pagoRemesa.benTipoIdentificacion}</BEN_TIPO_IDENTIFICACION>" + @"
            " + "\n" +
                        $"            <BEN_NUMERO_IDENTIFICACION>{pagoRemesa.benNumeroIdentificacion}</BEN_NUMERO_IDENTIFICACION>" + @"
            " + "\n" +
                        $"            <BEN_PAIS_IDENTIFICACION>{pagoRemesa.benPaisIdentificacion}</BEN_PAIS_IDENTIFICACION>" + @"
            " + "\n" +
                        $"            <BEN_FECHA_EMISION_IDENTIFICACION>{pagoRemesa.benFechaEmisionIdentificacion}</BEN_FECHA_EMISION_IDENTIFICACION>" + @"
            " + "\n" +
                        $"            <BEN_FECHA_VENCIMIENTO_IDENTIFICACION>{pagoRemesa.benFechaVencimientoIdentificacion}</BEN_FECHA_VENCIMIENTO_IDENTIFICACION>" + @"
            " + "\n" +
                        $"            <FECHA_PAGO>{fechaTransaccion}</FECHA_PAGO>" + @"
            " + "\n" +
                        $"            <HORA_PAGO>{horaTransaccion}</HORA_PAGO>" + @"
            " + "\n" +
                        $"            <GENERO>{pagoRemesa.benGenero}</GENERO>" + @"
            " + "\n" +
                        $"            <ESTADO_CIVIL>{pagoRemesa.benEstadoCivil}</ESTADO_CIVIL>" + @"
            " + "\n" +
                        $"            <NACIONALIDAD>{pagoRemesa.benNacionalidad}</NACIONALIDAD>" + @"
            " + "\n" +
                        $"            <FECHA_NACIMIENTO>{pagoRemesa.benFechaNacimiento}</FECHA_NACIMIENTO>" + @"
            " + "\n" +
                        $"            <PAIS_NACIMIENTO>{pagoRemesa.benPaisNacimiento}</PAIS_NACIMIENTO>" + @"
            " + "\n" +
                        $"            <ESTADO_NACIMIENTO>{pagoRemesa.benEstadoNacimiento}</ESTADO_NACIMIENTO>" + @"
            " + "\n" +
                        $"            <LUGAR_NACIMIENTO>{pagoRemesa.benLugarNacimiento}</LUGAR_NACIMIENTO>" + @"
            " + "\n" +
                        $"            <DIRECCION>{pagoRemesa.benDireccion}</DIRECCION>" + @"
            " + "\n" +
                        $"            <BEN_CORREO_ELECTRONICO>{pagoRemesa.benCorreoElectronico}</BEN_CORREO_ELECTRONICO>" + @"
            " + "\n" +
                        $"            <OCUPACION>{pagoRemesa.benOcupacion}</OCUPACION>" + @"
            " + "\n" +
                        $"            <PAIS_RESIDENCIA>{pagoRemesa.benPaisResidencia}</PAIS_RESIDENCIA>" + @"
            " + "\n" +
                        $"            <MOTIVO_RECEPCION>{pagoRemesa.benMotivoRecepcion}</MOTIVO_RECEPCION>" + @"
            " + "\n" +
                        $"            <ACTIVIDAD_ECONOMICA>{pagoRemesa.benActividadEconomica}</ACTIVIDAD_ECONOMICA>" + @"
            " + "\n" +
                        $"            <INGRESOS_MENSUALES>{pagoRemesa.benIngresosMensuales}</INGRESOS_MENSUALES>" + @"
            " + "\n" +
                        $"            <EGRESOS_MENSUALES>{pagoRemesa.benEgresosMensuales}</EGRESOS_MENSUALES>" + @"
            " + "\n" +
                        $"            <CANTIDAD_REMESAS>{pagoRemesa.benCantidadRemesas}</CANTIDAD_REMESAS>" + @"
            " + "\n" +
                        $"            <RELACION_REMITENTE>{relationship}</RELACION_REMITENTE>" + @"
            " + "\n" +
                        $"            <TIPO_REMESA>{idTipoRemesa}</TIPO_REMESA>" + @"
            " + "\n" +
                        $"            <VERIFICACION_TERCERO>{_configuracion.VerificacionTercero}</VERIFICACION_TERCERO>" + @"
            " + "\n" +
                        $"            <VERIFICACION_AGENTE>{_configuracion.VerificacionAgente}</VERIFICACION_AGENTE>" + @"
            " + "\n" +
                        $"            <LOCAL_RESERVA>{codigoNombreSucursal}</LOCAL_RESERVA>" + @"
            " + "\n" +
                        $"            <CAJA_RESERVA>{_usuarioTransaccion}</CAJA_RESERVA>" + @"
            " + "\n" +
                        @"         </registro>
            " + "\n" +
                        @"      </por:PagoVersionTres>
            " + "\n" +
                        @"   </soapenv:Body>
            " + "\n" +
                        @"</soapenv:Envelope>";

            request.AddParameter("text/xml", body, ParameterType.RequestBody);

            var json_respuesta = await client.ExecuteAsync(request);

            /**
            * A continuacion se asignan las variables output y bodu a las propiedades del 
            * objeto que se enviara como argumento al respositorio mas adelante
            */
            /**
             * A continuacion se obtiene el contenido de la variable  json_respuesta 
             */
            string output = json_respuesta.Content;
            oRespuestaApi.XmlResponse = output;
            oRespuestaApi.XmlRequest = body;
            if (json_respuesta.StatusCode != HttpStatusCode.OK)
            {

                oRespuestaApi.Codigo = 751;
                oRespuestaApi.Descripcion = $"ERROR PETICION NO EXITOSA COOITZA - RCH {json_respuesta.StatusDescription}";
                pago = false;
                oRepoRespuestaPago = await servicio.UpdateBitacoraPagoOk(null, oRespuestaApi, idTblTrnsaccionesRch, _usuarioTransaccion, pago,null,null);//<--- se obtiene la respuesta del respositorio
                return oRepoRespuestaPago;

                /**
                * A continuacion se valida que ña varible output no se encuentre vacia 
                */
            }
            if (string.IsNullOrEmpty(output))
            {
                oRespuestaApi.Codigo = 699;
                oRespuestaApi.Descripcion = "EL SERVICIO RED CHAPINA NO DEVOLVIO CONTENIDO DENTRO DE SU RESPUESTA ESTATUS 200 OK";
                pago = false;
                oRepoRespuestaPago = await servicio.UpdateBitacoraPagoOk(null, oRespuestaApi, idTblTrnsaccionesRch, _usuarioTransaccion, pago, null, null);//<--- se obtiene la respuesta del respositorio
            }
            /**
             * A continuacion se reemplazan los caracteres especiales por caracteres validos dentro de la cadena  output
             */
            var salida = output.Replace(":", "_").Replace("-", "_");
            /**
             * A continuacion se pasa la variable salida tratada anteriormente a la funcion Json.Xml.ToJson como argumento
             */
            string json = JSon.XmlToJSON(salida);
            /**
             * A continuacion se valida que la variable resultado pueda ser serealizada con exito 
             * si existe algun problema durante la serelaizacion se captura y se pasa al repositorio 
             * para grabar la bitacora
             */
            ResponsesPago resultado = new ResponsesPago();
            try
            {
                resultado = JsonConvert.DeserializeObject<ResponsesPago>(json);//<--- resultado xml serealizado en json
            }
            catch (JsonSerializationException ex)
            {
                oRespuestaApi.Codigo = 310;                         //<--- se esablece el codigo 11 no disponible                    //<--- se establce el id con 0, el servicio lo constuyr
                oRespuestaApi.Descripcion = $"IMPOSIBLE RESOLVER LA ESTRUCTURA DE DATOS PARA EL PAGO  DETALLE : {ex.Message}";//<--- se concatena la descripcion con la que retorna el servicio
                oRepoRespuestaPago = await servicio.UpdateBitacoraPagoOk(resultado, oRespuestaApi, idTblTrnsaccionesRch, _usuarioTransaccion, pago, null, null);

            }

            /**
             * A continuacion se valida que la propiedad dentro del condicional que vemos a continuacion no se encuentre vacia
             */
            if (string.IsNullOrEmpty(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_PagoVersionTresResponse.result.CODIGO_MENSAJE))
            {
                oRespuestaApi.Codigo = 311;
                oRespuestaApi.Descripcion = $"OCURRIO UN PROBLEMA EL SERVICIO RED CHAPINA NO DEVOLVIO UN CODIGO DE REFERENCIA VALIDO";
                pago = false;
                oRepoRespuestaPago = await servicio.UpdateBitacoraPagoOk(null, oRespuestaApi, idTblTrnsaccionesRch, _usuarioTransaccion, pago, null, null);
                return oRepoRespuestaPago;

            }
            /**
             * A continuacion se valida que el codigo sea 1800 lo que inidca un esdo pagada de la remesa
             * si no se obtinene el codigo y la descripcion del metodo 
             */
            if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_PagoVersionTresResponse.result.CODIGO_MENSAJE == "1800")
            {
                oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_PagoVersionTresResponse.result.CODIGO_MENSAJE);
                oRespuestaApi.Descripcion = "REMESA PAGADA CON EXITO";
                oRespuestaApi.CodigoPago = resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_PagoVersionTresResponse.result.ID_PAGO;//<--- se asigna el codigo de la remesa 
                pago = true;
                oRepoRespuestaPago = await servicio.UpdateBitacoraPagoOk(resultado, oRespuestaApi, idTblTrnsaccionesRch, _usuarioTransaccion, pago, pagoRemesa.asociado, pagoRemesa.numeroCuentaCooitza);
            }
            else
            {
                oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_PagoVersionTresResponse.result.CODIGO_MENSAJE);
                oRespuestaApi.Descripcion = $"OCURRIO UN PROBLEMA DURANTE EL PAGO DE LA REMESA: {(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_PagoVersionTresResponse.result.TEXTO_MENSAJE).ToUpper()}";
                pago = false;
                oRepoRespuestaPago = await servicio.UpdateBitacoraPagoOk(resultado, oRespuestaApi, idTblTrnsaccionesRch, _usuarioTransaccion, pago, null, null);
            }
            return oRepoRespuestaPago;

        }
        public async Task<RespuestaRepositorioReserva> ReservaRemesa(string idInterno, string numeroRemesa, string valorPagar, long? idTblTransaccionesRch, string _usuarioTransaccion)
        {

            RespuestaReservaApi oRespuestaApi = new RespuestaReservaApi();//obj de respuestas
            RespuestaRepositorioReserva oRepoRespuestaReserva = new RespuestaRepositorioReserva();
            var _configuracion = servicio.GetConfiguraciones();
            if (!_configuracion.Codigo)
            {
                oRepoRespuestaReserva.Codigo = 750;
                oRepoRespuestaReserva.Descripcion = "Login: No existen credenciales de acceso para el sistema";
                return oRepoRespuestaReserva;
            }

            var client = new RestClient("https://portal.redchapina.com");

            var request = new RestRequest("/Pagador.php", Method.Post);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://tempuri.org/ITransactionService/ExecuteTransaction");
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:por=""https://portal.redchapina.com/"">
            " + "\n" +
                        @"   <soapenv:Header/>
            " + "\n" +
                        @"   <soapenv:Body>
            " + "\n" +
                        @"      <por:Reserva>
            " + "\n" +
                        @"         <registro>
            " + "\n" +
                        $"            <ID_INTEGRACION>{_configuracion.IntegracionId}</ID_INTEGRACION>" + @"
            " + "\n" +
                        $"            <USUARIO>{_usuarioTransaccion}</USUARIO>" + @"
            " + "\n" +
                        $"            <ID_INTERNO>{idInterno}</ID_INTERNO>" + @"
            " + "\n" +
                        $"            <ID_OPERACION>{numeroRemesa}</ID_OPERACION>" + @"
            " + "\n" +
                        $"            <ID_PAGADOR>{_configuracion.PagadorId}</ID_PAGADOR>" + @"
            " + "\n" +
                        @"            <CORRELATIVO_ID></CORRELATIVO_ID>
            " + "\n" +
                        $"            <VALOR_PAGAR>{valorPagar}</VALOR_PAGAR>" + @"
            " + "\n" +
                        @"            <LOCAL_RESERVA></LOCAL_RESERVA>
            " + "\n" +
                        @"            <CAJA_RESERVA></CAJA_RESERVA>
            " + "\n" +
                        @"            <CAJERO_RESERVA></CAJERO_RESERVA>
            " + "\n" +
                        @"         </registro>
            " + "\n" +
                        @"      </por:Reserva>
            " + "\n" +
                        @"   </soapenv:Body>
            " + "\n" +
                        @"</soapenv:Envelope>";

            request.AddParameter("text/xml", body, ParameterType.RequestBody);

            var json_respuesta = await client.ExecuteAsync(request);
            if (json_respuesta.StatusCode != HttpStatusCode.OK)
            {
                oRepoRespuestaReserva.Codigo = 751;
                oRepoRespuestaReserva.Descripcion = $"ERROR RESPUESTA NO EXITOSA {json_respuesta.ErrorMessage}";
                return oRepoRespuestaReserva;
            }
            string output = json_respuesta.Content;
            bool reserva = false;//<--- variable para almacenar si la remesa esta reservada o no 
            if (!string.IsNullOrEmpty(output))
            {
                var salida = output.Replace(":", "_").Replace("-", "_");
                string json = JSon.XmlToJSON(salida);


                var resultado = JsonConvert.DeserializeObject<ResponseReserva>(json);//<--- resultado xml serealizado en json 
                oRespuestaApi.XmlResponse = output;
                oRespuestaApi.XmlRequest = body;

                if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE == "9993")//<--- la remesa ya se encuentra reservada
                {
                    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE);
                    oRespuestaApi.Descripcion = "LA REMESA SE ENCUENTRA RESERVADA";
                    reserva = false;
                }
                else if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE == "1600")
                {
                    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE);
                    oRespuestaApi.Descripcion = "REMESA RESERVADA CON EXITO";
                    oRespuestaApi.CodigoReserva = resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.ID_RESERVA;//<--- se asigna el codigo de la remesa 
                    reserva = true;
                }
                else
                {
                    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE);
                    oRespuestaApi.Descripcion = "EL SERVICIO RED CHAPINA DEVOLVIO UN CODIGO DESCONOCIDO PARA LA RESERVA DE LA REMESA";
                    reserva = false;
                }
                //oRespuestaApi.JsonResponse = _jsonRespuestaStr;

                oRepoRespuestaReserva = await servicio.UpdateBitacoraReservaOk(resultado, oRespuestaApi, idTblTransaccionesRch, _usuarioTransaccion, reserva);//<--- se obtiene la respuesta del respositorio

            }
            else
            {
                oRespuestaApi.Codigo = 699;
                oRespuestaApi.Descripcion = "EL SERVICIO RED CHAPINA NO DEVOLVIO CONTENIDO DENTRO DE SU RESPUESTA ESTATUS 200 OK";
                reserva = false;
                oRepoRespuestaReserva = await servicio.UpdateBitacoraReservaOk(null, oRespuestaApi, idTblTransaccionesRch, _usuarioTransaccion, reserva);//<--- se obtiene la respuesta del respositorio
            }
            return oRepoRespuestaReserva;
        }
        public async Task<RespuestaRepositorioLibera> LiberaRemesa(long? idTblTransaccionesRch, string idInterno, string idReserva, string numeroRemesa, string usuarioTransaccion)
        {

            RespuestaLiberaApi oRespuestaApi = new RespuestaLiberaApi();//obj de respuestas
            RespuestaRepositorioLibera oRepoRespuestaReserva = new RespuestaRepositorioLibera();
            var _configuracion = servicio.GetConfiguraciones();
            if (!_configuracion.Codigo)
            {
                oRepoRespuestaReserva.Codigo = 3;
                oRepoRespuestaReserva.Descripcion = "Login: No existen credenciales de acceso para el sistema";
                return oRepoRespuestaReserva;
            }

            var client = new RestClient("https://portal.redchapina.com");
            var request = new RestRequest("/Pagador.php", Method.Post);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://tempuri.org/ITransactionService/ExecuteTransaction");
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:por=""https://portal.redchapina.com/"">
                        " + "\n" +
                                    @"   <soapenv:Header/>
                        " + "\n" +
                                    @"   <soapenv:Body>
                        " + "\n" +
                                    @"      <por:Libera>
                        " + "\n" +
                                    @"         <registro>
                        " + "\n" +
                                    $"            <ID_INTEGRACION>{_configuracion.IntegracionId}</ID_INTEGRACION>" + @"
                        " + "\n" +
                                    $"            <USUARIO>{usuarioTransaccion}</USUARIO>" + @"
                        " + "\n" +
                                    $"            <ID_INTERNO>{idInterno}</ID_INTERNO>" + @"
                        " + "\n" +
                                    $"            <ID_PAGADOR>{_configuracion.PagadorId}</ID_PAGADOR>" + @"
                        " + "\n" +
                                    $"            <ID_RESERVA>{idReserva}</ID_RESERVA>" + @"
                        " + "\n" +
                                    $"            <ID_OPERACION>{numeroRemesa}</ID_OPERACION>" + @"
                        " + "\n" +
                                    @"            <CORRELATIVO_ID></CORRELATIVO_ID>
                        " + "\n" +
                                    @"            <VALOR_PAGAR></VALOR_PAGAR>
                        " + "\n" +
                                    @"         </registro>
                        " + "\n" +
                                    @"      </por:Libera>
                        " + "\n" +
                                    @"   </soapenv:Body>
                        " + "\n" +
                                    @"</soapenv:Envelope>";

            request.AddParameter("text/xml", body, ParameterType.RequestBody);

            var json_respuesta = await client.ExecuteAsync(request);
            if (json_respuesta.StatusCode != HttpStatusCode.OK)
            {
                oRepoRespuestaReserva.Codigo = 0;
                oRepoRespuestaReserva.Descripcion = $"ERROR RESPUESTA NO EXITOSA {json_respuesta.ErrorMessage}";
                return oRepoRespuestaReserva;
            }
            string output = json_respuesta.Content;
            //bool reserva = false;//<--- variable para almacenar si la remesa esta reservada o no 
            if (!string.IsNullOrEmpty(output))
            {
                var salida = output.Replace(":", "_").Replace("-", "_");
                string json = JSon.XmlToJSON(salida);

                ResponseLibera resultado = new ResponseLibera();
                try
                {
                    resultado = JsonConvert.DeserializeObject<ResponseLibera>(json);//<--- resultado xml serealizado en json 
                }
                catch (JsonSerializationException ex)
                {

                    oRespuestaApi.Codigo = 310;                         //<--- se esablece el codigo 11 no disponible                    //<--- se establce el id con 0, el servicio lo constuyr
                    oRespuestaApi.Descripcion = $"IMPOSIBLE SEREALIZAR LA ESTRUCTURA DE DATOS ES DESCONOCIDA DETALLE : {ex.Message}";//<--- se concatena la descripcion con la que retorna el servicio
                }

                if (string.IsNullOrEmpty(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_LiberaResponse.result.CODIGO_MENSAJE))
                {
                    oRespuestaApi.Codigo = 302;                         //<--- se esablece el codigo 11 no disponible                    //<--- se establce el id con 0, el servicio lo constuyr
                    oRespuestaApi.Descripcion = "OCURRIO UN PROBLEMA EL SERVICIO RED CHAPINA NO PROPORCIONO UN CODIGO DE REFERENCIA PARA LA RESPUESTA";//<--- se concatena la descripcion con la que retorna el servicio
                }
                long? codigoRch = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_LiberaResponse.result.CODIGO_MENSAJE);
                var oCatEstadosLiberaciones = db.CatEstadosLiberacionesRches.FirstOrDefault(c => c.codigo == codigoRch);

                if (oCatEstadosLiberaciones == null)
                {
                    oRespuestaApi.Codigo = 303;
                    oRespuestaApi.Descripcion = "EL WEB SERVICE COOITZA CON ENCONTRO UN CODIGO DE REFERENCIA PARA EL CODIGO QUE RESPONDIO RED CHAPINA";
                }

                /*
                 * 
                 * si cumple con las anteriores validaciones resolvemos el 
                 * objeto para enviarlo al respostorio
                 */
                oRespuestaApi.XmlResponse = output;
                oRespuestaApi.XmlRequest = body;
                oRespuestaApi.Codigo = oCatEstadosLiberaciones.codigo;
                oRespuestaApi.Descripcion = resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_LiberaResponse.result.TEXTO_MENSAJE;
                oRepoRespuestaReserva = await servicio.UpdateBitacoraLiberaOk(resultado, oRespuestaApi, idTblTransaccionesRch, usuarioTransaccion);
                //oRepoRespuestaReserva = await servicio.UpdateBitacoraReservaOk(resultado, oRespuestaApi, idTblTransaccionesRch, _usuarioTransaccion, reserva);//<--- se obtiene la respuesta del respositorio
                //if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE == "9993")//<--- la remesa ya se encuentra reservada
                //{
                //    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE);
                //    oRespuestaApi.Descripcion = "LA REMESA SE ENCUENTRA RESERVADA";
                //    reserva = false;
                //}
                //else if (resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE == "1600")
                //{
                //    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE);
                //    oRespuestaApi.Descripcion = "REMESA RESERVADA CON EXITO";
                //    oRespuestaApi.CodigoReserva = resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.ID_RESERVA;//<--- se asigna el codigo de la remesa 
                //    reserva = true;
                //}
                //else
                //{
                //    oRespuestaApi.Codigo = Convert.ToInt64(resultado.SOAP_ENV_Envelope.SOAP_ENV_Body.ns1_ReservaResponse.result.CODIGO_MENSAJE);
                //    oRespuestaApi.Descripcion = "EL SERVICIO RED CHAPINA DEVOLVIO UN CODIGO DESCONOCIDO PARA LA RESERVA DE LA REMESA";
                //    reserva = false;
                //}
                //oRespuestaApi.JsonResponse = _jsonRespuestaStr;

                //oRepoRespuestaReserva = await servicio.UpdateBitacoraReservaOk(resultado, oRespuestaApi, idTblTransaccionesRch, _usuarioTransaccion, reserva);//<--- se obtiene la respuesta del respositorio

            }
            else
            {
                //oRespuestaApi.Codigo = 699;
                //oRespuestaApi.Descripcion = "EL SERVICIO RED CHAPINA NO DEVOLVIO CONTENIDO DENTRO DE SU RESPUESTA ESTATUS 200 OK";
                //reserva = false;
                //oRepoRespuestaReserva = await servicio.UpdateBitacoraReservaOk(null, oRespuestaApi, idTblTransaccionesRch, _usuarioTransaccion, reserva);//<--- se obtiene la respuesta del respositorio
            }
            return oRepoRespuestaReserva;
        }
        public static class JSon
        {
            public static string XmlToJSON(string xml)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                return XmlToJSON(doc);
            }
            public static string XmlToJSON(XmlDocument xmlDoc)
            {
                StringBuilder sbJSON = new StringBuilder();
                sbJSON.Append("{ ");
                XmlToJSONnode(sbJSON, xmlDoc.DocumentElement, true);
                sbJSON.Append("}");
                return sbJSON.ToString();
            }

            //  XmlToJSONnode:  Output an XmlElement, possibly as part of a higher array
            private static void XmlToJSONnode(StringBuilder sbJSON, XmlElement node, bool showNodeName)
            {
                if (showNodeName)
                    sbJSON.Append("\"" + SafeJSON(node.Name) + "\": ");
                sbJSON.Append("{");
                // Build a sorted list of key-value pairs
                //  where   key is case-sensitive nodeName
                //          value is an ArrayList of string or XmlElement
                //  so that we know whether the nodeName is an array or not.
                SortedList<string, object> childNodeNames = new SortedList<string, object>();

                //  Add in all node attributes
                if (node.Attributes != null)
                    foreach (XmlAttribute attr in node.Attributes)
                        StoreChildNode(childNodeNames, attr.Name, attr.InnerText);

                //  Add in all nodes
                foreach (XmlNode cnode in node.ChildNodes)
                {
                    if (cnode is XmlText)
                        StoreChildNode(childNodeNames, "value", cnode.InnerText);
                    else if (cnode is XmlElement)
                        StoreChildNode(childNodeNames, cnode.Name, cnode);
                }

                // Now output all stored info
                foreach (string childname in childNodeNames.Keys)
                {
                    List<object> alChild = (List<object>)childNodeNames[childname];
                    if (alChild.Count == 1)
                        OutputNode(childname, alChild[0], sbJSON, true);
                    else
                    {
                        sbJSON.Append(" \"" + SafeJSON(childname) + "\": [ ");
                        foreach (object Child in alChild)
                            OutputNode(childname, Child, sbJSON, false);
                        sbJSON.Remove(sbJSON.Length - 2, 2);
                        sbJSON.Append(" ], ");
                    }
                }
                sbJSON.Remove(sbJSON.Length - 2, 2);
                sbJSON.Append(" }");
            }

            //  StoreChildNode: Store data associated with each nodeName
            //                  so that we know whether the nodeName is an array or not.
            private static void StoreChildNode(SortedList<string, object> childNodeNames, string nodeName, object nodeValue)
            {
                // Pre-process contraction of XmlElement-s
                if (nodeValue is XmlElement)
                {
                    // Convert  <aa></aa> into "aa":null
                    //          <aa>xx</aa> into "aa":"xx"
                    XmlNode cnode = (XmlNode)nodeValue;
                    if (cnode.Attributes.Count == 0)
                    {
                        XmlNodeList children = cnode.ChildNodes;
                        if (children.Count == 0)
                            nodeValue = null;
                        else if (children.Count == 1 && (children[0] is XmlText))
                            nodeValue = ((XmlText)(children[0])).InnerText;
                    }
                }
                // Add nodeValue to ArrayList associated with each nodeName
                // If nodeName doesn't exist then add it
                List<object> ValuesAL;

                if (childNodeNames.ContainsKey(nodeName))
                {
                    ValuesAL = (List<object>)childNodeNames[nodeName];
                }
                else
                {
                    ValuesAL = new List<object>();
                    childNodeNames[nodeName] = ValuesAL;
                }
                ValuesAL.Add(nodeValue);
            }

            private static void OutputNode(string childname, object alChild, StringBuilder sbJSON, bool showNodeName)
            {
                if (alChild == null)
                {
                    if (showNodeName)
                        sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
                    sbJSON.Append("null");
                }
                else if (alChild is string)
                {
                    if (showNodeName)
                        sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
                    string sChild = (string)alChild;
                    sChild = sChild.Trim();
                    sbJSON.Append("\"" + SafeJSON(sChild) + "\"");
                }
                else
                    XmlToJSONnode(sbJSON, (XmlElement)alChild, showNodeName);
                sbJSON.Append(", ");
            }

            // Make a string safe for JSON
            private static string SafeJSON(string sIn)
            {
                StringBuilder sbOut = new StringBuilder(sIn.Length);
                foreach (char ch in sIn)
                {
                    if (Char.IsControl(ch) || ch == '\'')
                    {
                        int ich = (int)ch;
                        sbOut.Append(@"\u" + ich.ToString("x4"));
                        continue;
                    }
                    else if (ch == '\"' || ch == '\\' || ch == '/')
                    {
                        sbOut.Append('\\');
                    }
                    sbOut.Append(ch);
                }
                return sbOut.ToString();
            }
        }

        public login ClearLogin(string xml)
        {
            var scriptArray = xml.Split('>');

            login oLogin = new login();

            //foreach (var item in scriptArray)
            //{

            //    /*primer nombre*/
            //    if (item.Contains("<faultstring"))
            //    {
            //        var resultado = item.Split('<');
            //        resultadoTransac.iResultCode = resultado[0];
            //    }
            //    /*segundo nombre*/
            //    if (item.Contains("</a:strResultMessage"))
            //    {
            //        var resultado = item.Split('<');
            //        resultadoTransac.strResultMessage = resultado[0];
            //    }


            //}

            return oLogin;
        }

        public async Task<bool> Login(string usuarioNombre, string password, string token)
        {
            /*se consulta en la base de datos si existe el usuario ingresado y se alamcena en el objeto usuario*/
            var usuario = await db.TblUsuarios.FirstOrDefaultAsync(x => x.usuario == usuarioNombre);
            /*se valida el contenido del objeto usuario*/
            if (usuario == null)
                return false;
            /*se ejecuta la funsion VerifyPasswordHash pra validar las contraseñas*/
            if ((!VerifyHashedPassword(usuario.token, token)) || (!VerifyHashedPassword(usuario.clave, password)))
            {
                return false;
            }
            return true;
        }

        /*a partir de estas contatnes se encripta una contraseña*/

        private const int _pbkdf2IterCount = 1000;//la cantidad maxima de veces que se llega a iterar oara generar la hash en la funcion que encripta
        private const int _pbkdf2SubkeyLength = 256 / 8; //primera parte de la combinacion para encriptar una contraseña es de 32 bytes
        private const int _saltSize = 128 / 8;//segunda parte de la combinacion para encriptar una contraseña mas conocida como salt o Key es de 16 bytes
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            if (hashedPasswordBytes.Length != (1 + _saltSize + _pbkdf2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                return false;
            }
            var salt = new byte[_saltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, _saltSize);
            var storedSubkey = new byte[_pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + _saltSize, storedSubkey, 0, _pbkdf2SubkeyLength);
            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, _pbkdf2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(_pbkdf2SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
        /*--------------------------------------------------------------------------------------*/

    }
}
