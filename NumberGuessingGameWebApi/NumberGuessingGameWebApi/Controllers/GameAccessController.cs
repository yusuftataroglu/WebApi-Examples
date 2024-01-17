using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NumberGuessingGameWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="BasicAuth")]
    public class GameAccessController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetGame()
        {
            return Ok();
        }
    }
}
