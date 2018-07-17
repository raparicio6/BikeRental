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
    public class ClientTestCase
    {
        private Client Client;

        private Bike Bike;

        private IRentalOperator RentalOperator;

        private BikeSpecifications BikeSpecifications; 

        [SetUp]
        public void SetUp()
        {
            this.Client = new Client();

            this.BikeSpecifications = new BikeSpecifications(TestsConstants.BIKE_BRAND,
                TestsConstants.BIKE_MODEL, TestsConstants.BIKE_COLOR);
            this.Bike = new Bike(TestsConstants.BIKE_IDENTIFICATION_CODE, this.BikeSpecifications);

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
        }

        [Test]
        public void RequestARental()
        {
            Rental rental = this.Client.RequestARental(this.Bike, UnitOfTime.Week, this.RentalOperator);

            Assert.AreEqual(this.Client, rental.Client);
            Assert.AreEqual(this.Bike, rental.Bike);
            Assert.AreEqual(UnitOfTime.Week, rental.Modality.UnitOfTime);
        }

        [Test]
        public void RequestAFamilyRental()
        {
            List<IClient> clients = new List<IClient>();
            IList<Bike> bikes = new List<Bike>();
            IList<UnitOfTime> unitsOfTimes = new List<UnitOfTime>();

            for (int i = 0; i < this.RentalOperator.CurrentFamilyRentalInformation.MaximumRentals; i++)
            {
                clients.Add(new Client());
                bikes.Add(new Bike("AAA000", this.BikeSpecifications));
                unitsOfTimes.Add(UnitOfTime.Day);
            }

            FamilyRental familyRental = this.Client.RequestAFamilyRental(clients, bikes, unitsOfTimes, this.RentalOperator);

            Assert.AreEqual(this.Client, familyRental.Client);            
            for (int i = 0; i < clients.Count; i++)
            {
                Assert.AreEqual(clients[i], familyRental.Rentals[i].Client);
                Assert.AreEqual(bikes[i], familyRental.Rentals[i].Bike);
                Assert.AreEqual(unitsOfTimes[i], familyRental.Rentals[i].Modality.UnitOfTime);
            }
        }

        [Test]
        public void FinalizeRental()
        {
            Rental rental = this.Client.RequestARental(this.Bike, UnitOfTime.Week, this.RentalOperator);

            // This is so that the beginning time does not coincide with the finalization time.
            System.Threading.Thread.Sleep(2000);
            RentalFinalization rentalFinalization = this.Client.FinalizeRental(rental, this.RentalOperator);

            Assert.AreEqual(this.Client, rentalFinalization.ClientWhoFinalizedRental);
        }

        [Test]
        public void PayPurchase()
        {
            Rental rental = this.Client.RequestARental(this.Bike, UnitOfTime.Week, this.RentalOperator);

            // This is so that the beginning time does not coincide with the finalization time.
            System.Threading.Thread.Sleep(2000);
            RentalFinalization rentalFinalization = this.Client.FinalizeRental(rental, this.RentalOperator);

            Payment payment = this.Client.PayPurchase(rental, rental.Cost, new Cashier());

            Assert.AreEqual(this.Client, payment.ClientWhoPaid);
        }

    }

}
