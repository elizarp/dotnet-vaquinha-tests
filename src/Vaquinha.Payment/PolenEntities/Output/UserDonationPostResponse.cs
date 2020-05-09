namespace Vaquinha.Payment.PolenEntities.Output
{
    public class UserDonationPostResponse
    {
        public long ElapsedMilliseconds { get; set; }

        public string Success { get; set; }
        public int? ErrorCode { get; set; }
        public string Description { get; set; }
        public bool? IsDuplicated { get; set; }

        public CreditCardErrorPostResponse Creditcard_errors { get; set; }
        public UserDonationDetailPostResponse Data { get; set; }
    }

    public class UserDonationDetailPostResponse
    {
        public string Error { get; set; }

        public string InvoiceId { get; set; }
        public string InvoiceUrl { get; set; }

        public string Metodo { get; set; }
        public string OrderId { get; set; }
    }

    public class CreditCardErrorPostResponse
    {
        public bool Valid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string SecurityCode { get; set; }
    }
}
