using BikeRental.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Tests.Domain
{
    [TestFixture]
    public class BikeTestCase
    {
        private Bike Bike;             

        [SetUp]
        public void SetUp()
        {
            BikeSpecifications bikeSpecifications = new BikeSpecifications("Schwinn", "Continental Commuter 7", "Black");
            this.Bike = new Bike("ABC043", bikeSpecifications);
        }

        [Test]
        public void WhenIsCreatedItsStateIsFree()
        {
            Assert.AreEqual(BikeState.Free, this.Bike.State);
        }

        [Test]
        public void IsAvailableWhenIsFree()
        {
            Assert.IsTrue(this.Bike.IsAvailable());
        }

        [Test]
        public void StateIsChangedCorrectly()
        {
            this.Bike.ChangeState(BikeState.In_Use);
            Assert.AreEqual(BikeState.In_Use, this.Bike.State);
        }

        [Test]
        public void IsNotAvailableWhenIsNotFree()
        {
            this.Bike.ChangeState(BikeState.In_Maintenance);
            Assert.IsFalse(this.Bike.IsAvailable());
            this.Bike.ChangeState(BikeState.In_Use);
            Assert.IsFalse(this.Bike.IsAvailable());
            this.Bike.ChangeState(BikeState.Broken);
            Assert.IsFalse(this.Bike.IsAvailable());
        }

    }
}
