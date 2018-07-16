using BikeRental.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public abstract class RentalModality
    {
        public abstract UnitOfTime UnitOfTime { get; }

        public Money CostPerUnitOfTime { get; } 

        public RentalModality(Money costPerUnitOfTime)
        {
            this.CostPerUnitOfTime = costPerUnitOfTime;
        }

        public abstract Money CalculateRentalCost(DateTime rentalBeginningDate, DateTime rentalFinalizationDate);

        protected void ValidateDates(DateTime rentalBeginningDate, DateTime rentalFinalizationDate)
        {
            if (rentalFinalizationDate.CompareTo(rentalBeginningDate) <= 0)
                throw new FinalizationDateOfRentalLessThanBeginningDateException("The finalization date of the rental must be higher than the beginning date");
        }

    }
}
