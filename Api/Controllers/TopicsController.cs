using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Hello()
    {
        return await Task.FromResult(Ok(new {text = "Hello World"}));
    }
}