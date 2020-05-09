using Dapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Repository.Context;
using Vaquinha.Repository.Provider;

namespace Vaquinha.Repository
{
    public class DoacaoRepository : IDoacaoRepository
    {
        private readonly ILogger<DoacaoRepository> _logger;
        private readonly GloballAppConfig _globalSettings;
        private readonly VaquinhaOnLineDbConnectionProvider _convideDBConnectionProvider;
        private readonly VaquinhaOnlineDBContext _convideDBContext;

        public DoacaoRepository(GloballAppConfig globalSettings,
                                VaquinhaOnLineDbConnectionProvider convideDBConnectionProvider,
                                VaquinhaOnlineDBContext convideDBContext,
                                ILogger<DoacaoRepository> logger)
        {
            _globalSettings = globalSettings;
            _convideDBConnectionProvider = convideDBConnectionProvider;
            _convideDBContext = convideDBContext;
            _logger = logger;
        }

        public async Task<Doacao> AdicionarAsync(Doacao model)
        {
            var doacao = await _convideDBContext.Doacoes.AddAsync(model);
            await _convideDBContext.SaveChangesAsync();

            return doacao.Entity;
        }

        public async Task<IEnumerable<Doacao>> RecuperarDoadoesAsync(int pageIndex = 0)
        {
            using var conn = _convideDBConnectionProvider.GetConnection();

            _logger.LogInformation(conn.ConnectionString);

            return await conn.QueryAsync<Doacao, Pessoa, Doacao>(GetAllAsync_query,
                                 (doacao, pessoa) =>
                                 {
                                     doacao.AdicionarPessoa(pessoa);
                                     return doacao;

                                 }, new { PageSize = _globalSettings.QuantidadeRegistrosPaginacao, Offset = pageIndex * _globalSettings.QuantidadeRegistrosPaginacao }, splitOn: "PessoaId");
        }

        private readonly static string GetAllAsync_query = @"
                                                            SELECT D.[Id]
                                                                  ,D.[DadosPessoaisId]
                                                                  ,D.[EnderecoCobrancaId]
                                                                  ,D.[Valor]
                                                                  ,D.[DataHora]
                                                                  ,P.[Id] as PessoaId
                                                                  ,P.[Anonima]
                                                                  ,P.[MensagemApoio]
	                                                              ,P.[Nome]
                                                                  ,P.[Email]
                                                            FROM [Doacao] AS D
                                                            INNER JOIN [Pessoa] AS P ON D.[DadosPessoaisId] = P.[Id]
                                                            ORDER BY D.[DataHora] DESC 
                                                            OFFSET @Offset ROWS 
                                                            FETCH NEXT @PageSize ROWS ONLY;";
    }
}