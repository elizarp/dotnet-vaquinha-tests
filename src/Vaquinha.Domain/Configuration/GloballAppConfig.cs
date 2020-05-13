using System;

namespace Vaquinha.Domain
{
    public class GloballAppConfig
    {
        public int MetaCampanha { get; set; }
        public DateTime DataFimCampanha { get; set; }     
       
        public RetryPolicy Polly { get; set; }        
    }

    public class RetryPolicy
    {
        public int QuantidadeRetry { get; set; }
        public int TempoDeEsperaEmSegundos { get; set; }
    }
}