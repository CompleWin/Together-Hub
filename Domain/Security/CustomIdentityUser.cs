using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Security;

public class CustomIdentityUser : IdentityUser
{
    public FullName FullName { get; set; } = default!;
    public string About { get; set; } = default!;


    public List<Relationship> Topics = new();
}