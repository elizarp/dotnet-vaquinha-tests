using System.Collections.Generic;
using Vaquinha.Domain.Entities;
using Vaquinha.Repository.Context;

namespace Vaquinha.Integration.Tests
{
	public class Utils{
		public static void InitializeDbForTests(VaquinhaOnlineDBContext db)
        {
            db.Causas.AddRange(GetSeedingCausas());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(VaquinhaOnlineDBContext db)
        {
            db.Causas.RemoveRange(db.Causas);
            InitializeDbForTests(db);
        }

        public static List<Causa> GetSeedingCausas()
        {
            return new List<Causa>()
            {
                new Causa(System.Guid.NewGuid(),"ONU","Pequim","CH")
                
            };
        }
	}
}