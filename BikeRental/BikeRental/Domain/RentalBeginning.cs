using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalBeginning
    {
        public IRentalOperator RentalOperatorWhoEmitted { get; }

        public DateTime DateOfBeginning { get; }

        public RentalBeginning(IRentalOperator rentalOperatorWhoIsEmitting)
        {
            this.RentalOperatorWhoEmitted = rentalOperatorWhoIsEmitting; 
            this.DateOfBeginning = DateTime.Now;
        }

    }
}
