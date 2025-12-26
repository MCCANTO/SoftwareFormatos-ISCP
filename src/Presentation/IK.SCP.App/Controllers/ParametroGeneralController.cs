using IK.SCP.App.Routes;
using IK.SCP.Application.ENV.Queries;
using Microsoft.AspNetCore.Mvc;


namespace IK.SCP.App.Controllers
{
    public class ParametroGeneralController : BaseApiController
    {

        [HttpGet(ApiRoutes.GET_ALL_PARAMETRO_GENERAL)]
        public async Task<IActionResult> GetAllParametroGeneral([FromQuery] GetAllParametroGeneralQuery getAllParametroGeneralQuery)
        {
            var _resp = await Mediator.Send(getAllParametroGeneralQuery);
            return Ok(_resp);
        }

    }
}

