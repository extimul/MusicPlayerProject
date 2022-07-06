using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.WebApp.Base.Controllers;

namespace MusicPlayer.API.Library.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/index")]
public class IndexController : BaseApiController
{
    [HttpGet("info")]
    public IActionResult GetInfo() => Ok(DateTime.Now.ToString(CultureInfo.InvariantCulture));
}