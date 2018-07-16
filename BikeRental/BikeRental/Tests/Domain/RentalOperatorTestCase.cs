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
            PromotionRules rules = new PromotionRules(TestsConstants.FAMILY_RENTAL_TERMS_AND_CONDITIONS,
            new DateTime(2018, 5, 1, 0, 0, 0), new DateTime(2018, 8, 1, 0, 0, 0));
            FamilyRentalInformation information = new FamilyRentalInformation(30, 3, 5, rules);

            RentalByHour rentalByHour = new RentalByHour(new Money(5, Currency.Dollar));
            RentalByDay rentalByDay = new RentalByDay(new Money(20, Currency.Dollar));
            RentalByWeek rentalByWeek = new RentalByWeek(new Money(60, Currency.Dollar));

            this.RentalOperator = new RentalOperator(information, rentalByHour, rentalByDay, rentalByWeek);

            this.Client = new Client();

            BikeSpecifications bikeSpecifications = new BikeSpecifications("Schwinn", "Continental Commuter 7", "Black");
            this.Bike = new Bike("ABC043", bikeSpecifications);
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

            Assert.AreEqual(this.RentalOperator, rental.Emission.RentalOperatorWhoEmitted);
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
