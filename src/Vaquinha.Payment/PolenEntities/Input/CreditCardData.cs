namespace Vaquinha.Payment.PolenEntities.Input
{
    public class CreditCardData
    {
        public CreditCardData()
        {
            InstallmentQuantity = 1;
            PaymentSystem = (int)Enumns.PaymentSystem.Default;
        }

        public int PaymentSystem { get; set; }

        public string FullName { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        public int InstallmentQuantity { get; set; }
    }
}