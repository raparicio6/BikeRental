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

            PromotionRules familyRentalRules = new PromotionRules(TestsConstants.FAMILY_RENTAL_TERMS_AND_CONDITIONS, 
            new DateTime(TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_YEAR, TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_MONTH,
            TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_DAY, 0, 0, 0),
            new DateTime(TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_YEAR, TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_MONTH,
            TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_DAY, 0, 0, 0));
            FamilyRentalInformation familyRentalInformation = new FamilyRentalInformation(
                TestsConstants.FAMILY_RENTAL_DISCOUNT_PERCENT, TestsConstants.FAMILY_RENTAL_MINIMUM_RENTALS,
                TestsConstants.FAMILY_RENTAL_MAXIMUM_RENTALS, familyRentalRules);

            this.FamilyRental = new FamilyRental(this.MockClient.Object, familyRentalInformation, rentals);
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
