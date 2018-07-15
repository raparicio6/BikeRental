using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalFinalization
    {
        public IClient ClientWhoFinisedhRental { get; }

        public IRentalOperator RentalOperatorWhoFinalized { get; }

        public DateTime CreationDate { get; }

        public RentalFinalization(IClient clientWhoIsFinishingRental, IRentalOperator rentalOperatorWhoIsFinalizing)
        {
            this.ClientWhoFinisedhRental = clientWhoIsFinishingRental;
            this.RentalOperatorWhoFinalized = rentalOperatorWhoIsFinalizing;
            this.CreationDate = DateTime.Now;
        }

    }
}
