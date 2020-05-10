using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Repository.Context;

namespace Vaquinha.Repository
{
    public class CausaRepository : ICausaRepository
    {
        private readonly VaquinhaOnlineDBContext _dbContext;

        public CausaRepository(VaquinhaOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Causa> Adicionar(Causa causa)
        {
            await _dbContext.AddAsync(causa);
            await _dbContext.SaveChangesAsync();

            return causa;
        }

        public async Task<IEnumerable<Causa>> RecuperarCausas()
        {
            return await _dbContext.Causas.ToListAsync();
        }
    }
}