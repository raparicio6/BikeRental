using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Cashier : ICashier
    {
        public string Name
        {
            get { return "Cashier"; }
        }

        public Payment ChargePurchase(IClient payingClient, ISale purchase, Money money)
        {
            throw new NotImplementedException();
        }
    }
}
