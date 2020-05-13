using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using Xunit;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(PessoaFixtureCollection))]
    public class PessoaFixtureCollection : ICollectionFixture<PessoaFixture>
    {
    }

    public class PessoaFixture
    {
        public Pessoa PessoaModelValida(bool emailInvalido = false)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Pessoa>("pt_BR");

            faker.RuleFor(c => c.MensagemApoio, (f, c) => f.Lorem.Sentence(30));
            faker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName(genero));
            faker.RuleFor(c => c.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate();
        }

        public IEnumerable<Pessoa> PessoaModelValida(int qtd, bool emailInvalido = false)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Pessoa>("pt_BR");

            faker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName(genero));
            faker.RuleFor(c => c.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate(qtd);
        }

        public IEnumerable<Pessoa> PessoaValida(int qtd, bool emailInvalido = false, bool maxLenghField = false)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Pessoa>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Pessoa(Guid.NewGuid(), f.Name.FirstName(genero), string.Empty, false, maxLenghField ? f.Lorem.Sentence(300) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate(qtd);
        }

        public Pessoa PessoaValida(bool emailInvalido = false, bool maxLenghField = false)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Pessoa>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Pessoa(Guid.NewGuid(), f.Name.FirstName(genero), string.Empty, false, maxLenghField ? f.Lorem.Sentence(300) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate();
        }

        public Pessoa PessoaVazia()
        {
            return new Pessoa(Guid.Empty, string.Empty, string.Empty, false, string.Empty);
        }

        public Pessoa PessoaMaxLenth()
        {
            const string TEXTO_COM_MAIS_DE_150_CARACTERES = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            return new Pessoa(Guid.NewGuid(), TEXTO_COM_MAIS_DE_150_CARACTERES, TEXTO_COM_MAIS_DE_150_CARACTERES, false, "AA");
        }
    }
}