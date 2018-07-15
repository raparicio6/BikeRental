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
    public class RentalByHourTestCase
    {
        private RentalByHour RentalByHour;
        private Money Dollars_5;
        private DateTime RentalEmissionDate;
        private DateTime RentalFinalizationDate;

        [SetUp]
        public void SetUp()
        {
            this.Dollars_5 = new Money(5, TypeOfCurrency.Dollar);
            this.RentalByHour = new RentalByHour(this.Dollars_5);
        }

        [Test]
        public void OneHourCostsTheSameAsTheCostPerUnitOfTime()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddHours(1);

            Assert.AreEqual(this.Dollars_5, this.RentalByHour.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }       

        [Test]
        public void TwoHoursCostTheSameAsTheCostPerUnitOfTimeMultipliedByTwo()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddHours(2);
            Money dollarsMultipliedByTwo = new Money(this.Dollars_5.Amount * 2, this.Dollars_5.TypeOfCurrency);

            Assert.AreEqual(dollarsMultipliedByTwo, this.RentalByHour.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }

        [Test]
        public void HalfAnHourCostsTheSameAsHalfOfTheCostPerUnitOfTime()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddHours(0.5);
            Money halfOfTheDollars = new Money(this.Dollars_5.Amount / 2, this.Dollars_5.TypeOfCurrency);

            Assert.AreEqual(halfOfTheDollars, this.RentalByHour.CalculateRentalCost(this.RentalEmissionDate, this.RentalFinalizationDate));
        }        

        [Test]
        public void FinalizationDateLessThanEmissionDateThrowsFinalizationDateOfRentalLessThanEmissionDateException()
        {
            RentalEmissionDate = new DateTime(2018, 12, 10, 14, 0, 0);
            RentalFinalizationDate = RentalEmissionDate.AddHours(-1);

            Assert.That(() => this.RentalByHour.CalculateRentalCost(this.RentalEmissionDate, 
                this.RentalFinalizationDate), Throws.TypeOf<FinalizationDateOfRentalLessThanEmissionDateException>());
        }

    }
}
