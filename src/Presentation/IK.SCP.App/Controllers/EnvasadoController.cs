
using IK.SCP.App.Routes;
using IK.SCP.Application.ENV.Commands;
using IK.SCP.Application.ENV.Orden.Queries;
using IK.SCP.Application.ENV.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IK.SCP.Application.PDF.Envasado.Dao;


namespace IK.SCP.App.Controllers
{

    [ApiController]
    [AllowAnonymous]
    public class EnvasadoController : BaseApiController
    {

        public EnvasadoController()
        {
        }

        #region ORDEN


        [HttpGet(ApiRoutes.GET_ALL_ENVASADORA)]
        public async Task<IActionResult> GetAllEnvasadora()
        {
            var _resp = await Mediator.Send(new GetAllEnvasadoraQuery());
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_ORDEN)]
        public async Task<IActionResult> GetByIdOrden([FromRoute] GetByIdOrdenQuery getByIdOrdenQuery)
        {
            var _resp = await Mediator.Send(getByIdOrdenQuery);
            return Ok(_resp);
        }


        #endregion ORDEN

        #region ARRANQUE MAQUINA

        [HttpGet(ApiRoutes.GET_ARRANQUE_MAQUINA_ABIERTO)]
        public async Task<IActionResult> GetArranqueMaquina([FromQuery] GetArranqueMaquinaQuery getArranqueMaquinaQuery)
        {
            var _resp = await Mediator.Send(getArranqueMaquinaQuery);
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_MAQUINA)]
        public async Task<IActionResult> GetAllArranqueMaquina([FromQuery] GetAllArranqueMaquinaQuery getAllArranqueMaquinaQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueMaquinaQuery);
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_BY_ID_ARRANQUE_MAQUINA)]
        public async Task<IActionResult> GetByIdArranqueMaquina([FromRoute] GetByIdArranqueMaquinaQuery getByIdArranqueMaquinaQuery)
        {
            var _resp = await Mediator.Send(getByIdArranqueMaquinaQuery);
            return Ok(_resp);
        }

        [HttpPost]
        [Route(ApiRoutes.GET_ALL_ARRANQUE_MAQUINA)]
        public async Task<IActionResult> GuardarArranqueMaquina([FromBody] SaveArranqueMaquinaCommand saveArranqueMaquinaCommand)
        {
            var _resp = await Mediator.Send(saveArranqueMaquinaCommand);
            return Ok(_resp);
        }

        [HttpGet]
        [Route(ApiRoutes.GET_ARRANQUE_MAQUINA_COND_PREV)]
        public async Task<IActionResult> GetArranqueMaquinaCondicionPrevia([FromQuery] GetArranqueMaquinaCondicionPreviaQuery getArranqueMaquinaCondicionPreviaCommand)
        {
            var _resp = await Mediator.Send(getArranqueMaquinaCondicionPreviaCommand);
            return Ok(_resp);
        }

        [HttpPost]
        [Route(ApiRoutes.GET_ARRANQUE_MAQUINA_COND_PREV)]
        public async Task<IActionResult> GuardarArranqueMaquinaCondicionPrevia([FromBody] SaveArranqueMaquinaCondicionPreviaCommand saveArranqueMaquinaCondicionPreviaCommand)
        {
            var _resp = await Mediator.Send(saveArranqueMaquinaCondicionPreviaCommand);
            return Ok(_resp);
        }

        [HttpGet]
        [Route(ApiRoutes.GET_ARRANQUE_MAQUINA_VAR_BAS)]
        public async Task<IActionResult> GetArranqueMaquinaVariableBasica([FromQuery] GetArranqueMaquinaVariableBasicaQuery getArranqueMaquinaVariableBasicaCommand)
        {
            var _resp = await Mediator.Send(getArranqueMaquinaVariableBasicaCommand);
            return Ok(_resp);
        }

        [HttpPost]
        [Route(ApiRoutes.GET_ARRANQUE_MAQUINA_VAR_BAS)]
        public async Task<IActionResult> GuardarArranqueMaquinaVariableBasica([FromBody] SaveArranqueMaquinaVariableBasicaCommand saveArranqueMaquinaVariableBasicaCommand)
        {
            var _resp = await Mediator.Send(saveArranqueMaquinaVariableBasicaCommand);
            return Ok(_resp);
        }

        [HttpGet]
        [Route(ApiRoutes.GET_ARRANQUE_MAQUINA_OBS)]
        public async Task<IActionResult> GetArranqueMaquinaObservacion([FromQuery] GetArranqueMaquinaObservacionQuery getArranqueMaquinaObservacionCommand)
        {
            var _resp = await Mediator.Send(getArranqueMaquinaObservacionCommand);
            return Ok(_resp);
        }

        [HttpPost]
        [Route(ApiRoutes.GET_ARRANQUE_MAQUINA_OBS)]
        public async Task<IActionResult> GuardarArranqueMaquinaObservacion([FromBody] SaveArranqueMaquinaObservacionCommand saveArranqueMaquinaObservacionCommand)
        {
            var _resp = await Mediator.Send(saveArranqueMaquinaObservacionCommand);
            return Ok(_resp);
        }

        
        #endregion ARRANQUE MAQUINA

        #region ARRANQUE


        [HttpGet(ApiRoutes.GET_ARRANQUE_BY_ORDEN)]
        public async Task<IActionResult> GetArranqueByOrden([FromQuery] GetArranqueByOrdenQuery getArranqueByOrdenQuery)
        {
            var _resp = await Mediator.Send(getArranqueByOrdenQuery);
            return Ok(_resp);
        }
        
        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_BY_ORDEN)]
        public async Task<IActionResult> GetAllArranqueByOrden([FromQuery] GetAllArranqueByOrdenQuery getAllArranqueByOrdenQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueByOrdenQuery);
            return Ok(_resp);
        }
        
        // MODIFICAR PENDIENTE DE ADJUNTAR EL GUARDADO ..... 
        [HttpPost]
        [Route(ApiRoutes.GET_ARRANQUE_BY_ORDEN)]
        public async Task<IActionResult> saveArranqueEnvasado([FromBody] SaveArranqueEnvasadoCommand saveArranqueEnvasadoCommand)
        {
            var _resp = await Mediator.Send(saveArranqueEnvasadoCommand);
            return Ok(_resp);
        }
        
        [HttpPost(ApiRoutes.POST_ARRANQUE)]
        public async Task<IActionResult> PostArranque([FromBody] PostArranqueCommand postArranqueCommand)
        {
            var _resp = await Mediator.Send(postArranqueCommand);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_CARGA_ARCHIVO)]
        public async Task<IActionResult> PostArranqueCargaArchivo()
        {
            var result = await Mediator.Send(new PostArranqueCargaCommand() { archivos = Request.Form, tipo = Domain.Enums.eTipoCarga.ENV_CODIFICACION });
            return Ok(result);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_CARGA_INSPECCION)]
        public async Task<IActionResult> PostArranqueCargaInspeccion()
        {
            var result = await Mediator.Send(new PostArranqueCargaCommand() { archivos = Request.Form, tipo = Domain.Enums.eTipoCarga.ENV_INSPECCION });
            return Ok(result);
        }

        #endregion ARRANQUE

        #region ARRANQUE CONDICION PREVIA

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_CONDICIONPREVIA)]
        public async Task<IActionResult> GetAllArranqueCondicionPrevia([FromQuery] GetAllArranqueCondicionPreviaQuery getAllArranqueCondicionPreviaQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueCondicionPreviaQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.PUT_ARRANQUE_CONDICIONPREVIA)]
        public async Task<IActionResult> PutArranqueCondicionPrevia(PutArranqueCondicionPreviaCommand putArranqueCondicionPreviaCommand)
        {
            var _resp = await Mediator.Send(putArranqueCondicionPreviaCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE CONDICION PREVIA

        #region ARRANQUE VARIABLE BASICA

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_VARIABLE_BASICA)]
        public async Task<IActionResult> GetAllArranqueVariableBasica([FromQuery] GetAllArranqueVariableBasicaQuery getAllArranqueVariableBasicaQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueVariableBasicaQuery);
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_ARRANQUE_VARIABLE_BASICA)]
        public async Task<IActionResult> GetArranqueVariableBasica([FromRoute] GetArranqueVariableBasicaQuery getArranqueVariableBasicaQuery)
        {
            var _resp = await Mediator.Send(getArranqueVariableBasicaQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_VARIABLE_BASICA)]
        public async Task<IActionResult> PostArranqueVariableBasica([FromBody] PostArranqueVariableBasicaCommand postArranqueVariableBasicaCommand)
        {
            var _resp = await Mediator.Send(postArranqueVariableBasicaCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE VARIABLE BASICA

        #region ARRANQUE CONTRAMUESTRA

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_CONTRAMUESTRA)]
        public async Task<IActionResult> GetAllArranqueContramuestra([FromQuery] GetAllArranqueContramuestraQuery getAllArranqueContramuestraQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueContramuestraQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_CONTRAMUESTRA)]
        public async Task<IActionResult> PostArranqueContramuestra(PostArranqueContramuestraCommand postArranqueContramuestraCommand)
        {
            var _resp = await Mediator.Send(postArranqueContramuestraCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE CONTRAMUESTRA

        #region ARRANQUE CODIFICACION

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_CODIFICACION)]
        public async Task<IActionResult> GetAllArranqueCodificacion([FromQuery] GetAllArranqueCodificacionQuery getAllArranqueCodificacionQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueCodificacionQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_CODIFICACION)]
        public async Task<IActionResult> PostArranqueCodificacion(PostArranqueCodificacionCommand postArranqueCodificacionCommand)
        {
            var _resp = await Mediator.Send(postArranqueCodificacionCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE CODIFICACION

        #region ARRANQUE PERSONAL

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_PERSONAL)]
        public async Task<IActionResult> GetAllArranquePersonal([FromQuery] GetAllArranquePersonalQuery getAllArranquePersonalQuery)
        {
            var _resp = await Mediator.Send(getAllArranquePersonalQuery);
            return Ok(_resp);
        }

        [HttpGet(ApiRoutes.GET_ARRANQUE_PERSONAL)]
        public async Task<IActionResult> GetArranquePersonal([FromQuery] GetArranquePersonalQuery getArranquePersonalQuery)
        {
            var _resp = await Mediator.Send(getArranquePersonalQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_PERSONAL)]
        public async Task<IActionResult> PostArranquePersonal(PostArranquePersonalCommand postArranquePersonalCommand)
        {
            var _resp = await Mediator.Send(postArranquePersonalCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE PERSONAL

        #region ARRANQUE COMPONENTE

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_COMPONENTE)]
        public async Task<IActionResult> GetAllArranqueComponente([FromQuery] GetAllArranqueComponenteQuery getAllArranqueComponenteQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueComponenteQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_COMPONENTE)]
        public async Task<IActionResult> PostArranqueComponente(PostArranqueComponenteCommand postArranqueComponenteCommand)
        {
            var _resp = await Mediator.Send(postArranqueComponenteCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE COMPONENTE

        #region ARRANQUE OBSERVACION

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_OBSERVACION)]
        public async Task<IActionResult> GetAllArranqueObservacion([FromQuery] GetAllArranqueObservacionQuery getAllArranqueObservacionQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueObservacionQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_OBSERVACION)]
        public async Task<IActionResult> PostArranqueObservacion(PostArranqueObservacionCommand postArranqueObservacionCommand)
        {
            var _resp = await Mediator.Send(postArranqueObservacionCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE OBSERVACION

        #region ARRANQUE INSPECCION

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_INSPECCION)]
        public async Task<IActionResult> GetAllArranqueInspeccion([FromQuery] GetAllArranqueInspeccionQuery getAllArranqueInspeccionQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueInspeccionQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_INSPECCION)]
        public async Task<IActionResult> PostArranqueInspeccion(PostArranqueInspeccionCommand postArranqueInspeccionCommand)
        {
            var _resp = await Mediator.Send(postArranqueInspeccionCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE INSPECCION

        #region ARRANQUE REVISION

        [HttpGet(ApiRoutes.GET_ALL_ARRANQUE_REVISION)]
        public async Task<IActionResult> GetAllArranqueRevision([FromQuery] GetAllArranqueRevisionQuery getAllArranqueRevisionQuery)
        {
            var _resp = await Mediator.Send(getAllArranqueRevisionQuery);
            return Ok(_resp);
        }

        [HttpPost(ApiRoutes.POST_ARRANQUE_REVISION)]
        public async Task<IActionResult> PostArranqueRevision(PostArranqueRevisionCommand postArranqueRevisionCommand)
        {
            var _resp = await Mediator.Send(postArranqueRevisionCommand);
            return Ok(_resp);
        }

        #endregion ARRANQUE REVISION
        
        #region REGISTRO PEDACERIA
        [HttpGet(ApiRoutes.GET_ALL_REGISTRO_PEDACERIA)]
        public async Task<IActionResult> GetAllRegistroPedacería([FromQuery] GetAllRegistroPedaceriaQuery getAllRegistroPedaceriaQuery)
        {
            var _resp = await Mediator.Send(getAllRegistroPedaceriaQuery);
            return Ok(_resp);
        }
        
        [HttpPost(ApiRoutes.POST_REGISTRO_PEDACERIA)]
        public async Task<IActionResult> PostRegistroPedaceria(InsertRegistroPedaceriaCommand insertRegistroPedaceriaCommand)
        {
            var _resp = await Mediator.Send(insertRegistroPedaceriaCommand);
            return Ok(_resp);
        }
        #endregion
        

        #region GENERATE PDF
        [HttpGet(ApiRoutes.PRINT_ARRANQUE_MAQUINA_ENVASADO)]
        public async Task<IActionResult> PrintDocumentArranqueMaquinistaEnvasado([FromQuery] ArranqueMaquinistaEnvasado arranqueMaquinistaEnvasado)
        {
            var response = Mediator.Send(arranqueMaquinistaEnvasado);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        
        [HttpGet(ApiRoutes.PRINT_ARRANQUE_ENVASADO)]
        public async Task<IActionResult> PrintDocumentArranqueEnvasado([FromQuery] ArranqueEnvasado arranqueEnvasado)
        {
            var response = Mediator.Send(arranqueEnvasado);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        [HttpGet(ApiRoutes.PRINT_REGISTRO_PEDACERIA)]
        public async Task<IActionResult> PrintDocumentRegistroPedaceria([FromQuery] Pedaceria pedaceria)
        {
            var response = Mediator.Send(pedaceria);
            var pdfStream = response.Result.Data as MemoryStream;
            var pdfBytes = pdfStream.ToArray();
                
            return File(pdfBytes,"application/pdf","archivo.pdf");
        }
        #endregion

    }
}
