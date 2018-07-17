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

        public Rental RequestARental(Bike bike, UnitOfTime unitOfTime, IRentalOperator rentalOperator)
        {
            return rentalOperator.ProvideRental(this, bike, unitOfTime);
        }

        public FamilyRental RequestAFamilyRental(IList<IClient> clientsOfTheRentals, IList<Bike> bikes, IList<UnitOfTime> unitsOfTime, IRentalOperator rentalOperator)
        {
            return rentalOperator.ProvideFamilyRental(this, clientsOfTheRentals, bikes, unitsOfTime);
        }        

        public RentalFinalization FinalizeRental(IRental rental, IRentalOperator rentalOperator)
        {
            return rentalOperator.FinalizeRental(this, rental);
        }        

        public Payment PayPurchase(ISale purchase, Money money, ICashier cashier)
        {
            return cashier.ChargePurchase(this, purchase, money);
        }

        public void AddRental(IRental rental)
        {
            this.Rentals.Add(rental);
        }

        public void AddFamilyRental(FamilyRental familyRental)
        {
            this.FamilyRentals.Add(familyRental);
        }

    }
}
