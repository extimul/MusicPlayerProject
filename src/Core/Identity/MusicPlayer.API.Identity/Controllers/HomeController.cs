using Microsoft.AspNetCore.Mvc;

namespace MusicPlayer.API.Identity.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/home")]
public class HomeController : ControllerBase
{
    [HttpGet("info")]
    public IActionResult GetInfo()
    {
        return Ok(DateTime.Now);
    }
}