using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public interface IRental : ISale
    {        
        RentalEmission RentalEmission { get; }
        Bike Bike { get; }
        UnitOfTime UnitOfTime { get; }
        RentalFinalization Finish();
        RentalFinalization RentalFinalization { get; }
        bool IsFinished();

    }
}
