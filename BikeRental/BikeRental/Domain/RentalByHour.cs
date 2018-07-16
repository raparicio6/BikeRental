using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalByHour : RentalModality
    {
        public override UnitOfTime UnitOfTime
        {
            get { return UnitOfTime.Hour; }
        }

        public RentalByHour(Money CostPerUnitOfTime) : base(CostPerUnitOfTime)
        {
        }

        public override Money CalculateRentalCost(DateTime rentalEmissionDate, DateTime rentalFinalizationDate)
        {
            base.ValidateDates(rentalEmissionDate, rentalFinalizationDate);

            double hours = rentalFinalizationDate.Subtract(rentalEmissionDate).TotalHours;
            return new Money((decimal)hours * this.CostPerUnitOfTime.Amount, this.CostPerUnitOfTime.TypeOfCurrency);
        }

    }
}
