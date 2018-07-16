using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalByDay : RentalModality
    {
        public override UnitOfTime UnitOfTime
        {
            get { return UnitOfTime.Day; }
        }

        public RentalByDay(Money CostPerUnitOfTime) : base(CostPerUnitOfTime)
        {
        }

        public override Money CalculateRentalCost(DateTime rentalBeginningDate, DateTime rentalFinalizationDate)
        {
            base.ValidateDates(rentalBeginningDate, rentalFinalizationDate);

            double days = rentalFinalizationDate.Subtract(rentalBeginningDate).TotalDays;
            return new Money((decimal)days * this.CostPerUnitOfTime.Amount, this.CostPerUnitOfTime.TypeOfCurrency);
        }

    }
}
