using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using Vaquinha.MVC.Extensions;

namespace Vaquinha.MVC
{
    public class StartupWebTests
    {
        public StartupWebTests(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(env.ContentRootPath)
                     .AddJsonFile("appsettings.json")
                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                     .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 5000
            }, new NToastNotifyOption
            {
                DefaultSuccessTitle = "Yeah!",
                DefaultSuccessMessage = "Operação realizada com sucesso!",

                DefaultErrorTitle = "Ops!",
                DefaultErrorMessage = "Algo deu errado!"

            }).AddRazorRuntimeCompilation();

            services
                .AddDbContext()
                .AddIocConfiguration(Configuration)
                .AddAutoMapper(Configuration)
                .AddCustomConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseNToastNotify();            
        }
    }
}

