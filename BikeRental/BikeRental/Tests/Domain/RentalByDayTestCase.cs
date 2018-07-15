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
        private Money Dollars_20;
        private DateTime RentalEmissionDate;
        private DateTime RentalFinalizationDate;

        [SetUp]
        public void SetUp()
        {
            this.Dollars_20 = new Money(20, TypeOfCurrency.Dollar);
            this.RentalByDay = new RentalByDay(this.Dollars_20);
        }

        [Test]
        public void OneDayCostsTheSameAsTheCostPerUnitOfTime()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(1);

            Assert.AreEqual(this.Dollars_20, this.RentalByDay.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }

        [Test]
        public void TwoDaysCostTheSameAsTheCostPerUnitOfTimeMultipliedByTwo()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(2);
            Money dollarsMultipliedByTwo = new Money(this.Dollars_20.Amount * 2, this.Dollars_20.TypeOfCurrency);

            Assert.AreEqual(dollarsMultipliedByTwo, this.RentalByDay.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }

        [Test]
        public void HalfADayCostsTheSameAsHalfOfTheCostPerUnitOfTime()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(0.5);
            Money halfOfTheDollars = new Money(this.Dollars_20.Amount / 2, this.Dollars_20.TypeOfCurrency);

            Assert.AreEqual(halfOfTheDollars, this.RentalByDay.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }

        [Test]
        public void FinalizationDateLessThanEmissionDateThrowsFinalizationDateOfRentalLessThanEmissionDateException()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(-1);

            Assert.That(() => this.RentalByDay.CalculateRentalCost(this.RentalEmissionDate,
                this.RentalFinalizationDate), Throws.TypeOf<FinalizationDateOfRentalLessThanEmissionDateException>());
        }

    }
}
