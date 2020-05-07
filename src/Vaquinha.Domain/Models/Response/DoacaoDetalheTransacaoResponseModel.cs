namespace Vaquinha.Domain.Models.Response
{
    public class DoacaoDetalheTransacaoResponseModel : IConvideDezenoveDiagnostics
    {
        public long ElapsedMilliseconds { get; set; }

        public string Success { get; set; }
        public int? ErrorCode { get; set; }
        public string Description { get; set; }
        public bool? IsDuplicated { get; set; }

        public PolenCreditCardErro Creditcard_errors { get; set; }
        public DoacaoDetalheTransacaoPostResponseModel Data { get; set; }
    }

    public class DoacaoDetalheTransacaoPostResponseModel
    {
        public string Error { get; set; }

        public string InvoiceId { get; set; }
        public string InvoiceUrl { get; set; }

        public string Metodo { get; set; }
        public string OrderId { get; set; }
    }

    public class PolenCreditCardErro
    {
        public bool Valid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string SecurityCode { get; set; }
    }

    public enum PolenResponseStatus
    {
        InternalServerError = 1,
        ApiTokenInvalid = 2,
        CompanyNotFound = 3,
        NgoNotFound = 4,
        TransacaoNaoAprovada = 10,
        PossibleDuplicationDonation = 20,
        DonorNotFound = 21,

        CreditCardDataNotFound = 22,

        CreditCardInvalid = 23,

        CreditCardFirstNameInvalid = 5000,
        CreditCardLastNameInvalid = 5001,

        CreditCardMonthCodeInvalid = 5002,
        CreditCardYearInvalid = 5004,

        CreditCardCardNumberInvalid = 5005,        
        CreditCardSecurityCodeInvalid = 5006,
        
    }
}