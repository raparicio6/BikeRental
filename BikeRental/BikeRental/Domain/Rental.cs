using BikeRental.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Rental : IRental
    {
        public IClient Client { get; }

        public RentalBeginning Beginning { get; }

        public Bike Bike { get; }

        public RentalModality Modality { get; }        

        public RentalFinalization Finalization { get; set; }

        public Payment Payment { get; set; }

        public Money Cost
        {
            get
            {
                if (this.Finalization == null)
                    throw new RentalHasNotFinalizedYetException("Rental has not finalized yet");
                return this.Modality.CalculateRentalCost(this.Beginning.Date,
                    this.Finalization.Date);
            }
        }

        public Rental(IClient client, RentalBeginning beginning, Bike bike, RentalModality modality)
        {
            this.Client = client;
            this.Beginning = beginning;
            this.Bike = bike;
            this.Modality = modality;
            this.Finalization = null;
            this.Payment = null;
        }
        
        public bool IsFinalized()
        {
            return this.Finalization != null;
        }

        public bool IsPaid()
        {
            return this.Payment != null;
        }

    }
}
