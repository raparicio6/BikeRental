using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public interface IClient : Role
    {        
        Rental RequestARental(Bike bike, UnitOfTime unitOfTime, IRentalOperator rentalOperator);
        FamilyRental RequestAFamilyRental(IList<IClient> clientsOfTheRentals, IList<Bike> bikes, IList<UnitOfTime> unitsOfTime, IRentalOperator rentalOperator);
        RentalFinalization FinalizeRental(IRental rental, IRentalOperator rentalOperator);
        IList<IRental> Rentals { get; }
        IList<FamilyRental> FamilyRentals { get; }
        void AddRental(IRental rental);
        void AddFamilyRental(FamilyRental familyRental);

        Payment PayPurchase(ISale purchase, Money money, ICashier cashier);

    }
}
