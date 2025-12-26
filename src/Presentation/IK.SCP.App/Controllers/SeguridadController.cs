using IK.SCP.App.Routes;
using IK.SCP.Application.SEG.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Controllers
{
    public class SeguridadController : BaseApiController
    {
        [HttpGet(ApiRoutes.GET_ACCIONES_X_ROL)]
        public async Task<IActionResult> GetAllAccionesXRol([FromQuery]GetAllAccionesXRolQuery getAllAccionesXRolQuery)
        {
            var result = await Mediator.Send(getAllAccionesXRolQuery);
            return Ok(result);
        }
    }
}
