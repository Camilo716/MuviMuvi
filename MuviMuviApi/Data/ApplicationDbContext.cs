using Microsoft.EntityFrameworkCore;
using MuviMuviApi.Models;

namespace MuviMuviApi.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Genre> Genres { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}