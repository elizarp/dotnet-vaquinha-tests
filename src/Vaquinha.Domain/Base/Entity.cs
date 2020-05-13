using FluentValidation.Results;
using System;
using System.Linq;

namespace Vaquinha.Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }
        public string[] ErrorMessages => ValidationResult?.Errors?.Select(a => a.ErrorMessage)?.ToArray();

        public abstract bool Valido();
    }
}