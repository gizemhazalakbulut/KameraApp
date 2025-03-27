using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("cors-check")]
    public IActionResult CheckCors()
    {
        return Ok(new { status = "CORS OK", time = DateTime.Now });
    }
}
