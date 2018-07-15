using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Money : IEquatable<Money>
    {
        public decimal Amount { get; }

        public TypeOfCurrency TypeOfCurrency { get; }

        public Money(decimal amount, TypeOfCurrency typeOfCurrency)
        {
            this.Amount = amount;
            this.TypeOfCurrency = typeOfCurrency;
        }

        #region Equality

        public bool Equals(Money other)
        {
            return other == null ? false : (this == other);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Money;
            return other == null ? false : Equals(other);
        }  

        public static bool operator == (Money money1, Money money2)
        {
            if ((object)money1 == null || (object)money2 == null)
                return false;

            return (money1.Amount == money2.Amount &&
                money1.TypeOfCurrency == money2.TypeOfCurrency) ? true : false;
        }

        public static bool operator != (Money money1, Money money2)
        {
            return !(money1 == money2);
        }

        #endregion

        public override int GetHashCode()
        {
            return this.Amount.GetHashCode() + this.TypeOfCurrency.GetHashCode();
        }

    }
}
