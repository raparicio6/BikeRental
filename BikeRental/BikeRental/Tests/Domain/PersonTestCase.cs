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
    public class PersonTestCase
    {
        private Person Person;           

        [SetUp]
        public void SetUp()
        {
            this.Person = new Person("33162982", "Frederick", "Jane", "1-541-754-3010", "fjane@test.com");
        }

        [Test]
        public void HasTheRoleAfterAddingIt()
        {
            ICashier cashier = new Cashier();
            this.Person.AddRole(cashier);
            Assert.AreEqual(cashier, this.Person.GetRoleByName(cashier.RoleName));
        }

        [Test]
        public void AddingRoleThatAlreadyHasThrowsPersonAlreadyHasThatRoleException()
        {
            ICashier cashier = new Cashier();
            this.Person.AddRole(cashier);

            ICashier anotherCashier = new Cashier();           

            Assert.That(() => this.Person.AddRole(anotherCashier), Throws.TypeOf<PersonAlreadyHasThatRoleException>());
        }
    }

}
