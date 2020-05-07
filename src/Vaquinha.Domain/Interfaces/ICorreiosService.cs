using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vaquinha.Domain.Models;

namespace Vaquinha.Domain
{
    public interface ICorreiosService
    {
        Task<EnderecoModel> RecuperarEnderecoAsync(string cep);
    }
}
