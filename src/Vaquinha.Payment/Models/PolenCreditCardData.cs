namespace Vaquinha.Payment.Models
{
    public class PolenCreditCardData
    {
        public PolenCreditCardData()
        {
            InstallmentQuantity = 1;
            PaymentSystem = (int)Models.PaymentSystem.Default;
        }

        public int PaymentSystem { get; set; }

        public string FullName { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        public int InstallmentQuantity { get; set; }
    }
}
