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
    public class RentalByDayTestCase
    {
        private RentalByDay RentalByDay;

        private Money CostPerUnitOfTime;

        private DateTime RentalBeginningDate;
        private DateTime RentalFinalizationDate;

        [SetUp]
        public void SetUp()
        {
            this.CostPerUnitOfTime = new Money(TestsConstants.RENTAL_BY_DAY_AMOUNT,
                TestsConstants.RENTAL_BY_DAY_TYPE_OF_CURRENCY);
            this.RentalByDay = new RentalByDay(this.CostPerUnitOfTime);
        }

        [Test]
        public void OneDayCostsTheSameAsTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(1);

            Assert.AreEqual(this.CostPerUnitOfTime, this.RentalByDay.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void TwoDaysCostTheSameAsTheCostPerUnitOfTimeMultipliedByTwo()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(2);
            Money dollarsMultipliedByTwo = new Money(this.CostPerUnitOfTime.Amount * 2, this.CostPerUnitOfTime.TypeOfCurrency);

            Assert.AreEqual(dollarsMultipliedByTwo, this.RentalByDay.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void HalfADayCostsTheSameAsHalfOfTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(0.5);
            Money halfOfTheDollars = new Money(this.CostPerUnitOfTime.Amount / 2, this.CostPerUnitOfTime.TypeOfCurrency);

            Assert.AreEqual(halfOfTheDollars, this.RentalByDay.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void FinalizationDateLessThanBeginningDateThrowsFinalizationDateOfRentalLessThanBeginningDateException()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(-1);

            Assert.That(() => this.RentalByDay.CalculateRentalCost(this.RentalBeginningDate,
                this.RentalFinalizationDate), Throws.TypeOf<FinalizationDateOfRentalIsLessThanBeginningDateException>());
        }

    }
}
