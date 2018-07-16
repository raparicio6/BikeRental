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

        private Money CostPerUnitOfTime;

        private DateTime RentalBeginningDate;
        private DateTime RentalFinalizationDate;

        [SetUp]
        public void SetUp()
        {
            this.CostPerUnitOfTime = new Money(TestsConstants.RENTAL_BY_HOUR_AMOUNT, 
                TestsConstants.RENTAL_BY_HOUR_TYPE_OF_CURRENCY);
            this.RentalByHour = new RentalByHour(this.CostPerUnitOfTime);
        }

        [Test]
        public void OneHourCostsTheSameAsTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddHours(1);

            Assert.AreEqual(this.CostPerUnitOfTime, this.RentalByHour.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }       

        [Test]
        public void TwoHoursCostTheSameAsTheCostPerUnitOfTimeMultipliedByTwo()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddHours(2);
            Money dollarsMultipliedByTwo = new Money(this.CostPerUnitOfTime.Amount * 2, this.CostPerUnitOfTime.TypeOfCurrency);

            Assert.AreEqual(dollarsMultipliedByTwo, this.RentalByHour.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }

        [Test]
        public void HalfAnHourCostsTheSameAsHalfOfTheCostPerUnitOfTime()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddHours(0.5);
            Money halfOfTheDollars = new Money(this.CostPerUnitOfTime.Amount / 2, this.CostPerUnitOfTime.TypeOfCurrency);

            Assert.AreEqual(halfOfTheDollars, this.RentalByHour.CalculateRentalCost(this.RentalBeginningDate, this.RentalFinalizationDate));
        }        

        [Test]
        public void FinalizationDateLessThanBeginningDateThrowsFinalizationDateOfRentalLessThanBeginningDateException()
        {
            this.RentalBeginningDate = new DateTime(2018, 12, 10, 14, 0, 0);
            this.RentalFinalizationDate = this.RentalBeginningDate.AddHours(-1);

            Assert.That(() => this.RentalByHour.CalculateRentalCost(this.RentalBeginningDate, 
                this.RentalFinalizationDate), Throws.TypeOf<FinalizationDateOfRentalLessThanBeginningDateException>());
        }

    }
}
