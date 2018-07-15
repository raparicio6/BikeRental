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
    public class MoneyTestCase
    {
        private Money Dollars_50;
        private Money Dollars_40;
        private Money ArgentinePesos_40;
        private Money AnotherDollars_50;

        [SetUp]
        public void SetUp()
        {
            this.Dollars_50 = new Money(50, TypeOfCurrency.Dollar);
            this.Dollars_40 = new Money(40, TypeOfCurrency.Dollar);
            this.ArgentinePesos_40 = new Money(40, TypeOfCurrency.Argentine_Peso);
            this.AnotherDollars_50 = new Money(50, TypeOfCurrency.Dollar);
        }

        #region Equality Operator Tests

        [Test]
        public void ComparingTwoMoneysWithSameAmountAndSameTypeOfCurrencyUsingEqualOperatorAreEqual()
        {
            Assert.AreEqual(true, this.Dollars_50 == this.AnotherDollars_50);
        }

        [Test]
        public void ComparingTwoMoneysWithDiffentAmountAndSameTypeOfCurrenrcyUsingEqualOperatorAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_50 == this.Dollars_40);
        }

        [Test]
        public void ComparingTwoMoneysWithSameAmountAndDiffentTypeOfCurrencyUsingEqualOperatorAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_40 == this.ArgentinePesos_40);
        }

        [Test]
        public void ComparingTwoMoneysWithDiffentAmountAndDiffentTypeOfCurrencyUsingEqualOperatorAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_50 == this.ArgentinePesos_40);
        }

        [Test]
        public void ComparingMoneyWithNullUsingEqualOperatorAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_50 == null);
        }       

        #endregion

        #region Inequality Operator Tests

        [Test]
        public void ComparingTwoMoneysWithSameAmountAndSameTypeOfCurrencyUsingInequalOperatorAreNotUnequal()
        {
            Assert.AreNotEqual(true, this.Dollars_50 != this.AnotherDollars_50);
        }

        [Test]
        public void ComparingTwoMoneysWithDiffentAmountAndSameTypeOfCurrenrcyUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_50 != this.Dollars_40);
        }

        [Test]
        public void ComparingTwoMoneysWithSameAmountAndDiffentTypeOfCurrencyUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_40 != this.ArgentinePesos_40);
        }

        [Test]
        public void ComparingTwoMoneysWithDiffentAmountAndDiffentTypeOfCurrencyUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_50 != this.ArgentinePesos_40);
        }

        [Test]
        public void ComparingMoneyWithNullUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_50 != null);
        }        

        #endregion

        #region Equality Method Tests

        [Test]
        public void ComparingTwoMoneysWithSameAmountAndSameTypeOfCurrencyUsingEqualMethodAreEqual()
        {
            Assert.AreEqual(true, this.Dollars_50.Equals(this.AnotherDollars_50));
        }

        [Test]
        public void ComparingTwoMoneysWithDifferentAmountAndSameTypeOfCurrencyUsingEqualMethodAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_50.Equals(this.Dollars_40));
        }

        [Test]
        public void ComparingTwoMoneysWithSameAmountAndDifferentTypeOfCurrencyUsingEqualMethodAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_40.Equals(this.ArgentinePesos_40));
        }

        [Test]
        public void ComparingTwoMoneysWithDifferentAmountAndDifferentTypeOfCurrencyUsingEqualMethodAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_50.Equals(this.ArgentinePesos_40));
        }

        [Test]
        public void ComparingMoneyWithNullUsingEqualMethodAreNotEqual()
        {
            Assert.AreNotEqual(true, this.Dollars_50.Equals(null));
        }

        [Test]
        public void ComparingMoneyWithNonMoneyClassTypeUsingEqualMethodDoesNotThrowException()
        {            
            Assert.That(() => this.Dollars_50.Equals((object)10), Throws.Nothing);
        }       

        #endregion

        #region GetHashCode Tests

        [Test]
        public void HashCodeOfTwoMoneysWithSameAmountAndSameTypeOfCurrencyAreEqual()
        {
            Assert.AreEqual(this.Dollars_50.GetHashCode(), this.AnotherDollars_50.GetHashCode());
        }

        [Test]
        public void HashCodeOfTwoMoneysWithDifferentAmountAndSameTypeOfCurrencyAreNotEqual()
        {
            Assert.AreNotEqual(this.Dollars_50.GetHashCode(), this.Dollars_40.GetHashCode());
        }

        [Test]
        public void HashCodeOfTwoMoneysWithSameAmountAndDifferentTypeOfCurrencyAreNotEqual()
        {
            Assert.AreNotEqual(this.Dollars_40.GetHashCode(), this.ArgentinePesos_40.GetHashCode());
        }

        [Test]
        public void HashCodeOfTwoMoneysWithDifferentAmountAndDifferentTypeOfCurrencyAreNotEqual()
        {
            Assert.AreNotEqual(this.Dollars_50.GetHashCode(), this.ArgentinePesos_40.GetHashCode());
        }       

        #endregion
    }
}
