using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class PromotionRules
    {
        public string TermsAndConditions { get; }
        public DateTime EffectiveDate { get; }
        public DateTime ExpirationDate { get; }

        public PromotionRules(string termsAndConditions, DateTime effectiveDate, DateTime expirationDate)
        {
            this.TermsAndConditions = termsAndConditions;
            this.EffectiveDate = effectiveDate;
            this.ExpirationDate = expirationDate;
        }

    }
}
