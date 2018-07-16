using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public interface ICashier : Role
    {
        Payment ChargePurchase(IClient payingClient, ISale purchase, Money moneyReceived);

    }
}
