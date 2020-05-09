namespace Vaquinha.Payment.PolenEntities.Enumns
{
    public enum DonationStatus
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