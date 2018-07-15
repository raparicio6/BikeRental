using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Rental : IRental
    {
        public RentalEmission RentalEmission => throw new NotImplementedException();

        public Bike Bike => throw new NotImplementedException();

        public UnitOfTime UnitOfTime => throw new NotImplementedException();

        public RentalFinalization RentalFinalization => throw new NotImplementedException();

        public IClient Client => throw new NotImplementedException();

        public Money Money => throw new NotImplementedException();

        public Payment Payment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RentalFinalization Finish()
        {
            throw new NotImplementedException();
        }

        public bool IsFinished()
        {
            throw new NotImplementedException();
        }

        public bool IsPaid()
        {
            throw new NotImplementedException();
        }
    }
}
