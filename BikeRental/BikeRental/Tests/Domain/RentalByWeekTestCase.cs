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

        private Money Dollars_60;

        private DateTime RentalBeginningDate;
        private DateTime RentalFinalizationDate;

        [SetUp]
        public void SetUp()
        {
            this.Dollars_60 = new Money(60, Currency.Dollar);
            this.RentalByWeek = new RentalByWeek(this.Dollars_60);
        }

        [Test]
        public void OneWeekCostsTheSameAsTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(7);

            Assert.AreEqual(this.Dollars_60, this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void TwoWeeksCostTheSameAsTheCostPerUnitOfTimeMultipliedByTwo()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(14);
            Money dollarsMultipliedByTwo = new Money(this.Dollars_60.Amount * 2, this.Dollars_60.TypeOfCurrency);

            Assert.AreEqual(dollarsMultipliedByTwo, this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void HalfAWeekCostsTheSameAsHalfOfTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(3.5);
            Money halfOfTheDollars = new Money(this.Dollars_60.Amount / 2, this.Dollars_60.TypeOfCurrency);

            Assert.AreEqual(halfOfTheDollars, this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void FinalizationDateLessThanBeginningDateThrowsFinalizationDateOfRentalLessThanBeginningDateException()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddDays(-7);

            Assert.That(() => this.RentalByWeek.CalculateRentalCost(this.RentalBeginningDate,
                this.RentalFinalizationDate), Throws.TypeOf<FinalizationDateOfRentalLessThanBeginningDateException>());
        }

    }
}
