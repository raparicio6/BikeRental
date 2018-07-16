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
    public class RentalOperatorTestCase
    {
        private RentalOperator RentalOperator;        

        private IClient Client;

        private Bike Bike;

        [SetUp]
        public void SetUp()
        {
            PromotionRules familyRentalRules = new PromotionRules(TestsConstants.FAMILY_RENTAL_TERMS_AND_CONDITIONS,
            new DateTime(TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_YEAR, TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_MONTH,
            TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_DAY, 0, 0, 0),
            new DateTime(TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_YEAR, TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_MONTH,
            TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_DAY, 0, 0, 0));
            FamilyRentalInformation familyRentalInformation = new FamilyRentalInformation(
                TestsConstants.FAMILY_RENTAL_DISCOUNT_PERCENT, TestsConstants.FAMILY_RENTAL_MINIMUM_RENTALS,
                TestsConstants.FAMILY_RENTAL_MAXIMUM_RENTALS, familyRentalRules);

            RentalByHour rentalByHour = new RentalByHour(new Money(TestsConstants.RENTAL_BY_HOUR_AMOUNT,
                TestsConstants.RENTAL_BY_HOUR_TYPE_OF_CURRENCY));
            RentalByDay rentalByDay = new RentalByDay(new Money(TestsConstants.RENTAL_BY_DAY_AMOUNT,
                TestsConstants.RENTAL_BY_DAY_TYPE_OF_CURRENCY));
            RentalByWeek rentalByWeek = new RentalByWeek(new Money(TestsConstants.RENTAL_BY_WEEK_AMOUNT,
                TestsConstants.RENTAL_BY_WEEK_TYPE_OF_CURRENCY));

            this.RentalOperator = new RentalOperator(familyRentalInformation, rentalByHour, rentalByDay, rentalByWeek);

            this.Client = new Client();

            BikeSpecifications bikeSpecifications = new BikeSpecifications(TestsConstants.BIKE_BRAND,
                TestsConstants.BIKE_MODEL, TestsConstants.BIKE_COLOR);
            this.Bike = new Bike(TestsConstants.BIKE_IDENTIFICATION_CODE, bikeSpecifications);
        }

        #region Provide Rental Tests

        [Test]
        public void ProvideRentalBikeAvailableUnitOfTimeValid()
        {
            Rental rental = this.RentalOperator.ProvideRental(this.Client, this.Bike, UnitOfTime.Day);

            Assert.AreEqual(UnitOfTime.Day, rental.Modality.UnitOfTime);

            Assert.AreEqual(this.Client, rental.Client);
            Assert.IsTrue(this.Client.Rentals.Contains(rental));

            Assert.AreEqual(this.Bike, rental.Bike);
            Assert.AreEqual(rental, this.Bike.Rental);
            Assert.AreEqual(BikeState.In_Use, this.Bike.State);

            Assert.AreEqual(this.RentalOperator, rental.Beginning.RentalOperatorWhoBeganRental);
        }

        [Test]
        public void ProvideRentalBikeNotAvailable()
        {
            this.Bike.ChangeState(BikeState.Broken);    

            Assert.That(() => this.RentalOperator.ProvideRental(this.Client, this.Bike, UnitOfTime.Day), 
                Throws.TypeOf<BikeIsNotAvailableException>());
        }

        [Test]
        public void ProvideRentalUnitOfTimeNotValid()
        {            
            Assert.That(() => this.RentalOperator.ProvideRental(this.Client, this.Bike, (UnitOfTime)1000),
                Throws.TypeOf<UnitOfTimeIsNotValidException>());
        }

        #endregion

        #region Finalize Rental Tests




        #endregion
    }
}
