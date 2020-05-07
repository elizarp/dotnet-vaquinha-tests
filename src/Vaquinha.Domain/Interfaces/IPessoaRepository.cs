using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task<Pessoa> UpdateAsync(Pessoa model);
        Task<Pessoa> AddAsync(Pessoa model);
        Task<Pessoa> GetAsync(Guid id);
        Task<IEnumerable<Pessoa>> GetAllAsync(int pageIndex = 0);
    }
}