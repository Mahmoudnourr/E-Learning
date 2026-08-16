using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            // Try to find appsettings.json in common locations
            string apiDir = null;
            var currentDir = Directory.GetCurrentDirectory();
            
            // Check various possible locations
            var possiblePaths = new[]
            {
                currentDir,
                Path.Combine(currentDir, "Api"),
                Path.Combine(currentDir, "..", "Api"),
                Path.Combine(currentDir, "E-Learning", "Api")
            };
            
            foreach (var path in possiblePaths)
            {
                var fullPath = Path.GetFullPath(path);
                var appSettingsPath = Path.Combine(fullPath, "appsettings.json");
                if (File.Exists(appSettingsPath))
                {
                    apiDir = fullPath;
                    break;
                }
            }
            
            if (apiDir == null)
            {
                throw new Exception($"Could not find appsettings.json. Searched in: {string.Join(", ", possiblePaths)}");
            }
            
            // Read connection string from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiDir)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("Infrastructure");
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
