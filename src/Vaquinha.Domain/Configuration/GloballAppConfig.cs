using System;

namespace Vaquinha.Domain
{
    public class GloballAppConfig
    {
        public bool IsTest { get; set; }

        public string ConvideTokenApi { get; set; }

        public string LinkCampanha { get; set; }
        public string ImagemEmailCampanha { get; set; }

        public int MetaCampanha { get; set; }
        public DateTime DataFimCampanha { get; set; }

        public string OrdenacaoPadrao { get; set; }
        public int QuantidadeRegistrosPaginacao { get; set; }

        public int CacheBuscaDoacoesEmSegundos { get; set; }
        public int CacheBuscaInstituicoesEmHoras { get; set; }
        public int CacheBuscaDadosTotaisEmSegundos { get; set; }

        public Polen Polen { get; set; }

        public Correio Correios { get; set; }

        public RetryPolicy Polly { get; set; }

        public Email Email { get; set; }
    }

    public class RetryPolicy
    {
        public int QuantidadeRetry { get; set; }
        public int TempoDeEsperaEmSegundos { get; set; }
    }

    public class Polen
    {
        public bool IsTest { get; set; }
        public string CampaignId { get; set; }
        public string StoreId { get; set; }

        public string ApiToken { get; set; }

        public string BaseUrlV1 { get; set; }
        public string BaseUrlV2 { get; set; }

        public string CauseId { get; set; }

        public string Notes { get; set; }
    }

    public class Correio
    {
        public string BuscaCepBaseUrl { get; set; }
    }

    public class Email
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
    }
}