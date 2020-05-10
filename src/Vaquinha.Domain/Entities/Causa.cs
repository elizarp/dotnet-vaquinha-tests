using System;
using Vaquinha.Domain.Base;

namespace Vaquinha.Domain.Entities
{
    public class Causa : Entity
    {
        private Causa() { }

        public Causa(Guid id, string nome, string cidade, string estado)
        {
            Id = id;
            Nome = nome;
            Cidade = cidade;
            Estado = estado;
        }

        public string Nome { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public override bool Valido()
        {
            return true;
        }
    }
}
