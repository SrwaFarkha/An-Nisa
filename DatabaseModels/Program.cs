using DatabaseModels.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("db_appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Database connection string 'DefaultConnection' is missing or empty.");
        }

        services.AddDbContext<AnContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                x =>
                {
                    x.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Ignore);
                })
        );

        // Register other services here
    });

using (var host = builder.Build())
{
    using (var serviceScope = host.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        var dbContext = services.GetRequiredService<AnContext>();

        try
        {
            dbContext.Database.Migrate();
            Console.WriteLine("Database migration applied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
        }
    }

    host.Run();
}