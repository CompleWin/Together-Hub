using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    DbSet<Topic> Topics { get; }
}