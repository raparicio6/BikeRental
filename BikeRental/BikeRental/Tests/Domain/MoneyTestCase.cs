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
        private Money Argentine_Peso_40;
        private Money Another_Dollars_50;

        [SetUp]
        public void SetUp()
        {
            this.Dollars_50 = new Money(50, TypeOfCurrency.Dollar);
            this.Dollars_40 = new Money(40, TypeOfCurrency.Dollar);
            this.Argentine_Peso_40 = new Money(40, TypeOfCurrency.Argentine_Peso);
            this.Another_Dollars_50 = new Money(50, TypeOfCurrency.Dollar);
        }

        #region Equality Operator Tests

        [TestCase]
        public void ComparingTwoMoneysWithSameAmountAndSameTypeOfCurrencyUsingEqualOperatorAreEqual()
        {
            Assert.AreEqual(true, this.Dollars_50 == this.Another_Dollars_50);
        }

        [TestCase]
        public void ComparingTwoMoneysWithDiffentAmountAndSameTypeOfCurrenrcyUsingEqualOperatorArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_50 == this.Dollars_40);
        }

        [TestCase]
        public void ComparingTwoMoneysWithSameAmountAndDiffentTypeOfCurrencyUsingEqualOperatorArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_40 == this.Argentine_Peso_40);
        }

        [TestCase]
        public void ComparingTwoMoneysWithDiffentAmountAndDiffentTypeOfCurrencyUsingEqualOperatorArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_50 == this.Argentine_Peso_40);
        }

        [TestCase]
        public void ComparingMoneyWithNullUsingEqualOperatorArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_50 == null);
        }       

        #endregion

        #region Inequality Operator Tests

        [TestCase]
        public void ComparingTwoMoneysWithSameAmountAndSameTypeOfCurrencyUsingInequalOperatorAreNotUnequal()
        {
            Assert.AreEqual(false, this.Dollars_50 != this.Another_Dollars_50);
        }

        [TestCase]
        public void ComparingTwoMoneysWithDiffentAmountAndSameTypeOfCurrenrcyUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_50 != this.Dollars_40);
        }

        [TestCase]
        public void ComparingTwoMoneysWithSameAmountAndDiffentTypeOfCurrencyUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_40 != this.Argentine_Peso_40);
        }

        [TestCase]
        public void ComparingTwoMoneysWithDiffentAmountAndDiffentTypeOfCurrencyUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_50 != this.Argentine_Peso_40);
        }

        [TestCase]
        public void ComparingMoneyWithNullUsingInequalOperatorAreUnequal()
        {
            Assert.AreEqual(true, this.Dollars_50 != null);
        }        

        #endregion

        #region Equality Method Tests

        [TestCase]
        public void ComparingTwoMoneysWithSameAmountAndSameTypeOfCurrencyUsingEqualMethodAreEqual()
        {
            Assert.AreEqual(true, this.Dollars_50.Equals(this.Another_Dollars_50));
        }

        [TestCase]
        public void ComparingTwoMoneysWithDifferentAmountAndSameTypeOfCurrencyUsingEqualMethodArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_50.Equals(this.Dollars_40));
        }

        [TestCase]
        public void ComparingTwoMoneysWithSameAmountAndDifferentTypeOfCurrencyUsingEqualMethodArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_40.Equals(this.Argentine_Peso_40));
        }

        [TestCase]
        public void ComparingTwoMoneysWithDifferentAmountAndDifferentTypeOfCurrencyUsingEqualMethodArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_50.Equals(this.Argentine_Peso_40));
        }

        [TestCase]
        public void ComparingMoneyWithNullUsingEqualMethodArentEqual()
        {
            Assert.AreEqual(false, this.Dollars_50.Equals(null));
        }

        [TestCase]
        public void ComparingMoneyWithNonMoneyClassTypeUsingEqualMethodDoesntThrowException()
        {            
            Assert.That(() => this.Dollars_50.Equals((object)10), Throws.Nothing);
        }       

        #endregion

        #region GetHashCode Tests

        [TestCase]
        public void HashCodeOfTwoMoneysWithSameAmountAndSameTypeOfCurrencyAreEqual()
        {
            Assert.AreEqual(this.Dollars_50.GetHashCode(), this.Another_Dollars_50.GetHashCode());
        }

        [TestCase]
        public void HashCodeOfTwoMoneysWithDifferentAmountAndSameTypeOfCurrencyArentEqual()
        {
            Assert.AreNotEqual(this.Dollars_50.GetHashCode(), this.Dollars_40.GetHashCode());
        }

        [TestCase]
        public void HashCodeOfTwoMoneysWithSameAmountAndDifferentTypeOfCurrencyArentEqual()
        {
            Assert.AreNotEqual(this.Dollars_40.GetHashCode(), this.Argentine_Peso_40.GetHashCode());
        }

        [TestCase]
        public void HashCodeOfTwoMoneysWithDifferentAmountAndDifferentTypeOfCurrencyArentEqual()
        {
            Assert.AreNotEqual(this.Dollars_50.GetHashCode(), this.Argentine_Peso_40.GetHashCode());
        }       

        #endregion
    }
}
