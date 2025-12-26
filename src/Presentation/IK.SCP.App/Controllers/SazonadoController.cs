using IK.SCP.App.Routes;
using IK.SCP.Application.PDF.Sazonado.Dao;
using IK.SCP.Application.SAZ.Commands;
using IK.SCP.Application.SAZ.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Controllers
{
    public class SazonadoController : BaseApiController
    {
        [HttpGet(ApiRoutes.GET_ALL_SAZONADOR)]
        public async Task<IActionResult> GetAllSazonador()
        {
            return Ok(await Mediator.Send(new GetAllSazonadorQuery()));
        }

        [HttpGet(ApiRoutes.GET_ALL_SAZONADO_LINEA_FR)]
        public async Task<IActionResult> GetAllSazonadorLineaFR([FromQuery] GetAllSazonadorLineaFRQuery getAllSaborizadoLineaFRQuery)
        {
            return Ok(await Mediator.Send(getAllSaborizadoLineaFRQuery));
        }

        #region ARRANQUE

        [HttpGet(ApiRoutes.POST_SAZONADO_ARRANQUE)]
        public async Task<IActionResult> GetAllArranqueSaborizado([FromQuery] GetAllArranqueSaborizadoQuery getAllArranqueSaborizadoQuery)
        {
            return Ok(await Mediator.Send(getAllArranqueSaborizadoQuery));
        }

        [HttpPost(ApiRoutes.POST_SAZONADO_ARRANQUE)]
        public async Task<IActionResult> InsertArranqueSaborizado([FromBody] InsertArranqueSaborizadoCommand insertArranqueSaborizadoCommand)
        {
            return Ok(await Mediator.Send(insertArranqueSaborizadoCommand));
        }
        
        [HttpPut(ApiRoutes.POST_SAZONADO_ARRANQUE)]
        public async Task<IActionResult> CloseArranqueSaborizado([FromBody] CloseArranqueSaborizadoCommand closeArranqueSaborizadoCommand)
        {
            return Ok(await Mediator.Send(closeArranqueSaborizadoCommand));
        }

        [HttpGet(ApiRoutes.POST_SAZONADO_ARRANQUE + "/{id}")]
        public async Task<IActionResult> GetByIdArranqueSaborizado([FromRoute] GetByIdArranqueSaborizadoQuery getByIdArranqueSaborizadoQuery)
        {
            return Ok(await Mediator.Send(getByIdArranqueSaborizadoQuery));
        }

        [HttpPost(ApiRoutes.POST_SAZONADO_ARRANQUE_CONDICION)]
        public async Task<IActionResult> InsertArranqueCondicionSaborizado([FromBody] InsertArranqueCondicionSaborizadoCommand insertArranqueCondicionSaborizadoCommand)
        {
            return Ok(await Mediator.Send(insertArranqueCondicionSaborizadoCommand));
        }

        [HttpGet(ApiRoutes.GET_SAZONADO_ARRANQUE_VERIFICACION_EQUIPO)]
        public async Task<IActionResult> GetAllArranqueVerificacionSazonado([FromRoute] GetAllArranqueVerificacionSazonadoQuery getAllArranqueVerificacionSazonadoQuery)
        {
            return Ok(await Mediator.Send(getAllArranqueVerificacionSazonadoQuery));
        }

        [HttpPost(ApiRoutes.POST_SAZONADO_ARRANQUE_VERIFICACION_EQUIPO)]
        public async Task<IActionResult> InsertArranqueVerificacionSazonado([FromBody] InsertArranqueVerificacionSazonadoCommand insertArranqueVerificacionSazonadoCommand)
        {
            return Ok(await Mediator.Send(insertArranqueVerificacionSazonadoCommand));
        }
        
        [HttpPost(ApiRoutes.POST_SAZONADO_ARRANQUE_VARIABLE_BASICA)]
        public async Task<IActionResult> InsertArranqueVariableSazonado([FromBody] InsertArranqueVariableSazonadoCommand insertArranqueVariableSazonadoCommand)
        {
            return Ok(await Mediator.Send(insertArranqueVariableSazonadoCommand));
        }
        
        [HttpPost(ApiRoutes.POST_SAZONADO_ARRANQUE_OBSERVACION)]
        public async Task<IActionResult> InsertArranqueObservacionSazonado([FromBody] InsertArranqueObservacionSazonadoCommand insertArranqueObservacionSazonadoCommand)
        {
            return Ok(await Mediator.Send(insertArranqueObservacionSazonadoCommand));
        }
        #endregion ARRANQUE
        
        #region PRINT SAZONADO
        
        [HttpGet(ApiRoutes.PRINT_SAZONADO_CHECKLIST_ARRANQUE)]
        public async Task<IActionResult> PrintDocumentCheckListArranqueSazonado([FromQuery] ChecklistArranqueSazonado checklistArranqueSazonado)
        {
            var response = Mediator.Send(checklistArranqueSazonado);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        #endregion
    }
}
