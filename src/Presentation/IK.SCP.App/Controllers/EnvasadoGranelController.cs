using IK.SCP.App.Routes;
using IK.SCP.Application.ENV.Commands;
using IK.SCP.Application.ENV.Queries;
using IK.SCP.Application.PDF.Envasado.Dao;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Controllers
{
    public class EnvasadoGranelController : BaseApiController
    {

        #region CHECKLIST

        [HttpGet(ApiRoutes.GET_ALL_GRANEL_CHECKLIST)]
        public async Task<IActionResult> GetAllChecklist([FromQuery] GetAllChecklistQuery getAllChecklistQuery)
        {
            var result = await Mediator.Send(getAllChecklistQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_GRANEL_CHECKLIST_ORDEN)]
        public async Task<IActionResult> GetChecklistOrden([FromQuery] GetGranelChecklistOrdenQuery getGranelChecklistOrdenQuery)
        {
            var result = await Mediator.Send(getGranelChecklistOrdenQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_GRANEL_CHECKLIST)]
        public async Task<IActionResult> GetChecklistById([FromRoute] GetGranelChecklistByIdQuery getGranelChecklistByIdQuery)
        {
            var result = await Mediator.Send(getGranelChecklistByIdQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_GRANEL_CHECKLIST_ORDEN)]
        public async Task<IActionResult> PostChecklistOrden([FromBody] InsertGranelChecklistCommand insertGranelChecklistCommand)
        {
            var result = await Mediator.Send(insertGranelChecklistCommand);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_GRANEL_CHECKLIST_CIERRE)]
        public async Task<IActionResult> PostChecklistCierre([FromRoute] UpdateGranelChecklistCommand updateGranelChecklistCommand)
        {
            var result = await Mediator.Send(updateGranelChecklistCommand);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_GRANEL_CHECKLIST_DATOS)]
        public async Task<IActionResult> PostChecklistDatos([FromBody] UpdateGranelChecklistDatosCommand updateGranelChecklistDatosCommand)
        {
            var result = await Mediator.Send(updateGranelChecklistDatosCommand);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.GET_GRANEL_CHECKLIST_ESPECIFICACIONES)]
        public async Task<IActionResult> GetChecklistEspecificaciones([FromQuery] GetChecklistEspecificacionesQuery getChecklistEspecificacionesQuery)
        {
            var result = await Mediator.Send(getChecklistEspecificacionesQuery);
            return Ok(result);
        }
        
        [HttpPut(ApiRoutes.GET_GRANEL_CHECKLIST_ESPECIFICACIONES)]
        public async Task<IActionResult> PutChecklistEspecificaciones([FromBody] UpdateGranelEspecificacionesCommand updateGranelEspecificacionesCommand)
        {
            var result = await Mediator.Send(updateGranelEspecificacionesCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_GRANEL_CONDICION_OPERATIVA_DETALLE)]
        public async Task<IActionResult> GetGranelCondicionOperativaDetalle([FromRoute] GetGranelCondicionOperativaDetalleQuery getGranelCondicionOperativaDetalleQuery)
        {
            var result = await Mediator.Send(getGranelCondicionOperativaDetalleQuery);
            return Ok(result);
        }
        
        [HttpPut(ApiRoutes.POST_GRANEL_CONDICION_OPERATIVA_DETALLE)]
        public async Task<IActionResult> PutGranelCondicionOperativaDetalle([FromBody] UpdateGranelCondicionOperativaDetalleCommand updateGranelCondicionOperativaDetalleCommand)
        {
            var result = await Mediator.Send(updateGranelCondicionOperativaDetalleCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_GRANEL_CONDICION_PROCESO_DETALLE)]
        public async Task<IActionResult> GetGranelCondicionProcesoDetalle([FromRoute] GetGranelCondicionProcesoDetalleQuery getGranelCondicionProcesoDetalleQuery)
        {
            var result = await Mediator.Send(getGranelCondicionProcesoDetalleQuery);
            return Ok(result);
        }
        
        [HttpPut(ApiRoutes.PUT_GRANEL_CONDICION_PROCESO_DETALLE)]
        public async Task<IActionResult> PutGranelCondicionProcesoDetalle([FromBody] UpdateGranelCondicionProcesoDetalleCommand updateGranelCondicionProcesoDetalleCommand)
        {
            var result = await Mediator.Send(updateGranelCondicionProcesoDetalleCommand);
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.POST_GRANEL_OBSERVACION)]
        public async Task<IActionResult> PostGranelObservacion([FromBody] InsertGranelChecklistObservacionCommand insertGranelChecklistObservacionCommand)
        {
            var result = await Mediator.Send(insertGranelChecklistObservacionCommand);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_GRANEL_CHECKLIST_REVISION)]
        public async Task<IActionResult> PostGranelChecklistRevision([FromBody] InsertGranelChecklistRevisionCommand insertGranelChecklistRevisionCommand)
        {
            var result = await Mediator.Send(insertGranelChecklistRevisionCommand);
            return Ok(result);
        }


        #endregion CHECKLIST

        #region CONTROL DE PARAMETROS


        [HttpGet(ApiRoutes.GET_ALL_GRANEL_PARAMETRO_CONTROL)]
        public async Task<IActionResult> GetGranelParametrosControl([FromQuery] GetGranelParametrosControlQuery getGranelParametrosControlQuery)
        {
            var result = await Mediator.Send(getGranelParametrosControlQuery);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.GET_ALL_GRANEL_CONTROL)]
        public async Task<IActionResult> GetAllGranelControl([FromQuery] GetAllGranelControlQuery getAllGranelControlQuery)
        {
            var result = await Mediator.Send(getAllGranelControlQuery);
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.GET_ALL_GRANEL_CONTROL)]
        public async Task<IActionResult> PostGranelControl([FromBody] InsertGranelControlCommand insertGranelControlCommand)
        {
            var result = await Mediator.Send(insertGranelControlCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.POST_GRANEL_CONTROL_OBSERVACION)]
        public async Task<IActionResult> GetAllGranelControlObservacion([FromQuery] GetAllGranelControlObservacionQuery getAllGranelControlObservacionQuery)
        {
            var result = await Mediator.Send(getAllGranelControlObservacionQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_GRANEL_CONTROL_OBSERVACION)]
        public async Task<IActionResult> PostGranelControlObservacion([FromBody] InsertGranelControlObservacionCommand insertGranelControlObservacionCommand)
        {
            var result = await Mediator.Send(insertGranelControlObservacionCommand);
            return Ok(result);
        }


        #endregion CONTROL DE PARAMETROS

        #region EVALUACION DE PT


        [HttpGet(ApiRoutes.GET_ALL_GRANEL_EVALUACION_PT)]
        public async Task<IActionResult> GetAllGranelEvaluacion([FromQuery] GetAllGranelEvaluacionQuery getAllGranelEvaluacionQuery)
        {
            var result = await Mediator.Send(getAllGranelEvaluacionQuery);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.GET_GRANEL_EVALUACION_PT)]
        public async Task<IActionResult> GetGranelEvaluacion([FromRoute] GetGranelEvaluacionQuery getGranelEvaluacionQuery)
        {
            var result = await Mediator.Send(getGranelEvaluacionQuery);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_GRANEL_EVALUACION_PT)]
        public async Task<IActionResult> PostGranelEvaluacion([FromBody] InsertGranelEvaluacionCommand insertGranelEvaluacionCommand)
        {
            var result = await Mediator.Send(insertGranelEvaluacionCommand);
            return Ok(result);
        }


        #endregion EVALUACION DE PT

        #region CODIFICACION


        [HttpPost(ApiRoutes.POST_GRANEL_CODIFICACION_CARGA)]
        public async Task<IActionResult> PostGranelCodificacionCarga()
        {
            var result = await Mediator.Send(new PostArranqueCargaCommand() { archivos = Request.Form, tipo = Domain.Enums.eTipoCarga.ENV_GRANEL_CODIFICACION });
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_ALL_GRANEL_CODIFICACION)]
        public async Task<IActionResult> GetAllGranelCodificacion([FromQuery] GetAllGranelCodificacionQuery getAllGranelCodificacionQuery)
        {
            var _resp = await Mediator.Send(getAllGranelCodificacionQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_GRANEL_CODIFICACION)]
        public async Task<IActionResult> PostGranelCodificacion([FromBody] InsertGranelCodificacionCommand insertGranelCodificacionCommand)
        {
            var _resp = await Mediator.Send(insertGranelCodificacionCommand);
            return Ok(_resp);
        }


        #endregion CODIFICACION

        #region Impresion PDF
        [HttpGet(ApiRoutes.PRINT_GRANEL_CHECKLIST)]
        public async Task<IActionResult> PrintDocumentGranelArranqueControlProceso([FromQuery] ArranqueControlProcesosE4 arranqueControl)
        {
            var response = Mediator.Send(arranqueControl);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        

        #endregion
    }
}
