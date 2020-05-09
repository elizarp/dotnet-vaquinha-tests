using System;
using System.Collections.Generic;

namespace Vaquinha.Payment.PolenEntities.Input
{
    public class UserDonation
    {
        public UserDonation(Cause cause)
        {
            OrderId = Guid.NewGuid().ToString();

            Donor = new Donor();
            CreditCardData = new CreditCardData();

            Causes = new List<Cause> { cause };

            PaymentMethod = (int)Enumns.PaymentMethod.CreditCard;
        }

        public string Notes { get; set; }

        public bool IsTest { get; set; }

        public string StoreId { get; set; }

        public int PaymentMethod { get; set; }

        public string OrderId { get; set; }

        public string CampaignId { get; set; }

        public Donor Donor { get; set; }

        public CreditCardData CreditCardData { get; set; }

        public List<Cause> Causes { get; set; }
    }
}
