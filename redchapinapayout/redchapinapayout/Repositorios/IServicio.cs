using redchapinapayout.Models;
using redchapinapayout.Models.Json.Anula;
using redchapinapayout.Models.Json.Configuracion;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redchapinapayout.Repositorios
{
    
    internal interface IServicio
    {
        Task<bool> ExisteSucursal(long? idSucursal);
        Configuracion GetConfiguraciones();
        RespuestaApi SetBitacoraBusqueda(TblTransaccionesRch tblBusquedasRch, long? idTblTransacciones);
        //RespuestaApi UpdateBitacoraBusqueda(TblTransaccionesRchTest tblBusquedasRch);
        Task<RespuestaRch> UpdateBitacoraBusquedaOk(RespuestaApi responseApi, long? id, string xml, long? idCatEstadosBusquedas);
        Task<RespuestaApi> UpdateBitacoraBusquedaError(RespuestaApi responseApi, long? id, string xml, long? idEstadoTransaccionCws);
        Task<RespuestaRepositorioLogin> SetBitacoraLogin(string xmlRequest, string xmlResponse, RespuestaRepositorioLogin jsonResponse);
        Task<RespuestaRepositorioReserva> UpdateBitacoraReservaOk(ResponseReserva _resultadoJson, RespuestaReservaApi respuestaReservaApi, long? idTblTransaccionesRch, string usuarioTransaccion, bool reserva);
        Task<RespuestaRepositorioLibera> UpdateBitacoraLiberaOk(ResponseLibera _resultadoJson, RespuestaLiberaApi respuestaReservaApi, long? idTblTransaccionesRch, string usuarioTransaccion);
        Task<RespuestaRepositorioPago> UpdateBitacoraPagoOk(ResponsesPago _resultadoJson, RespuestaExecutePagoApi respuestaExecuteApi, long? idTblTransaccionesRch, string usuarioTransaccion, bool pago, int? asociado, string numeroCuenta);
        Task<RespuestaRepositorioAnula> UpdateBitacoraAnulacionOk(ResponsesAnula _resultadoJson, ResponseAnula respuestaAnulaApi, long? idTblTransaccionesRch, string usuarioTransaccion, bool anulacion, string motivo);
    }
}
