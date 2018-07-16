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
        private RentalEmission Emission;

        private Bike Bike;
        private BikeSpecifications BikeSpecifications;
        private const string BIKE_IDENTIFICATION_CODE = "ABC043";        

        private RentalModality Modality;        

        [SetUp]
        public void SetUp()
        {
            this.MockClient = new Mock<IClient>();

            this.MockRentalOperator = new Mock<IRentalOperator>();
            this.Emission = new RentalEmission(this.MockRentalOperator.Object);

            this.BikeSpecifications = new BikeSpecifications("Schwinn", "Continental Commuter 7", "Black");
            this.Bike = new Bike(BIKE_IDENTIFICATION_CODE, this.BikeSpecifications);

            this.Modality = new RentalByHour(new Money(5, Currency.Dollar));

            this.Rental = new Rental(this.MockClient.Object, this.Emission, this.Bike, this.Modality);
        }

        #region Is Finished Tests

        [Test]
        public void WhenIsCreatedItIsNotFinished()
        {
            Assert.IsFalse(this.Rental.IsFinished());
        }

        [Test]
        public void IsFinishedWhenItHasFinalization()
        {
            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);
            Assert.IsTrue(this.Rental.IsFinished());
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
        public void WhenItHasNoFinalizationThrowsRentalHasNotFinalizedYetException()
        {
            Assert.That(() => this.Rental.Cost, Throws.TypeOf<RentalHasNotFinalizedYetException>());
        }

        [Test]
        public void CostIsGottenWhenItIsFinalized()
        {
            // This is so that the time of emission does not coincide with the time of finalization.
            System.Threading.Thread.Sleep(3000);

            this.Rental.Finalization = new RentalFinalization(this.MockClient.Object, this.MockRentalOperator.Object);
            Money expectedCost = this.Rental.Modality.CalculateRentalCost(this.Rental.Emission.CreationDate, 
                this.Rental.Finalization.CreationDate);
            Assert.AreEqual(expectedCost, this.Rental.Cost);
        }

        #endregion

    }
}
