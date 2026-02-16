using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAuthController : ControllerBase
{
    [HttpGet("test1")]
    public async Task<IResult> Test1()
    {
        await Task.CompletedTask;
        return Results.Ok(new {result = "test1 ok"});
    }
    
    [HttpGet("test2")]
    public async Task<IResult> Test2()
    {
        await Task.CompletedTask;
        return Results.Ok(new {result = "test2 ok"});
    }
}