using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalEmission
    {
        public IRentalOperator RentalOperatorWhoEmitted { get; }

        public DateTime CreationDate { get; }

        public RentalEmission(IRentalOperator rentalOperatorWhoIsEmitting)
        {
            this.RentalOperatorWhoEmitted = rentalOperatorWhoIsEmitting; 
            this.CreationDate = DateTime.Now;
        }

    }
}
