using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalBeginning
    {
        public IRentalOperator RentalOperatorWhoBeganRental { get; }

        public DateTime Date { get; }

        public RentalBeginning(IRentalOperator rentalOperatorWhoIsBeginningRental)
        {
            this.RentalOperatorWhoBeganRental = rentalOperatorWhoIsBeginningRental; 
            this.Date = DateTime.Now;
        }

    }
}
