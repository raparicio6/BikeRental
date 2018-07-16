using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain.Exceptions
{
    public class RentalIsAlreadyFinalizedException : Exception
    {
        public RentalIsAlreadyFinalizedException(string message) : base(message)
        {
        }
    }
}
