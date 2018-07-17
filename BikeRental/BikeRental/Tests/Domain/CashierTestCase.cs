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
            RentalBeginning rentalBeginning = new RentalBeginning(this.MockRentalOperator.Object);

            BikeSpecifications bikeSpecifications = new BikeSpecifications(TestsConstants.BIKE_BRAND,
                TestsConstants.BIKE_MODEL, TestsConstants.BIKE_COLOR);
            Bike bike = new Bike(TestsConstants.BIKE_IDENTIFICATION_CODE, bikeSpecifications);

            RentalModality rentalModality = new RentalByDay(new Money(TestsConstants.RENTAL_BY_DAY_AMOUNT,
                TestsConstants.RENTAL_BY_DAY_TYPE_OF_CURRENCY));

            this.Rental = new Rental(this.MockClient.Object, rentalBeginning, bike, rentalModality);            
        }

        [Test]
        public void SaleIsPaidAfterCharging()
        {
            IClient client = this.MockClient.Object;

            // This is so that the beginning time does not coincide with the finalization time.
            System.Threading.Thread.Sleep(2000);
            this.Rental.Finalization = new RentalFinalization(client, this.MockRentalOperator.Object);

            Payment payment = this.Cashier.ChargePurchase(client, this.Rental, this.Rental.Cost);

            Assert.AreEqual(payment, this.Rental.Payment);
            Assert.AreEqual(this.Cashier, payment.CashierWhoCharged);
            Assert.AreEqual(client, payment.ClientWhoPaid);
        }

        [Test]
        public void MoneyReceivedIsUnequalThanPurchaseCostThrowsMoneyReceivedIsUnequalThanPurchaseCostException()
        {
            // This is so that the beginning time does not coincide with the finalization time.
            System.Threading.Thread.Sleep(2000);
            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);            

            Assert.That(() => this.Cashier.ChargePurchase(this.MockClient.Object, this.Rental, new Money(this.Rental.Cost.Amount / 2, this.Rental.Cost.TypeOfCurrency)), 
                Throws.TypeOf<MoneyReceivedIsUnequalThanPurchaseCostException>());
        }

        [Test]
        public void PurchaseIsAlreadyPaidThrowsPurchaseIsAlreadyPaidException()
        {
            // This is so that the beginning time does not coincide with the finalization time.
            System.Threading.Thread.Sleep(2000);
            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);

            this.Cashier.ChargePurchase(this.MockClient.Object, this.Rental, this.Rental.Cost);

            Assert.That(() => this.Cashier.ChargePurchase(this.MockClient.Object, this.Rental, this.Rental.Cost),
                Throws.TypeOf<PurchaseIsAlreadyPaidException>());
        }

    }
}
