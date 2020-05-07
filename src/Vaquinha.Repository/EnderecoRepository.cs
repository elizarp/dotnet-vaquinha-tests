using System;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Interfaces;
using Vaquinha.Repository.Context;
using Vaquinha.Repository.Provider;

namespace Vaquinha.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GloballAppConfig _globalSettings;
        private readonly ConvideDBConnectionProvider _convideDBConnectionProvider;
        private readonly ConvideDBContext _convideDBContext;

        public EnderecoRepository(GloballAppConfig globalSettings, ConvideDBConnectionProvider convideDBConnectionProvider, ConvideDBContext convideDBContext)
        {
            _globalSettings = globalSettings;
            _convideDBConnectionProvider = convideDBConnectionProvider;
            _convideDBContext = convideDBContext;
        }

        public async Task<Endereco> AddAsync(Endereco model)
        {
            var endereco = await _convideDBContext.Enderecos.AddAsync(model);
            await _convideDBContext.SaveChangesAsync();

            return endereco.Entity;
        }

        public async Task<Endereco> UpdateAsync(Endereco model)
        {
            var endereco = _convideDBContext.Enderecos.Update(model);
            await _convideDBContext.SaveChangesAsync();

            return endereco.Entity;
        }

        public async Task<IEnumerable<Endereco>> GetAllAsync(int pageIndex = 0)
        {
            if (pageIndex == 0) pageIndex++;

            using var conn = _convideDBConnectionProvider.GetConnection();

            return await conn.QueryAsync<Endereco>(GetAllAsync_query, new { PageSize = _globalSettings.QuantidadeRegistrosPaginacao, Offset = pageIndex * _globalSettings.QuantidadeRegistrosPaginacao });
        }

        public async Task<Endereco> GetAsync(Guid id)
        {
            return await _convideDBContext.Enderecos.FindAsync(id);
        }

        private readonly static string GetAllAsync_query = @"
SELECT [Id] 
      ,[CEP]
      ,[TextoEndereco]
      ,[Complemento]
      ,[Cidade]
      ,[Estado]
      ,[Telefone]
  FROM [Endereco]
ORDER BY [Id] 
OFFSET @Offset ROWS 
FETCH NEXT @PageSize ROWS ONLY; 
";
    }
}