using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vaquinha.Domain;
using Vaquinha.Repository;
using Vaquinha.Repository.Context;
using Vaquinha.Service;
using Vaquinha.Service.AutoMapper;

namespace Vaquinha.MVC.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
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
