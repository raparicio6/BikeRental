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

        public FamilyRental CreateFamilyRental(IClient client, IList<Bike> bikes, IList<UnitOfTime> unitsOfTime)
        {
            throw new NotImplementedException();
        }

        public Rental CreateRental(IClient client, Bike bike, UnitOfTime unitOfTime)
        {
            throw new NotImplementedException();
        }

        public RentalFinalization FinishRental(IClient client, IRental rental)
        {
            throw new NotImplementedException();
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
