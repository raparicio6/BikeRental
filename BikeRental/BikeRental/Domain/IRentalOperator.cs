using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public interface IRentalOperator : Role
    {
        RentalFinalization FinishRental(IClient client, IRental rental);
        Rental CreateRental(IClient client, Bike bike, UnitOfTime unitOfTime);
        FamilyRental CreateFamilyRental(IClient client, IList<Bike> bikes, IList<UnitOfTime> unitsOfTime);

        void UpdateCurrentFamilyRentalInformation(FamilyRentalInformation updatedFamilyRentalInformation);
        FamilyRentalInformation CurrentFamilyRentalInformation { get; }

        void UpdateCurrentRentalByHourModality(RentalByHour updatedRentalByHour);
        void UpdateCurrentRentalByDayModality(RentalByDay updatedRentalByDay);
        void UpdateCurrentRentalByWeekModality(RentalByWeek updatedRentalByWeek);
        RentalByHour CurrentRentalByHourModality { get; }
        RentalByDay CurrentRentalByDayModality { get; }
        RentalByWeek CurrentRentalByWeekModality { get; }

    }
}
