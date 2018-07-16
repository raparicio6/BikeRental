using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalByWeek : RentalModality
    {
        public override UnitOfTime UnitOfTime
        {
            get { return UnitOfTime.Week; }
        }

        public RentalByWeek(Money CostPerUnitOfTime) : base(CostPerUnitOfTime)
        {
        }

        public override Money CalculateRentalCost(DateTime rentalBeginningDate, DateTime rentalFinalizationDate)
        {
            base.ValidateDates(rentalBeginningDate, rentalFinalizationDate);

            double weeks = rentalFinalizationDate.Subtract(rentalBeginningDate).TotalDays / 7;
            return new Money((decimal)weeks * this.CostPerUnitOfTime.Amount, this.CostPerUnitOfTime.TypeOfCurrency);
        }

    }
}
