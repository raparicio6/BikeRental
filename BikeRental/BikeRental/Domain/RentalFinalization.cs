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

        public IRentalOperator RentalOperatorWhoFinalizedRental { get; }

        public DateTime Date { get; }

        public RentalFinalization(IClient clientWhoIsFinalizingRental, IRentalOperator rentalOperatorWhoIsFinalizingRental)
        {
            this.ClientWhoFinalizedRental = clientWhoIsFinalizingRental;
            this.RentalOperatorWhoFinalizedRental = rentalOperatorWhoIsFinalizingRental;
            this.Date = DateTime.Now;
        }

    }
}
