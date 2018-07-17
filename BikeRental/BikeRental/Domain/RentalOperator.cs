using BikeRental.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class RentalOperator : IRentalOperator
    {
        private const string ROLE_NAME = "RentalOperator";

        public FamilyRentalInformation CurrentFamilyRentalInformation { get; private set; }

        public RentalByHour CurrentRentalByHourModality { get; private set; }

        public RentalByDay CurrentRentalByDayModality { get; private set; }

        public RentalByWeek CurrentRentalByWeekModality { get; private set; }

        public string RoleName
        {
            get { return ROLE_NAME; }            
        }

        public RentalOperator(FamilyRentalInformation updatedFamilyRentalInformation, 
            RentalByHour updatedRentalByHour, RentalByDay updatedRentalByDay, RentalByWeek updatedRentalByWeek)
        {
            this.CurrentFamilyRentalInformation = updatedFamilyRentalInformation;
            this.CurrentRentalByHourModality = updatedRentalByHour;
            this.CurrentRentalByDayModality = updatedRentalByDay;
            this.CurrentRentalByWeekModality = updatedRentalByWeek;
        }

        public Rental ProvideRental(IClient client, Bike bike, UnitOfTime unitOfTime)
        {
            if (!bike.IsAvailable())
                throw new BikeIsNotAvailableException("Bike is not available");

            RentalModality rentalModality;           

            switch (unitOfTime)
            {
                case UnitOfTime.Hour:
                    rentalModality = this.CurrentRentalByHourModality;
                    break;
                case UnitOfTime.Day:
                    rentalModality = this.CurrentRentalByDayModality;
                    break;
                case UnitOfTime.Week:
                    rentalModality = this.CurrentRentalByWeekModality;
                    break;
                default:
                    throw new UnitOfTimeIsNotValidException("Unit of time is not valid");                   
            }

            Rental rental = new Rental(client, new RentalBeginning(this), bike, rentalModality);

            bike.ChangeState(BikeState.In_Use);
            bike.Rental = rental;

            client.AddRental(rental);

            return rental;
        }

        private void ValidateAmounts(IList<IClient> clientsOfTheRentals, IList<Bike> bikes, IList<UnitOfTime> unitsOfTime)
        {
            if (clientsOfTheRentals.Count != bikes.Count || clientsOfTheRentals.Count != unitsOfTime.Count)
                throw new AmountOfClientsAndBikesAndUnitsOfTimeDoNotMatchException("The amount of clients, bikes and units of time does not match");

            if (clientsOfTheRentals.Count < this.CurrentFamilyRentalInformation.MinimumRentals
                || clientsOfTheRentals.Count > this.CurrentFamilyRentalInformation.MaximumRentals)
                throw new AmountOfRentalsWantedDoesNotRespectTheEstablishedMarginException("The amount of rentals wanted does not respect the established margin");

        }

        // Pre condition: The clientsOfTheRentals, bikes and unitsOfTime ILists must be ordered contemplating that elements of the same position are for the same rental
        public FamilyRental ProvideFamilyRental(IClient clientWhoRequests, IList<IClient> clientsOfTheRentals, IList<Bike> bikes, IList<UnitOfTime> unitsOfTime)
        {
            this.ValidateAmounts(clientsOfTheRentals, bikes, unitsOfTime);

            IList<IRental> rentals = new List<IRental>();

            for (int i = 0; i < clientsOfTheRentals.Count; i++)            
                rentals.Add(this.ProvideRental(clientsOfTheRentals[i], bikes[i], unitsOfTime[i]));            

            return new FamilyRental(clientWhoRequests, this.CurrentFamilyRentalInformation, rentals);
        }       

        public RentalFinalization FinalizeRental(IClient client, IRental rental)
        {
            if (rental.IsFinalized())
                throw new RentalIsAlreadyFinalizedException("Rental is already finalized");

            RentalFinalization rentalFinalization = new RentalFinalization(client, this);
            rental.Finalization = rentalFinalization;

            Bike bike = rental.Bike;
            bike.ChangeState(BikeState.Free);
            bike.Rental = null;

            return rentalFinalization;
        }               

        public void UpdateCurrentFamilyRentalInformation(FamilyRentalInformation updatedFamilyRentalInformation)
        {
            this.CurrentFamilyRentalInformation = updatedFamilyRentalInformation;
        }

        public void UpdateCurrentRentalByHourModality(RentalByHour updatedRentalByHour)
        {
            this.CurrentRentalByHourModality = updatedRentalByHour;
        }

        public void UpdateCurrentRentalByDayModality(RentalByDay updatedRentalByDay)
        {
            this.CurrentRentalByDayModality = updatedRentalByDay;
        }        

        public void UpdateCurrentRentalByWeekModality(RentalByWeek updatedRentalByWeek)
        {
            this.CurrentRentalByWeekModality = updatedRentalByWeek;
        }
        
    }
}
