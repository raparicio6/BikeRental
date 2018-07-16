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
            this.Person = new Person(TestsConstants.PERSON_ID_NUMBER, TestsConstants.PERSON_NAME,
                TestsConstants.PERSON_LAST_NAME, TestsConstants.PERSON_PHONE, TestsConstants.PERSON_EMAIL);
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
