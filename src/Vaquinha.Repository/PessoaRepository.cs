using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Interfaces;
using Vaquinha.Repository.Context;
using Vaquinha.Repository.Provider;

namespace Vaquinha.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly GloballAppConfig _globalSettings;
        private readonly ConvideDBConnectionProvider _convideDBConnectionProvider;
        private readonly ConvideDBContext _convideDBContext;

        public PessoaRepository(GloballAppConfig globalSettings, ConvideDBConnectionProvider convideDBConnectionProvider, ConvideDBContext convideDBContext)
        {
            _globalSettings = globalSettings;
            _convideDBConnectionProvider = convideDBConnectionProvider;
            _convideDBContext = convideDBContext;
        }

        public async Task<Pessoa> AddAsync(Pessoa model)
        {
            var pessoa = await _convideDBContext.Pessoas.AddAsync(model);
            await _convideDBContext.SaveChangesAsync();

            return pessoa.Entity;
        }

        public async Task<Pessoa> UpdateAsync(Pessoa model)
        {
            var pessoa = _convideDBContext.Pessoas.Update(model);
            await _convideDBContext.SaveChangesAsync();

            return pessoa.Entity;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync(int pageIndex = 0)
        {
            using var conn = _convideDBConnectionProvider.GetConnection();

            return await conn.QueryAsync<Pessoa>(GetAllAsync_query, new { PageSize = _globalSettings.QuantidadeRegistrosPaginacao, Offset = pageIndex * _globalSettings.QuantidadeRegistrosPaginacao });
        }

        public async Task<Pessoa> GetAsync(Guid id)
        {
            return await _convideDBContext.Pessoas.FindAsync(id);
        }

        private readonly static string GetAllAsync_query = @"
SELECT [Id] 
      ,[Nome] 
      ,[Email] 
      ,[MensagemApoio] 
      ,[Anonima] 
FROM [Pessoa] 
ORDER BY [Id] 
OFFSET @Offset ROWS 
FETCH NEXT @PageSize ROWS ONLY; 
";
    }
}