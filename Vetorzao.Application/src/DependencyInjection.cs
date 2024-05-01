using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vetorzao.Application.Infra;
using Vetorzao.UI.Infra.Context;

namespace Vetorzao.Application;


public static class DependencyInjection
{
    public static IServiceCollection AddAll(this IServiceCollection services)
    => services
    .AddDatabase()
    .AddTransient<EmbeddingProvider>()
    ;

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var connectionString = "Server=localhost;Port=5432;Database=vectozao;User Id=postgres;Password=postgres;";

        services.AddDbContext<ApplicationDbContext>(x =>
            x.UseNpgsql(connectionString, o =>
            {
                o.MigrationsAssembly("Vetorzao.API");

                o.UseVector();
            })
        );

        return services;
    }
}
