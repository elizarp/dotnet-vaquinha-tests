using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;
using Vaquinha.Repository.Context;

namespace Vaquinha.Repository
{
    public class HomeInfoRepository : IHomeInfoRepository
    {
        private readonly VaquinhaOnlineDBContext _dbContext;

        public HomeInfoRepository(VaquinhaOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HomeViewModel> RecuperarDadosIniciaisHomeAsync()
        {
            var valorTotal = await _dbContext.Doacoes.SumAsync(a => a.Valor);
            return new HomeViewModel { ValorTotalArrecadado = valorTotal };
        }
    }
}