using IK.SCP.App.Routes;
using IK.SCP.Application.ENV.Commands;
using IK.SCP.Application.ENV.Queries;
using IK.SCP.Application.PDF.Envasado.Dao;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Controllers
{

    public class BlendingController : BaseApiController
    {

        #region CHECKLIST ARRANQUE

        [HttpGet(ApiRoutes.GET_ALL_BLENDING_ARTICULO_MEZCLA)]
        public async Task<IActionResult> GetBlendingValidacionArticulo([FromQuery] GetBlendingValidacionArticuloQuery getBlendingValidacionArticuloQuery)
        {
            var result = await Mediator.Send(getBlendingValidacionArticuloQuery);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.GET_ALL_BLENDING_COMPONENTES)]
        public async Task<IActionResult> GetAllBlendingComponentes([FromQuery] GetAllBlendingComponentesQuery getAllBlendingComponentesQuery)
        {
            var result = await Mediator.Send(getAllBlendingComponentesQuery);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.GET_ALL_BLENDING_ARRANQUES)]
        public async Task<IActionResult> GetAllBlendingArranques([FromQuery] GetAllBlendingArranquesQuery getAllBlendingArranquesQuery)
        {
            var result = await Mediator.Send(getAllBlendingArranquesQuery);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.GET_BLENDING_ARRANQUE_OPEN)]
        public async Task<IActionResult> GetBlendingArranqueActivo([FromQuery] GetBlendingArranqueActivoQuery getBlendingArranqueActivoQuery)
        {
            var result = await Mediator.Send(getBlendingArranqueActivoQuery);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_BLENDING_ARRANQUE_X_ID)]
        public async Task<IActionResult> GetByIdBlendingArranque([FromRoute] GetByIdBlendingArranqueQuery getByIdBlendingArranqueQuery)
        {
            var result = await Mediator.Send(getByIdBlendingArranqueQuery);
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.POST_BLENDING_ARRANQUE)]
        public async Task<IActionResult> InsertBlendingArranque([FromBody] InsertBlendingArranqueCommand insertBlendingArranqueCommand)
        {
            var result = await Mediator.Send(insertBlendingArranqueCommand);
            return Ok(result);
        }


        [HttpGet(ApiRoutes.GET_BLENDING_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> GetAllBlendingVerificacionEquipoDetalle([FromRoute] GetAllBlendingVerificacionEquipoDetalleQuery getAllBlendingVerificacionEquipoDetalleQuery)
        {
            var result = await Mediator.Send(getAllBlendingVerificacionEquipoDetalleQuery);
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.POST_BLENDING_VERIFICACION_EQUIPO_DETALLE)]
        public async Task<IActionResult> InsertBlendingVerificacionEquipoDetalle([FromBody] InsertBlendingVerificacionEquipoDetalleCommand insertBlendingVerificacionEquipoDetalleCommand)
        {
            var result = await Mediator.Send(insertBlendingVerificacionEquipoDetalleCommand);
            return Ok(result);
        }
        
        [HttpPut(ApiRoutes.PUT_BLENDING_CONDICION)]
        public async Task<IActionResult> UpdateBlendingCondicionPrevia([FromBody] UpdateBlendingCondicionPreviaCommand updateBlendingCondicionPreviaCommand)
        {
            var result = await Mediator.Send(updateBlendingCondicionPreviaCommand);
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.POST_BLENDING_OBSERVACION)]
        public async Task<IActionResult> InsertBlendingObservacion([FromBody] InsertBlendingObservacionCommand insertBlendingObservacionCommand)
        {
            var result = await Mediator.Send(insertBlendingObservacionCommand);
            return Ok(result);
        }
        
        [HttpPut(ApiRoutes.PUT_BLENDING_ARRANQUE_CIERRE)]
        public async Task<IActionResult> UpdateBlendingArranqueCierre([FromBody] UpdateBlendingArranqueCierreCommand updateBlendingArranqueCierreCommand)
        {
            var result = await Mediator.Send(updateBlendingArranqueCierreCommand);
            return Ok(result);
        }


        #endregion CHECKLIST ARRANQUE

        #region CONTROL


        [HttpGet(ApiRoutes.GET_BLENDING_CONTROL_COMPONENTES)]
        public async Task<IActionResult> GetAllBlendingControlComponente([FromQuery] GetAllBlendingControlComponenteQuery getAllBlendingControlComponenteQuery)
        {
            var result = await Mediator.Send(getAllBlendingControlComponenteQuery);
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.POST_BLENDING_CONTROL_COMPONENTES)]
        public async Task<IActionResult> InsertBlendingControlComponente([FromBody] InsertBlendingControlComponenteCommand insertBlendingControlComponenteCommand)
        {
            var result = await Mediator.Send(insertBlendingControlComponenteCommand);
            return Ok(result);
        }
        
        [HttpPut(ApiRoutes.PUT_BLENDING_CONTROL_COMPONENTES)]
        public async Task<IActionResult> UpdateBlendingControlComponente([FromBody] UpdateBlendingControlComponenteCommand updateBlendingControlComponenteCommand)
        {
            var result = await Mediator.Send(updateBlendingControlComponenteCommand);
            return Ok(result);
        }
        
        [HttpGet(ApiRoutes.GET_BLENDING_CONTROL)]
        public async Task<IActionResult> GetAllBlendingControl([FromQuery] GetAllBlendingControlQuery getAllBlendingControlQuery)
        {
            var result = await Mediator.Send(getAllBlendingControlQuery);
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.POST_BLENDING_CONTROL)]
        public async Task<IActionResult> InsertBlendingControl([FromBody] InsertBlendingControlCommand insertBlendingControlCommand)
        {
            var result = await Mediator.Send(insertBlendingControlCommand);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.GET_BLENDING_CONTROL_MERMA)]
        public async Task<IActionResult> GetAllBlendingControlMerma([FromQuery] GetAllBlendingControlMermaQuery getAllBlendingControlMermaQuery)
        {
            var result = await Mediator.Send(getAllBlendingControlMermaQuery);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.GET_BLENDING_CONTROL_MERMA)]
        public async Task<IActionResult> UpdateBlendingControlMerma([FromBody] UpdateBlendingControlMermaCommand updateBlendingControlMermaCommand)
        {
            var result = await Mediator.Send(updateBlendingControlMermaCommand);
            return Ok(result);
        }


        #endregion CONTROL

        #region Generate PDF
        [HttpGet(ApiRoutes.PRINT_BLENDING_ARRANQUE)]
        public async Task<IActionResult> PrintDocumentArranqueEnvasado([FromQuery] ArranqueBlending arranqueBlending)
        {
            var response = Mediator.Send(arranqueBlending);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_BLENDING_CONTROL)]
        public async Task<IActionResult> PrintDocumentArranqueEnvasado([FromQuery] ControlBlending controlBlending)
        {
            var response = Mediator.Send(controlBlending);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }

        #endregion
    }
}
