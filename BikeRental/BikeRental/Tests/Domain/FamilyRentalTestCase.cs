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
    public class FamilyRentalTestCase
    {
        private FamilyRental FamilyRental;

        private Mock<IClient> MockClient;

        private Mock<IRental> MockRental1;
        private Mock<IRental> MockRental2;
        private Mock<IRental> MockRental3;
        private Mock<IRental> MockRental4;

        [SetUp]
        public void SetUp()
        {
            this.MockClient = new Mock<IClient>();

            this.MockRental1 = new Mock<IRental>();
            this.MockRental2 = new Mock<IRental>();
            this.MockRental3 = new Mock<IRental>();
            this.MockRental4 = new Mock<IRental>();           
            IList<IRental> rentals = new List<IRental>();
            rentals.Add(this.MockRental1.Object);
            rentals.Add(this.MockRental2.Object);
            rentals.Add(this.MockRental3.Object);
            rentals.Add(this.MockRental4.Object);

            PromotionRules rules = new PromotionRules("The Family Rental promotion will begin on May 1st and end on August 1st, 2018. " +
            "There will be a discount of 30 percent making between 3 and 5 rentals. " +
            "Any person over 16 years of age will be eligible to participate. The promotion is subject to the availability of bicycles.", 
            new DateTime(2018, 5, 1, 0, 0, 0), new DateTime(2018, 8, 1, 0, 0, 0));
            FamilyRentalInformation information = new FamilyRentalInformation(30, 3, 5, rules);

            this.FamilyRental = new FamilyRental(this.MockClient.Object, information, rentals);
        }       

        #region Is Paid Tests

        [Test]
        public void WhenIsCreatedItIsNotPaid()
        {
            Assert.IsFalse(this.FamilyRental.IsPaid());
        }

        [Test]
        public void IsPaidWhenItHasPayment()
        {
            Mock<ICashier> mockCashier = new Mock<ICashier>();
            this.FamilyRental.Payment = new Payment(this.MockClient.Object, mockCashier.Object);
            Assert.IsTrue(this.FamilyRental.IsPaid());
        }

        #endregion

        #region Cost Tests      

        [Test]
        public void CostIsGottenWithTheDiscountApplied()
        {
            this.MockRental1.Setup(rental => rental.Cost).Returns(new Money(7, Currency.Dollar));
            this.MockRental2.Setup(rental => rental.Cost).Returns(new Money(12, Currency.Dollar));
            this.MockRental3.Setup(rental => rental.Cost).Returns(new Money(55, Currency.Dollar));
            this.MockRental4.Setup(rental => rental.Cost).Returns(new Money(26, Currency.Dollar));

            Money expectedCost = new Money(((this.FamilyRental.Rentals.Sum(rental => rental.Cost.Amount)) 
                * (100 - (decimal)this.FamilyRental.Information.DiscountPercent)) / 100, Currency.Dollar);

            Assert.AreEqual(expectedCost, this.FamilyRental.Cost);
        }

        #endregion

    }
}
