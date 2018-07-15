using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class FamilyRental : ISale, IPromotion
    {
        public IClient Client => throw new NotImplementedException();

        public Money Money => throw new NotImplementedException();

        public Payment Payment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PromotionRules PromotionRules => throw new NotImplementedException();

        public bool IsPaid()
        {
            throw new NotImplementedException();
        }
    }
}
