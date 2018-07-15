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

        public abstract Money CalculateRentalCost(DateTime rentalEmissionDate, DateTime rentalFinalizationDate);

        protected void ValidateDates(DateTime rentalEmissionDate, DateTime rentalFinalizationDate)
        {
            if (rentalFinalizationDate.CompareTo(rentalEmissionDate) <= 0)
                throw new FinalizationDateOfRentalLessThanEmissionDateException("The finalization date of the rental must be higher than the date of emission");
        }

    }
}
