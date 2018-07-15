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
        private DateTime RentalEmissionDate;
        private DateTime RentalFinalizationDate;

        [SetUp]
        public void SetUp()
        {
            this.Dollars_60 = new Money(60, TypeOfCurrency.Dollar);
            this.RentalByWeek = new RentalByWeek(this.Dollars_60);
        }

        [Test]
        public void OneWeekCostsTheSameAsTheCostPerUnitOfTime()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(7);

            Assert.AreEqual(this.Dollars_60, this.RentalByWeek.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }

        [Test]
        public void TwoWeeksCostTheSameAsTheCostPerUnitOfTimeMultipliedByTwo()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(14);
            Money dollarsMultipliedByTwo = new Money(this.Dollars_60.Amount * 2, this.Dollars_60.TypeOfCurrency);

            Assert.AreEqual(dollarsMultipliedByTwo, this.RentalByWeek.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }

        [Test]
        public void HalfAWeekCostsTheSameAsHalfOfTheCostPerUnitOfTime()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(3.5);
            Money halfOfTheDollars = new Money(this.Dollars_60.Amount / 2, this.Dollars_60.TypeOfCurrency);

            Assert.AreEqual(halfOfTheDollars, this.RentalByWeek.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }

        [Test]
        public void FinalizationDateLessThanEmissionDateThrowsFinalizationDateOfRentalLessThanEmissionDateException()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddDays(-7);

            Assert.That(() => this.RentalByWeek.CalculateRentalCost(this.RentalEmissionDate,
                this.RentalFinalizationDate), Throws.TypeOf<FinalizationDateOfRentalLessThanEmissionDateException>());
        }

    }
}
