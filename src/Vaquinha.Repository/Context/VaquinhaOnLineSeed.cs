using System;
using System.Collections.Generic;
using System.Linq;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Repository.Context
{
    public class VaquinhaOnLineSeed
    {
        public static void Seed(VaquinhaOnlineDBContext context)
        {
            if (!context.Causas.Any())
            {
                var causas = new List<Causa> {
                    new Causa(Guid.NewGuid(), "Santa Casa", "Araraquara", "SP"),
                    new Causa(Guid.NewGuid(), "Amigos do bem", "Araraquara", "SP"),
                    new Causa(Guid.NewGuid(), "A Passos", "São Carlos", "SP")
                };

                context.AddRange(causas);
                context.SaveChanges();
            }
        }
    }
}
