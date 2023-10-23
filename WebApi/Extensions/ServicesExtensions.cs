using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Repositories.EfCore;

namespace WebApi.Extensions;

public static class ServicesExtensions
{
    public static void ConfigurationSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

}
