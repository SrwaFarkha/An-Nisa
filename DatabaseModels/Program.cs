using DatabaseModels.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("db_appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
    // Configure MySQL as the database provider
    services.AddDbContext<AnContext>(options =>
        options.UseMySql(
            hostContext.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(hostContext.Configuration.GetConnectionString("DefaultConnection")),
            x =>
            {
                x.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Ignore);
            })
        );

        // Register other services here
        // services.AddTransient<YourService>();
    });

using (var host = builder.Build())
{
    using (var serviceScope = host.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        var dbContext = services.GetRequiredService<AnContext>();

        // Apply any pending migrations
        dbContext.Database.Migrate();
    }

    // Run the application
    host.Run();
}
