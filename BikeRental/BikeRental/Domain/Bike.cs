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

        public BikeSpecifications BikeSpecifications { get; }

        public BikeState BikeState { get; private set; }

        public IRental Rental { get; set; }

        public Bike(string identificationCode, BikeSpecifications bikeSpecifications)
        {
            this.IdentificationCode = identificationCode;
            this.BikeSpecifications = bikeSpecifications;
            this.BikeState = BikeState.Free;
            this.Rental = null;
        }

        public bool IsAvailable()
        {
            return this.BikeState == BikeState.Free;
        }

        public void ChangeState(BikeState bikeState)
        {
            this.BikeState = bikeState;
        }

    }
}
