using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Payment
{
    public class PolenPaymentService : IPaymentService
    {
        public Task AdicionadDoacaoAsync(DoacaoViewModel doacao)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstituicaoViewModel>> RecuperarInstituicoesAsync(int page = 0)
        {
            throw new NotImplementedException();
        }
    }
}
