using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vaquinha.MVC;
using Vaquinha.Repository.Context;

namespace Vaquinha.Integration.Tests
{
	public class TestingWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{

				var descriptor = services.SingleOrDefault(
				  d => d.ServiceType ==
					 typeof(DbContextOptions<VaquinhaOnlineDBContext>));

				if (descriptor != null)
				{
					services.Remove(descriptor);
				}

				var serviceProvider = new ServiceCollection()
				  .AddEntityFrameworkInMemoryDatabase()
				  .BuildServiceProvider();

				services.AddDbContext<VaquinhaOnlineDBContext>(options =>
				{
					options.UseInMemoryDatabase("InMemoryVaquinhaTest");
					options.UseInternalServiceProvider(serviceProvider);
				});

				var sp = services.BuildServiceProvider();

				using (var scope = sp.CreateScope())
				{
					using (var db = scope.ServiceProvider.GetRequiredService<VaquinhaOnlineDBContext>())
					{
						try
						{
							db.Database.EnsureCreated();
							// Seed the database with test data.
							Utils.InitializeDbForTests(db);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							throw;
						}
					}
				}
			});
		}
	}
}