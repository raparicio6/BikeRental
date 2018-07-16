using BikeRental.Domain;
using BikeRental.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Tests.Domain
{
    [TestFixture]
    public class RentalByWeekTestCase
    {
        private RentalByWeek RentalByWeek;

        private Money CostPerUnitOfTime;

        private DateTime RentalBeginningDate;
        private DateTime RentalFinalizationDate;

        [SetUp]
        public void SetUp()
        {
            this.CostPerUnitOfTime = new Money(TestsConstants.RENTAL_BY_WEEK_AMOUNT,
                TestsConstants.RENTAL_BY_WEEK_TYPE_OF_CURRENCY);
            this.RentalByWeek = new RentalByWeek(this.CostPerUnitOfTime);
        }

        [Test]
        public void OneWeekCostsTheSameAsTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(7);

            Assert.AreEqual(this.CostPerUnitOfTime, this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void TwoWeeksCostTheSameAsTheCostPerUnitOfTimeMultipliedByTwo()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(14);
            Money dollarsMultipliedByTwo = new Money(this.CostPerUnitOfTime.Amount * 2, this.CostPerUnitOfTime.TypeOfCurrency);

            Assert.AreEqual(dollarsMultipliedByTwo, this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void HalfAWeekCostsTheSameAsHalfOfTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(3.5);
            Money halfOfTheDollars = new Money(this.CostPerUnitOfTime.Amount / 2, this.CostPerUnitOfTime.TypeOfCurrency);

            Assert.AreEqual(halfOfTheDollars, this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void FinalizationDateLessThanBeginningDateThrowsFinalizationDateOfRentalLessThanBeginningDateException()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(-7);

            Assert.That(() => this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate,
                this.RentalFinalizationDate), Throws.TypeOf<FinalizationDateOfRentalIsLessThanBeginningDateException>());
        }

    }
}
