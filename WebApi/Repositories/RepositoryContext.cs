using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi;

public class RepositoryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
}
