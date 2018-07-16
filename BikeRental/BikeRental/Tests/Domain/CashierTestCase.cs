using BikeRental.Domain;
using BikeRental.Domain.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Tests.Domain
{
    [TestFixture]
    public class CashierTestCase
    {
        private Cashier Cashier;

        private IRental Rental;

        private Mock<IClient> MockClient;

        private Mock<IRentalOperator> MockRentalOperator;

        [SetUp]
        public void SetUp()
        {
            this.Cashier = new Cashier();

            this.MockClient = new Mock<IClient>();

            this.MockRentalOperator = new Mock<IRentalOperator>();
            RentalEmission emission = new RentalEmission(this.MockRentalOperator.Object);

            BikeSpecifications bikeSpecifications = new BikeSpecifications("Schwinn", "Continental Commuter 7", "Black");
            Bike bike = new Bike("ABC043", bikeSpecifications);

            RentalByDay rentalByDay = new RentalByDay(new Money(20, Currency.Dollar));

            this.Rental = new Rental(this.MockClient.Object, emission, bike, rentalByDay);            
        }

        [Test]
        public void SaleIsPaidAfterCharging()
        {
            // This is so that the time of emission does not coincide with the time of finalization.
            System.Threading.Thread.Sleep(2000);

            Assert.IsFalse(this.Rental.IsPaid());

            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);
            this.Cashier.ChargePurchase(this.MockClient.Object, this.Rental, this.Rental.Cost);

            Assert.IsTrue(this.Rental.IsPaid());
        }

        [Test]
        public void MoneyReceivedIsUnequalThanPurchaseCostThrowsMoneyReceivedIsUnequalThanPurchaseCostException()
        {
            // This is so that the time of emission does not coincide with the time of finalization.
            System.Threading.Thread.Sleep(2000);

            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);            

            Assert.That(() => this.Cashier.ChargePurchase(this.MockClient.Object, this.Rental, new Money(this.Rental.Cost.Amount / 2, this.Rental.Cost.TypeOfCurrency)), 
                Throws.TypeOf<MoneyReceivedIsUnequalThanPurchaseCostException>());
        }

        [Test]
        public void PurchaseIsAlreadyPaidThrowsPurchaseIsAlreadyPaidException()
        {
            // This is so that the time of emission does not coincide with the time of finalization.
            System.Threading.Thread.Sleep(2000);

            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);
            this.Cashier.ChargePurchase(this.MockClient.Object, this.Rental, this.Rental.Cost);

            Assert.That(() => this.Cashier.ChargePurchase(this.MockClient.Object, this.Rental, this.Rental.Cost),
                Throws.TypeOf<PurchaseIsAlreadyPaidException>());
        }

    }
}
