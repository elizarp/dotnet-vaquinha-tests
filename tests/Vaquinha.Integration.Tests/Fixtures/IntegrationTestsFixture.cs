using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using Vaquinha.Domain;
using Vaquinha.Integration.Tests.Config;
using Vaquinha.MVC;
using Xunit;

namespace Vaquinha.Integration.Tests.Fixtures
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupWebTests>>
    {
    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public HttpClient Client;
        public IConfigurationRoot Configuration;
        public GloballAppConfig ConfiguracaoGeralAplicacao;
        public readonly VaquinhaAppFactory<TStartup> Factory;

        public IntegrationTestsFixture()
        {
            var clientOption = new WebApplicationFactoryClientOptions
            {
            };

            Factory = new VaquinhaAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOption);
            Configuration = GetConfiguration();

            ConfiguracaoGeralAplicacao = BuildGlobalAppConfiguration();
        }

        private GloballAppConfig BuildGlobalAppConfiguration()
        {   
            var globalAppSettings = new GloballAppConfig();
            Configuration.Bind("ConfiguracoesGeralAplicacao", globalAppSettings);

            return globalAppSettings;
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        private IConfigurationRoot GetConfiguration()
        {
            var workingDir = Directory.GetCurrentDirectory();

            return new ConfigurationBuilder()
                      .SetBasePath(workingDir)
                      .AddJsonFile("appsettings.json")
                      .AddJsonFile("appsettings.Testing.json")
                      .Build();
        }
    }
}
