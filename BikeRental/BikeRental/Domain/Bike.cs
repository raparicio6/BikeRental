using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Bike
    {
        public string IdentificationCode { get; }

        public BikeSpecifications Specifications { get; }

        public BikeState State { get; private set; }

        public IRental Rental { get; set; }

        public Bike(string identificationCode, BikeSpecifications specifications)
        {
            this.IdentificationCode = identificationCode;
            this.Specifications = specifications;
            this.State = BikeState.Free;
            this.Rental = null;
        }

        public bool IsAvailable()
        {
            return this.State == BikeState.Free;
        }

        public void ChangeState(BikeState state)
        {
            this.State = state;
        }

    }
}
