using IK.SCP.App.Routes;
using IK.SCP.Application.Commands;
using IK.SCP.Application.SEG.Auth.Commands;
using IK.SCP.Application.SEG.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {

        [HttpPost(ApiRoutes.POST_AUTH_VALIDATE)]
        public async Task<IActionResult> Login([FromBody] ValidateCommand validateCommand)
        {
            System.Diagnostics.Debug.WriteLine("Credenciales del Usuario: ");
            System.Diagnostics.Debug.WriteLine(validateCommand.clave);
            System.Diagnostics.Debug.WriteLine(validateCommand.usuario);
            var res = await Mediator.Send(validateCommand);
            return Ok(res);
        }


        [HttpPost(ApiRoutes.POST_AUTH_LOGIN)]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            System.Diagnostics.Debug.WriteLine("Login del Usuario: ");
            var res = await Mediator.Send(loginCommand);
            return Ok(res);
        }

        [HttpGet(ApiRoutes.GET_AUTH_INFO)]
        public async Task<IActionResult> GetInfo([FromQuery] GetAllModuloXRolQuery getAllModuloXRolQuery)
        {
            var res = await Mediator.Send(getAllModuloXRolQuery);
            return Ok(res);
        }
    }
}
