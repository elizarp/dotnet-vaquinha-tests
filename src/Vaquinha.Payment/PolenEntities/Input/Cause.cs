using System;
using System.Runtime.Serialization;

namespace Vaquinha.Payment.PolenEntities.Input
{
    public class Cause
    {
        private double _donation { get; set; }

        public Cause(string causeId, double donation)
        {
            CauseId = causeId;
            _donation = donation;
        }

        public string CauseId { get; set; }

        [DataMember(Name = "Donation")]
        public double Donation => Math.Round(_donation, 2);
    }
}
