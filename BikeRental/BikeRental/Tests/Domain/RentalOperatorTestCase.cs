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

        private PromotionRules FamilyRentalRules;

        private BikeSpecifications BikeSpecifications;

        [SetUp]
        public void SetUp()
        {
            this.FamilyRentalRules = new PromotionRules(TestsConstants.FAMILY_RENTAL_TERMS_AND_CONDITIONS,
            new DateTime(TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_YEAR, TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_MONTH,
            TestsConstants.FAMILY_RENTAL_EFFECTIVE_DATE_DAY, 0, 0, 0),
            new DateTime(TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_YEAR, TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_MONTH,
            TestsConstants.FAMILY_RENTAL_EXPIRATON_DATE_DAY, 0, 0, 0));
            FamilyRentalInformation familyRentalInformation = new FamilyRentalInformation(
                TestsConstants.FAMILY_RENTAL_DISCOUNT_PERCENT, TestsConstants.FAMILY_RENTAL_MINIMUM_RENTALS,
                TestsConstants.FAMILY_RENTAL_MAXIMUM_RENTALS, this.FamilyRentalRules);
            

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

            this.BikeSpecifications = new BikeSpecifications("Scott", "Plasma 5", "Red");
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

        [Test]
        public void FinalizeNotFinalizedRental()
        {
            RentalBeginning beginning = new RentalBeginning(this.RentalOperator);
            RentalModality rentalModality = new RentalByWeek(new Money(TestsConstants.RENTAL_BY_WEEK_AMOUNT,
                TestsConstants.RENTAL_BY_WEEK_TYPE_OF_CURRENCY));
            IRental rental = new Rental(this.Client, beginning, this.Bike, rentalModality);

            RentalFinalization rentalFinalization = this.RentalOperator.FinalizeRental(this.Client, rental);

            Assert.AreEqual(rentalFinalization, rental.Finalization);
            Assert.AreEqual(this.RentalOperator, rental.Finalization.RentalOperatorWhoFinalizedRental);
            Assert.AreEqual(this.Client, rental.Finalization.ClientWhoFinalizedRental);

            Assert.AreEqual(BikeState.Free, this.Bike.State);
            Assert.AreEqual(null, this.Bike.Rental);
        }

        [Test]
        public void FinalizeFinalizedRentalThrowsRentalIsAlreadyFinalizedException()
        {
            RentalBeginning beginning = new RentalBeginning(this.RentalOperator);
            RentalModality rentalModality = new RentalByWeek(new Money(TestsConstants.RENTAL_BY_WEEK_AMOUNT,
                TestsConstants.RENTAL_BY_WEEK_TYPE_OF_CURRENCY));
            IRental rental = new Rental(this.Client, beginning, this.Bike, rentalModality);

            this.RentalOperator.FinalizeRental(this.Client, rental);

            Assert.That(() => this.RentalOperator.FinalizeRental(this.Client, rental),
                Throws.TypeOf<RentalIsAlreadyFinalizedException>());
        }

        #endregion

        #region Provide FamilyRental Tests

        [Test]
        public void ProvideFamilyRentalWithAmountsOk()
        {
            // I force the 3 and 5 to control the amount of rentals allowed and not depend on the constants 
            FamilyRentalInformation familyRentalInformation = new FamilyRentalInformation(
                TestsConstants.FAMILY_RENTAL_DISCOUNT_PERCENT, 3, 5, this.FamilyRentalRules);
            this.RentalOperator.UpdateCurrentFamilyRentalInformation(familyRentalInformation);

            IClient father = new Client();
            IClient mother = new Client();
            IClient son = new Client();
            IClient daughter = new Client();

            IList<IClient> clients = new List<IClient>();
            clients.Add(mother);
            clients.Add(father);
            clients.Add(son);
            clients.Add(daughter);
            
            Bike bike2 = new Bike("YGR496", this.BikeSpecifications);
            BikeSpecifications bikeSpecifications3 = new BikeSpecifications("Trek", "Madone", "Green");
            Bike bike3 = new Bike("JWT764", bikeSpecifications3);
            BikeSpecifications bikeSpecifications4 = new BikeSpecifications("Santa Cruz", "V10", "Brown");
            Bike bike4 = new Bike("MTN858", bikeSpecifications4);

            IList<Bike> bikes = new List<Bike>();
            bikes.Add(this.Bike);
            bikes.Add(bike2);
            bikes.Add(bike3);
            bikes.Add(bike4);

            IList<UnitOfTime> unitsOfTimes = new List<UnitOfTime>();
            unitsOfTimes.Add(UnitOfTime.Hour);
            unitsOfTimes.Add(UnitOfTime.Hour);
            unitsOfTimes.Add(UnitOfTime.Day);
            unitsOfTimes.Add(UnitOfTime.Day);

            FamilyRental familyRental = this.RentalOperator.ProvideFamilyRental(mother, clients, bikes, unitsOfTimes);

            Assert.AreEqual(mother, familyRental.Client);
            Assert.AreEqual(this.RentalOperator.CurrentFamilyRentalInformation, familyRental.Information);
            for (int i = 0; i < clients.Count; i++)
            {
                Assert.AreEqual(clients[i], familyRental.Rentals[i].Client);
                Assert.AreEqual(bikes[i], familyRental.Rentals[i].Bike);
                Assert.AreEqual(unitsOfTimes[i], familyRental.Rentals[i].Modality.UnitOfTime);
            }
        }

        [Test]
        public void ProvideFamilyRentalWithDifferentAmountOfElementsThrowsAmountOfClientsAndBikesAndUnitsOfTimeDoNotMatchException()
        {
            IClient father = new Client();
            IClient mother = new Client();
            IClient son = new Client();
            IClient daughter = new Client();

            IList<IClient> clients = new List<IClient>();
            clients.Add(mother);
            clients.Add(father);
            clients.Add(son);
            clients.Add(daughter);

            Bike bike2 = new Bike("YGR496", this.BikeSpecifications);
            BikeSpecifications bikeSpecifications3 = new BikeSpecifications("Trek", "Madone", "Green");
            Bike bike3 = new Bike("JWT764", bikeSpecifications3);

            IList<Bike> bikes = new List<Bike>();
            bikes.Add(this.Bike);
            bikes.Add(bike2);
            bikes.Add(bike3);

            IList<UnitOfTime> unitsOfTimes = new List<UnitOfTime>();
            unitsOfTimes.Add(UnitOfTime.Hour);
            unitsOfTimes.Add(UnitOfTime.Hour);

            Assert.That(() => this.RentalOperator.ProvideFamilyRental(mother, clients, bikes, unitsOfTimes),
                Throws.TypeOf<AmountOfClientsAndBikesAndUnitsOfTimeDoNotMatchException>());
        }

        [Test]
        public void ProvideFamilyRentalWithAmountOfElementsGreaterThanTheMaximumAllowedThrowsAmountOfRentalsWantedDoesNotRespectTheEstablishedMarginException()
        {
            IList<IClient> clients = new List<IClient>();
            IList<Bike> bikes = new List<Bike>();
            IList<UnitOfTime> unitsOfTimes = new List<UnitOfTime>();

            for (int i = 0; i <= this.RentalOperator.CurrentFamilyRentalInformation.MaximumRentals + 2; i++)
            {
                clients.Add(new Client());
                bikes.Add(new Bike("AAA000", this.BikeSpecifications));
                unitsOfTimes.Add(UnitOfTime.Day);
            }

            Assert.That(() => this.RentalOperator.ProvideFamilyRental(clients[0], clients, bikes, unitsOfTimes),
                Throws.TypeOf<AmountOfRentalsWantedDoesNotRespectTheEstablishedMarginException>());
        }

        [Test]
        public void ProvideFamilyRentalWithAmountOfElementsLessThanTheMinimumAllowedThrowsAmountOfRentalsWantedDoesNotRespectTheEstablishedMarginException()
        {
            // There can not be less than 1 rental
            if (this.RentalOperator.CurrentFamilyRentalInformation.MinimumRentals == 1)
                return;

            IList<IClient> clients = new List<IClient>();
            IList<Bike> bikes = new List<Bike>();
            IList<UnitOfTime> unitsOfTimes = new List<UnitOfTime>();           

            for (int i = 0; i < this.RentalOperator.CurrentFamilyRentalInformation.MinimumRentals - 1; i++)
            {
                clients.Add(new Client());
                bikes.Add(new Bike("AAA000", this.BikeSpecifications));
                unitsOfTimes.Add(UnitOfTime.Day);
            }

            Assert.That(() => this.RentalOperator.ProvideFamilyRental(clients[0], clients, bikes, unitsOfTimes),
                Throws.TypeOf<AmountOfRentalsWantedDoesNotRespectTheEstablishedMarginException>());
        }

        [Test]
        public void ProvideFamilyRentalWithAmountsEqualToMaximum()
        {
            IList<IClient> clients = new List<IClient>();
            IList<Bike> bikes = new List<Bike>();
            IList<UnitOfTime> unitsOfTimes = new List<UnitOfTime>();

            for (int i = 0; i < this.RentalOperator.CurrentFamilyRentalInformation.MaximumRentals; i++)
            {
                clients.Add(new Client());
                bikes.Add(new Bike("AAA000", this.BikeSpecifications));
                unitsOfTimes.Add(UnitOfTime.Day);
            }

            FamilyRental familyRental = this.RentalOperator.ProvideFamilyRental(clients[0], clients, bikes, unitsOfTimes);

            Assert.AreEqual(clients[0], familyRental.Client);
            Assert.AreEqual(this.RentalOperator.CurrentFamilyRentalInformation, familyRental.Information);
            for (int i = 0; i < clients.Count; i++)
            {
                Assert.AreEqual(clients[i], familyRental.Rentals[i].Client);
                Assert.AreEqual(bikes[i], familyRental.Rentals[i].Bike);
                Assert.AreEqual(unitsOfTimes[i], familyRental.Rentals[i].Modality.UnitOfTime);
            }
        }

        [Test]
        public void ProvideFamilyRentalWithAmountsEqualToMinimum()
        {
            IList<IClient> clients = new List<IClient>();
            IList<Bike> bikes = new List<Bike>();
            IList<UnitOfTime> unitsOfTimes = new List<UnitOfTime>();

            for (int i = 0; i < this.RentalOperator.CurrentFamilyRentalInformation.MinimumRentals; i++)
            {
                clients.Add(new Client());
                bikes.Add(new Bike("AAA000", this.BikeSpecifications));
                unitsOfTimes.Add(UnitOfTime.Day);
            }

            FamilyRental familyRental = this.RentalOperator.ProvideFamilyRental(clients[0], clients, bikes, unitsOfTimes);

            Assert.AreEqual(clients[0], familyRental.Client);
            Assert.AreEqual(this.RentalOperator.CurrentFamilyRentalInformation, familyRental.Information);
            for (int i = 0; i < clients.Count; i++)
            {
                Assert.AreEqual(clients[i], familyRental.Rentals[i].Client);
                Assert.AreEqual(bikes[i], familyRental.Rentals[i].Bike);
                Assert.AreEqual(unitsOfTimes[i], familyRental.Rentals[i].Modality.UnitOfTime);
            }
        }

        #endregion


    }
}
