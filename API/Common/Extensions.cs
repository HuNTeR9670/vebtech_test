using Microsoft.EntityFrameworkCore;

namespace VebtechTest.API.Common;

public static class Extensions
{
    public static void AddAppDbContext<TContext>(this WebApplicationBuilder builder) where TContext : DbContext
    {
        builder.Services.AddDbContext<TContext>(options =>
        {
            options.UseNpgsql(builder.Configuration["ConnectionString"],
                npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                });
        });
    }

}
