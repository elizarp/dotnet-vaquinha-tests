using System.Collections.Generic;

namespace Vaquinha.Payment.PolenEntities.Output
{
    public class CauseResponse
    {
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
