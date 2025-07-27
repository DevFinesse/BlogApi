using Microsoft.EntityFrameworkCore;
using Repository;

namespace BlogApi.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app) 
        { 
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using RepositoryContext dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
            dbContext.Database.Migrate();
        }
    }
}
