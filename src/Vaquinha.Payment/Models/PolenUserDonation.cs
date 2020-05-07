using System;
using System.Collections.Generic;

namespace Vaquinha.Payment.Models
{
    public class PolenUserDonation
    {
        public PolenUserDonation(PolenCause cause)
        {
            OrderId = Guid.NewGuid().ToString();

            Donor = new PolenDonor();
            Address = null;
            CreditCardData = new PolenCreditCardData();
            
            Causes = new List<PolenCause> { cause };

            PaymentMethod = (int)PolenPaymentMethod.CreditCard;
        }

        public string Notes { get; set; }

        public bool IsTest { get; set; }

        public string StoreId { get; set; }

        public int PaymentMethod { get; set; }

        public string OrderId { get; set; }

        public string CampaignId { get; set; }

        public PolenDonor Donor { get; set; }

        public PolenCreditCardData CreditCardData { get; set; }

        public PolenAdress Address { get; set; }

        public List<PolenCause> Causes { get; set; }
    }
}
