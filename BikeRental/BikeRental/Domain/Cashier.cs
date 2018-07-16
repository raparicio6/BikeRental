using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Cashier : ICashier
    {
        private const string ROLE_NAME = "Cashier";

        public string RoleName
        {
            get { return ROLE_NAME; }
        }

        public Cashier()
        {
        }

        public Payment ChargePurchase(IClient payingClient, ISale purchase, Money money)
        {
            throw new NotImplementedException();
        }

    }
}
