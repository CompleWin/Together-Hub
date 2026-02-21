using System.Security.Claims;
using Application.Security.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security.Services;

public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    public string GetUsername()
    {
        return httpContextAccessor
            .HttpContext!
            .User
            .FindFirstValue("name")!;
    }
}