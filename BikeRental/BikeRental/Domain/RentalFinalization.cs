using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalFinalization
    {
        public IClient ClientWhoFinalizedRental { get; }

        public IRentalOperator RentalOperatorWhoFinalized { get; }

        public DateTime DateOfFinalization { get; }

        public RentalFinalization(IClient clientWhoIsFinalizingRental, IRentalOperator rentalOperatorWhoIsFinalizing)
        {
            this.ClientWhoFinalizedRental = clientWhoIsFinalizingRental;
            this.RentalOperatorWhoFinalized = rentalOperatorWhoIsFinalizing;
            this.DateOfFinalization = DateTime.Now;
        }

    }
}
