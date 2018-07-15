using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class FamilyRentalInformation
    {
        public float DiscountPercent { get; }
        public int MinimumRentals { get; }
        public int MaximumRentals { get; }
        public PromotionRules PromotionRules { get; }

        public FamilyRentalInformation(float discountPercent, int minimumRentals, int maximumRentals, 
            PromotionRules promotionRules)
        {
            this.DiscountPercent = discountPercent;
            this.MinimumRentals = minimumRentals;
            this.MaximumRentals = maximumRentals;
            this.PromotionRules = promotionRules;
        }

    }
}
