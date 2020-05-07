using System;
using System.Runtime.Serialization;

namespace Vaquinha.Payment.Models
{
    public class PolenCause
    {
        private double _donation { get; set; }
        public PolenCause(string causeId,
                          double donation)
        {
            CauseId = causeId;
            _donation = donation;
        }

        public string CauseId { get; set; }

        [DataMember(Name = "Donation")]
        public double Donation => Math.Round(_donation, 2);
    }    
}
