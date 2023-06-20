using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder()
	.ConfigureAppConfiguration((hostContext, config) =>
	{
		config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
	})
	.ConfigureServices((hostContext, services) =>
	{
		services.AddDbContext<AnContext>(options =>
			options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

		// Register other services
		// ...
	});

using (var host = builder.Build())
{
	using (var serviceScope = host.Services.CreateScope())
	{
		var services = serviceScope.ServiceProvider;
		var dbContext = services.GetRequiredService<AnContext>();

		dbContext.Database.Migrate();
	}

	host.Run();
}