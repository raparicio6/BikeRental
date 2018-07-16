using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Payment
    {
        public IClient ClientWhoPaid { get; }

        public ICashier CashierWhoCharged { get; }

        public DateTime DateOfPayment { get; } 
        
        public Payment(IClient clientWhoIsPaying, ICashier cashierWhoIsCharging)
        {
            this.ClientWhoPaid = clientWhoIsPaying;
            this.CashierWhoCharged = cashierWhoIsCharging;
            this.DateOfPayment = DateTime.Now;
        }

    }
}
