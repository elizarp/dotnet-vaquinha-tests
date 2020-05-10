using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Repository.Context;

namespace Vaquinha.Repository
{
    public class InstituicaoRepository : IInstituicaoRepository
    {
        private readonly VaquinhaOnlineDBContext _dbContext;

        public InstituicaoRepository(VaquinhaOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Instituicao>> RecuperarInstituicoes()
        {
            return await _dbContext.Instituicoes.ToListAsync();
        }
    }
}