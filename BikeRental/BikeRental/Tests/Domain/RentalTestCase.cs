using BikeRental.Domain;
using BikeRental.Domain.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Tests.Domain
{
    [TestFixture]
    public class RentalTestCase
    {
        private Rental Rental;

        private Mock<IClient> MockClient;

        private Mock<IRentalOperator> MockRentalOperator;            

        [SetUp]
        public void SetUp()
        {
            this.MockClient = new Mock<IClient>();

            this.MockRentalOperator = new Mock<IRentalOperator>();
            RentalBeginning rentalBeginning = new RentalBeginning(this.MockRentalOperator.Object);

            BikeSpecifications bikeSpecifications = new BikeSpecifications(TestsConstants.BIKE_BRAND,
                TestsConstants.BIKE_MODEL, TestsConstants.BIKE_COLOR);
            Bike bike = new Bike(TestsConstants.BIKE_IDENTIFICATION_CODE, bikeSpecifications);

            RentalModality rentalModality = new RentalByHour(new Money(TestsConstants.RENTAL_BY_HOUR_AMOUNT,
                TestsConstants.RENTAL_BY_HOUR_TYPE_OF_CURRENCY));

            this.Rental = new Rental(this.MockClient.Object, rentalBeginning, bike, rentalModality);
        }

        #region Is Finalized Tests

        [Test]
        public void WhenIsCreatedItIsNotFinalized()
        {
            Assert.IsFalse(this.Rental.IsFinalized());
        }

        [Test]
        public void IsFinalizedWhenItHasFinalization()
        {
            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);
            Assert.IsTrue(this.Rental.IsFinalized());
        }

        #endregion

        #region Is Paid Tests

        [Test]
        public void WhenIsCreatedItIsNotPaid()
        {
            Assert.IsFalse(this.Rental.IsPaid());
        }

        [Test]
        public void IsPaidWhenItHasPayment()
        {
            Mock<ICashier> mockCashier = new Mock<ICashier>();
            this.Rental.Payment = new Payment(this.MockClient.Object, mockCashier.Object);
            Assert.IsTrue(this.Rental.IsPaid());
        }

        #endregion

        #region Cost Tests

        [Test]
        public void GetCostWhenItHasNoFinalizationThrowsRentalHasNotFinalizedYetException()
        {
            Assert.That(() => this.Rental.Cost, Throws.TypeOf<RentalHasNotFinalizedYetException>());
        }

        [Test]
        public void CostIsGottenWhenItIsFinalized()
        {
            // This is so that the beginning time does not coincide with the finalization time.
            System.Threading.Thread.Sleep(2000);
            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);

            Money expectedCost = this.Rental.Modality.CalculateRentalCost(this.Rental.Beginning.Date, 
                this.Rental.Finalization.Date);
            Assert.AreEqual(expectedCost, this.Rental.Cost);
        }

        #endregion

    }
}
