using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using Vaquinha.Domain;
using Vaquinha.Repository;
using Vaquinha.Repository.Context;
using Vaquinha.Service;
using Vaquinha.Service.AutoMapper;

namespace Vaquinha.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
                .AddDatabaseSetup()
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

    public static class StartupExtensions
    {
        public static IServiceCollection AddDatabaseSetup(this IServiceCollection services)
        {
            services.AddDbContext<VaquinhaOnlineDBContext>(opt => opt.UseInMemoryDatabase("VaquinhaOnLineDIO"));

            return services;
        }

        public static IServiceCollection AddIocConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICausaService, CausaService>();
            services.AddScoped<IHomeInfoService, HomeInfoService>();

            services.AddScoped<IDomainNotificationService, DomainNotificationService>();
            services.AddScoped<IDoacaoService, DoacaoService>();
            services.AddScoped<IDoacaoRepository, DoacaoRepository>();

            services.AddScoped<ICausaRepository, CausaRepository>();
            services.AddScoped<IHomeInfoRepository, HomeInfoRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var globalAppSettings = new GloballAppConfig();
            configuration.Bind("ConfiguracoesGeralAplicacao", globalAppSettings);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VaquinhaOnLineMappingProfile>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new GloballAppConfig();

            configuration.Bind("ConfiguracoesGeralAplicacao", config);
            services.AddSingleton(config);

            return services;
        }
    }
}

