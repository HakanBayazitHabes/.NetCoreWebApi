using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}
