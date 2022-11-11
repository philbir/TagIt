using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TagIt.Controller;

[Route("api/sharex")]
[ApiController]
public class ShareXController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        return Ok("OK");
    }
}
