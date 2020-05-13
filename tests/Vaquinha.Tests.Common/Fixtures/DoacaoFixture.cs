using Bogus;
using Bogus.DataSets;
using System;
using Xunit;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(DoacaoFixtureCollection))]
    public class DoacaoFixtureCollection : ICollectionFixture<DoacaoFixture>, ICollectionFixture<EnderecoFixture>, ICollectionFixture<CartaoCreditoFixture>
    {
    }

    public class DoacaoFixture
    {
        public DoacaoViewModel DoacaoModelValida()
        {
            var faker = new Faker<DoacaoViewModel>("pt_BR");

            const int MIN_VALUE = 1;
            const int MAX_VALUE = 500;
            const int DECIMALS = 2;

            faker.RuleFor(c => c.Valor, (f, c) => f.Finance.Amount(MIN_VALUE, MAX_VALUE, DECIMALS));
            
            var retorno = faker.Generate();

            retorno.DadosPessoais = PessoaModelValida();

            return retorno;
        }

        public Doacao DoacaoValida(bool emailInvalido = false, double? valor = 5, bool maxLenghField = false)
        {            
            var faker = new Faker<Doacao>("pt_BR");

            const int MIN_VALUE = 1;
            const int MAX_VALUE = 500;
            const int DECIMALS = 2;

            faker.CustomInstantiator(f => new Doacao(Guid.Empty, Guid.Empty, Guid.Empty, valor ?? (double)f.Finance.Amount(MIN_VALUE, MAX_VALUE, DECIMALS), 
                                                        PessoaValida(emailInvalido, maxLenghField), null, null));

            return faker.Generate();
        }

        public DoacaoViewModel DoacaoModelInvalidaValida()
        {
            return new DoacaoViewModel();
        }

        public Doacao DoacaoInvalida(bool doacaoAnonima = false)
        {
            var pessoaInvalida = new Pessoa(Guid.Empty, string.Empty, string.Empty, doacaoAnonima, string.Empty);
            return new Doacao(Guid.Empty, Guid.Empty, Guid.Empty, 0, pessoaInvalida, null, null );
        }

        public Pessoa PessoaValida(bool emailInvalido = false,bool maxLenghField = false)
        {            
            var pessoa = new Faker().Person;

            var faker = new Faker<Pessoa>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Pessoa(Guid.NewGuid(), pessoa.FullName, string.Empty, false, maxLenghField ? f.Lorem.Sentence(501) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate();
        }

        public PessoaViewModel PessoaModelValida(bool emailInvalido = false)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<PessoaViewModel>("pt_BR");

            faker.RuleFor(a => a.Nome, (f, c) => f.Name.FirstName(genero));
            faker.RuleFor(a => a.Email, (f, c) => emailInvalido ? "EMAIL_INVALIDO" : f.Internet.Email(c.Nome.ToLower(), c.Nome.ToLower()));

            return faker.Generate();
        }
    }
}