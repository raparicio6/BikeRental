using BikeRental.Domain.Exceptions;
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

        private bool MoneyIsCorrect(Money cost, Money moneyReceived)
        {
            return cost == moneyReceived;
        }

        public Payment ChargePurchase(IClient payingClient, ISale purchase, Money moneyReceived)
        {
            if (purchase.IsPaid())
                throw new PurchaseIsAlreadyPaidException("The purchase is already paid");

            if (!this.MoneyIsCorrect(purchase.Cost, moneyReceived))
                throw new MoneyReceivedIsUnequalThanPurchaseCostException("Money received is unequal than the purchase cost");

            Payment payment = new Payment(payingClient, this);
            purchase.Payment = payment;
            return payment;
        }

    }
}
