using IK.SCP.App.Routes;
using IK.SCP.Application.FR.Commands;
using IK.SCP.Application.FR.Commands.Insert;
using IK.SCP.Application.FR.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IK.SCP.Application.PDF.Fritura.Dao;


namespace IK.SCP.App.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class FrituraController : BaseApiController
    {

        #region ORDEN

        [HttpGet(ApiRoutes.GET_ALL_FREIDORA)]
        public async Task<IActionResult> GetAllLineas()
        {
            var _resp = await Mediator.Send(new GetAllFreidoraQuery());
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_ORDEN_FR)]
        public async Task<IActionResult> GetByIdOrden([FromQuery] GetByIdOrdenQuery getByIdOrdenQuery)
        {
            var _resp = await Mediator.Send(getByIdOrdenQuery);
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_ALL_ORDEN_FR)]
        public async Task<IActionResult> GetAllOrden([FromQuery] GetAllOrdenQuery getAllOrdenQuery)
        {
            var _resp = await Mediator.Send(getAllOrdenQuery);
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_ALL_ORDEN_FR_CONSUMO)]
        public async Task<IActionResult> GetAllOrdenConsumo([FromQuery] GetAllOrdenConsumoQuery getAllOrdenConsumoQuery)
        {
            var _resp = await Mediator.Send(getAllOrdenConsumoQuery);
            return Ok(_resp);
        }


        #endregion ORDEN

        #region ARRANQUE MAQUINA

        [HttpGet(ApiRoutes.GET_ALL_FR_ARRANQUE_MAQUINA)]
        public async Task<IActionResult> GetAllArranqueMaquina([FromQuery] GetAllArranqueMaquinaQuery getAllArranqueMaquinaQuery)
        {
            var res = await Mediator.Send(getAllArranqueMaquinaQuery);
            return Ok(res);
        }

        [HttpGet(ApiRoutes.GET_FR_ARRANQUE_MAQUINA_ABIERTO)]
        public async Task<IActionResult> GetArranqueMaquina([FromQuery] GetByIdArranqueMaquinaQuery getArranqueMaquinaQuery)
        {
            var _resp = await Mediator.Send(getArranqueMaquinaQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.INSERT_FR_ARRANQUE_MAQUINA)]
        public async Task<IActionResult> InsertArranqueMaquina([FromBody] InsertArranqueMaquinaCommand insertArranqueMaquinaCommand)
        {
            var res = await Mediator.Send(insertArranqueMaquinaCommand);
            return Ok(res);
        }

        [HttpPut(ApiRoutes.UPDATE_FR_ARRANQUE_MAQUINA)]
        public async Task<IActionResult> UpdateArranqueMaquina([FromBody] UpdateArranqueMaquinaCommand updateArranqueMaquinaCommand)
        {
            var res = await Mediator.Send(updateArranqueMaquinaCommand);
            return Ok(res);
        }

        #endregion ARRANQUE MAQUINA

        #region ARRANQUE CONDICIONES PREVIAS

        [HttpGet(ApiRoutes.GET_ALL_FR_CONDICION_PREVIA)]
        public async Task<IActionResult> GetAllCondicionesPrevias([FromQuery] GetAllCondicionesPreviasQuery getAllCondicionesPreviasQuery)
        {
            var _response = await Mediator.Send(getAllCondicionesPreviasQuery);
            return Ok(_response);
        }

        [HttpPost(ApiRoutes.GET_ALL_FR_CONDICION_PREVIA)]
        public async Task<IActionResult> SaveCondicionesPrevias([FromBody] SaveCondicionesPreviasCommand saveCondicionesPreviasCommand)
        {
            var _response = await Mediator.Send(saveCondicionesPreviasCommand);
            return Ok(_response);
        }

        #endregion ARRANQUE CONDICIONES PREVIAS

        #region ARRANQUE VERIFICACION EQUIPO

        [HttpGet(ApiRoutes.GET_ALL_FR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO)]
        public async Task<IActionResult> GetAllVerificacionEquipo([FromQuery] GetAllVerificacionEquipoQuery getAllVerificacionEquipoQuery)
        {
            var _response = await Mediator.Send(getAllVerificacionEquipoQuery);
            return Ok(_response);
        }

        [HttpGet(ApiRoutes.GET_ALL_FR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> GetAllVerificacionEquipoDetalle([FromQuery] GetAllVerificacionEquipoDetalleQuery getAllVerificacionEquipoDetalleQuery)
        {
            var _response = await Mediator.Send(getAllVerificacionEquipoDetalleQuery);
            return Ok(_response);
        }

        [HttpPost(ApiRoutes.INSERT_FR_ARRANQUE_MAQUINA_VERIFICACION_EQUIPO)]
        public async Task<IActionResult> InsertVerificacionEquipo([FromBody] InsertArranqueMaquinaVerificacionEquipoCommand insertArranqueMaquinaVerificacionEquipoCommand)
        {
            var _response = await Mediator.Send(insertArranqueMaquinaVerificacionEquipoCommand);
            return Ok(_response);
        }

        #endregion ARRANQUE VERIFICACION EQUIPO

        #region ARRANQUE OBSERVACIONES

        [HttpPost(ApiRoutes.INSERT_FR_ARRANQUE_MAQUINA_OBSERVACION)]
        public async Task<IActionResult> InsertArranqueMaquinaObservacion([FromBody] InsertArranqueMaquinaObservacionCommand insertArranqueMaquinaObservacionCommand)
        {
            var _response = await Mediator.Send(insertArranqueMaquinaObservacionCommand);
            return Ok(_response);
        }

        #endregion ARRANQUE OBSERVACIONES

        #region PANELISTA

        [HttpGet(ApiRoutes.GET_ALL_FR_PANELISTA)]
        public async Task<IActionResult> GetAllPanelista()
        {
            var _response = await Mediator.Send(new GetAllPanelistaQuery());
            return Ok(_response);
        }

        #endregion PANELISTA

        #region EVALUACION ATRIBUTO


        [HttpGet(ApiRoutes.GET_ALL_FR_EVALUACION_ATRIBUTO)]
        public async Task<IActionResult> GetAllEvaluacionDesempenio([FromQuery] GetAllEvaluacionAtributoQuery getAllEvaluacionAtributoQuery)
        {
            var res = await Mediator.Send(getAllEvaluacionAtributoQuery);
            return Ok(res);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_FR_EVALUACION_ATRIBUTO)]
        public async Task<IActionResult> GetByIdEvaluacionDesempenio([FromRoute] GetByIdEvaluacionAtributoQuery getByIdEvaluacionAtributoQuery)
        {
            var res = await Mediator.Send(getByIdEvaluacionAtributoQuery);
            return Ok(res);
        }

        [HttpPost(ApiRoutes.INSERT_FR_EVALUACION_ATRIBUTO)]
        public async Task<IActionResult> InsertEvaluacionDesempenio([FromBody] InsertEvaluacionAtributoCommand insertEvaluacionAtributoCommand)
        {
            var res = await Mediator.Send(insertEvaluacionAtributoCommand);
            return Ok(res);
        }


        #endregion EVALUACION ATRIBUTO
        
        #region CONTROL DE ACEITE

        [HttpGet(ApiRoutes.GET_ALL_FR_CONTROL_ACEITE)]
        public async Task<IActionResult> GetAllControlAceiteFritura([FromQuery] GetAllControlAceiteFrituraQuery getAllControlAceiteFrituraQuery)
        {
            var res = await Mediator.Send(getAllControlAceiteFrituraQuery);
            return Ok(res);
        }

        [HttpPost(ApiRoutes.POST_FR_CONTROL_ACEITE)]
        public async Task<IActionResult> InsertControlAceiteFritura([FromBody] InsertControlAceiteFrituraCommand insertControlAceiteFrituraCommand)
        {
            var res = await Mediator.Send(insertControlAceiteFrituraCommand);
            return Ok(res);
        }

        #endregion CONTROL DE ACEITE
        
        #region REGISTRO DE CARACTERIZACION

        [HttpGet(ApiRoutes.GET_ALL_FR_DEFECTO_CARACTERIZACION)]
        public async Task<IActionResult> GetAllDefectoCaracterizacionFritura([FromQuery] GetAllDefectoCaracterizacionFrituraQuery getAllDefectoCaracterizacionFrituraQuery)
        {
            var res = await Mediator.Send(getAllDefectoCaracterizacionFrituraQuery);
            return Ok(res);
        }
        
        [HttpGet(ApiRoutes.GET_ALL_FR_REGISTRO_CARACTERIZACION)]
        public async Task<IActionResult> GetAllRegistroCaracterizacionFritura([FromQuery] GetAllRegistroCaracterizacionFrituraQuery getAllRegistroCaracterizacionFrituraQuery)
        {
            var res = await Mediator.Send(getAllRegistroCaracterizacionFrituraQuery);
            return Ok(res);
        }

        [HttpPost(ApiRoutes.POST_FR_REGISTRO_CARACTERIZACION)]
        public async Task<IActionResult> InsertRegistroCaracterizacionFritura([FromBody] InsertRegistroCaracterizacionFrituraCommand insertRegistroCaracterizacionFrituraCommand)
        {
            var res = await Mediator.Send(insertRegistroCaracterizacionFrituraCommand);
            return Ok(res);
        }

        #endregion REGISTRO DE CARACTERIZACION

        #region GENERATE PDF
        [HttpGet(ApiRoutes.GET_FR_ARRANQUE_MAQUINA_PDF)]
        public async Task<IActionResult> PrintDocumentArranqueManufactura([FromRoute] ArranqueManufactura arranqueManufactura)
        {
            var response = Mediator.Send(arranqueManufactura);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        
        [HttpGet(ApiRoutes.GET_FR_ATTRIBUTE_EVALUATION_PDF)]
        public async Task<IActionResult> PrintDocumentAttributeEvaluationFormat([FromQuery] EvaluacionAtributo evaluacionAtributo)
        {
            var response = Mediator.Send(evaluacionAtributo);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        
        [HttpGet(ApiRoutes.PRINT_FR_CONTROL_ACEITE)]
        public async Task<IActionResult> PrintDocumentControlParametroCalidadAceite([FromQuery] ControlParametroCalidadAceite parametroCalidadAceite)
        {
            var response = Mediator.Send(parametroCalidadAceite);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_FR_CARACTERIZACION_PRODUCTO_TERMINADO)]
        public async Task<IActionResult> PrintDocumentCaracterizacionProductoTerminado([FromQuery] CaracterizacionProductoTerminado caracterizacionProductoTerminado)
        {
            var response = Mediator.Send(caracterizacionProductoTerminado);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        #endregion
    }
}

