using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class DefaultController : ControllerBase
    {
        [HttpGet("/test")]
        public IActionResult Index()
        {
            return Ok(new { ok = true });
        }
    }
}
