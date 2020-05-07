using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco> UpdateAsync(Endereco model);
        Task<Endereco> AddAsync(Endereco model);
        Task<Endereco> GetAsync(Guid id);
        Task<IEnumerable<Endereco>> GetAllAsync(int pageIndex = 0);
    }
}