using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Client : IClient
    {
        private const string ROLE_NAME = "Client";

        public IList<IRental> Rentals { get; }

        public IList<FamilyRental> FamilyRentals { get; }

        public string RoleName
        {
            get { return ROLE_NAME; }
        }

        public Client()
        {
            this.Rentals = new List<IRental>();
            this.FamilyRentals = new List<FamilyRental>();
        }        

        public RentalFinalization FinalizeRental(IRental rental, IRentalOperator rentalOperator)
        {
            throw new NotImplementedException();
        }

        public FamilyRental RequestAFamilyRental(IList<IClient> clientsOfTheRentals, IList<Bike> bikes, IList<UnitOfTime> unitsOfTime, IRentalOperator rentalOperator)
        {
            throw new NotImplementedException();
        }

        public Rental RequestARental(Bike bike, UnitOfTime unitOfTime, IRentalOperator rentalOperator)
        {
            throw new NotImplementedException();
        }

        public void AddRental(IRental rental)
        {
            this.Rentals.Add(rental);
        }

        public void AddFamilyRental(FamilyRental familyRental)
        {
            this.FamilyRentals.Add(familyRental);
        }

        public Payment PayPurchase(ISale purchase, Money money, ICashier cashier)
        {
            throw new NotImplementedException();
        }

    }
}
