using System.Collections.Generic;
using Vaquinha.Domain;

namespace Vaquinha.Payment.Models
{
    public class PolenCauseResponse : IConvideDezenoveDiagnostics
    {
        public long ElapsedMilliseconds { get; set; }

        public IEnumerable<PolenCauseGetResponse> Results { get; set; }
    }

    public class PolenCauseGetResponse
    {
        public string NgoName { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        public string NgoLogo { get; set; }

        public bool Active { get; set; }
    }
}