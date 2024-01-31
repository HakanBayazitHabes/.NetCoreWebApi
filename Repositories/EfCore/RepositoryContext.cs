using System.Reflection;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EfCore.Config;

namespace Repositories.EfCore;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); //Kimlik çerçevesi için gereken şemayı yapılandırır.
        //modelBuilder.ApplyConfiguration(new BookConfig());
        //modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // IEntityTypeConfiguration ifadesini kullanılanları doğrudan burada toplamış olucaz 
    }
}
