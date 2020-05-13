using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(EnderecoFixtureCollection))]
    public class EnderecoFixtureCollection : ICollectionFixture<EnderecoFixture>
    {
    }
    public class EnderecoFixture
    {
        public EnderecoViewModel EnderecoModelValido()
        {
            var endereco = new Faker().Address;

            var faker = new Faker<EnderecoViewModel>("pt_BR");

            faker.RuleFor(c => c.CEP, (f, c) => "14800-700");
            faker.RuleFor(c => c.Cidade, (f, c) => endereco.City());
            faker.RuleFor(c => c.Estado, (f, c) => endereco.StateAbbr());
            faker.RuleFor(c => c.TextoEndereco, (f, c) => endereco.StreetAddress());            

            return faker.Generate();
        }

        public Endereco EnderecoValido()
        {
            var endereco = new Faker("pt_BR").Address;
            
            var faker = new Faker<Endereco>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Endereco(Guid.NewGuid(), "14800-000", endereco.StreetAddress(false), string.Empty, endereco.City(), endereco.StateAbbr(), "16995811385", "100A"));

            return faker.Generate();
        }

        public Endereco EnderecoVazio()
        {
            return new Endereco(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public Endereco EnderecoCepTelefoneEstadoInvalido()
        {
            var endereco = new Faker("pt_BR").Address;

            var faker = new Faker<Endereco>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Endereco(Guid.NewGuid(), "14800-0000", endereco.StreetAddress(false), string.Empty, endereco.City(), endereco.State(), "169958113859", "2005"));

            return faker.Generate();
        }

        public Endereco EnderecoMaxLength()
        {
            const string TEXTO_COM_MAIS_DE_250_CARACTERES = "AHIUDHASHOIFJOASJPFPOKAPFOKPKQPOFKOPQKWPOFEMMVIMWPOVPOQWPMVPMQOPIPQMJEOIPFMOIQOIFMCOKQMEWVMOPMQEOMVOPMWQOEMVOWMEOMVOIQMOIVMQEHISUAHDUIHASIUHDIHASIUHDUIHIAUSHIDUHAIUSDQWMFMPEQPOGFMPWEMGVWEM CQPWEM,CPQWPMCEOWIMVOEWOINMMFWOIEMFOIMOIOWEMFOIEWMFOIWEMFOWEOIMF";

            var endereco = new Faker("pt_BR").Address;

            var faker = new Faker<Endereco>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Endereco(Guid.NewGuid(), "14800-000", TEXTO_COM_MAIS_DE_250_CARACTERES, TEXTO_COM_MAIS_DE_250_CARACTERES, TEXTO_COM_MAIS_DE_250_CARACTERES, endereco.StateAbbr(), "16995811385", "1234567"));

            return faker.Generate();
        }

    }
}
