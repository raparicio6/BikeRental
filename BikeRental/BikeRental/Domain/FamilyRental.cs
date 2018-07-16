using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class FamilyRental : ISale, IPromotion
    {
        public IClient Client { get; }

        public IList<IRental> Rentals { get; }

        public FamilyRentalInformation Information { get; }

        public Payment Payment { get; set; }

        public Money Cost
        {
            get { return new Money (this.ApplyDiscount(this.Rentals.Sum(rental => rental.Cost.Amount)), 
                this.Rentals.First().Cost.TypeOfCurrency); }
        }

        public PromotionRules Rules
        {
            get { return this.Information.Rules; }
        }

        public FamilyRental(IClient client, FamilyRentalInformation information, IList<IRental> rentals)
        {
            this.Client = client;
            this.Information = information;
            this.Rentals = rentals;
            this.Payment = null;
        }

        public bool IsPaid()
        {
            return this.Payment != null;
        }

        private decimal ApplyDiscount(decimal amount)
        {
            return (amount * (100 - (decimal)this.Information.DiscountPercent)) / 100;
        }

    }
}
