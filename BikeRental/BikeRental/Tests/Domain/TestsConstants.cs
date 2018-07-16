using BikeRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Tests.Domain
{
    public static class TestsConstants
    {
        public const string BIKE_BRAND = "Schwinn";
        public const string BIKE_MODEL = "Continental Commuter 7";
        public const string BIKE_COLOR = "Black";
        public const string BIKE_IDENTIFICATION_CODE = "ABC043";

        public const decimal RENTAL_BY_HOUR_AMOUNT = 5;
        public const Currency RENTAL_BY_HOUR_TYPE_OF_CURRENCY = Currency.Dollar;
        public const decimal RENTAL_BY_DAY_AMOUNT = 20;
        public const Currency RENTAL_BY_DAY_TYPE_OF_CURRENCY = Currency.Dollar;
        public const decimal RENTAL_BY_WEEK_AMOUNT = 60;
        public const Currency RENTAL_BY_WEEK_TYPE_OF_CURRENCY = Currency.Dollar;

        public const string PERSON_ID_NUMBER = "33162982";
        public const string PERSON_NAME = "Frederick";
        public const string PERSON_LAST_NAME = "Jane";        
        public const string PERSON_PHONE = "1-541-754-3010";
        public const string PERSON_EMAIL = "fjane@test.com";
        
        public const string FAMILY_RENTAL_TERMS_AND_CONDITIONS = "The Family Rental promotion will begin on May 1st and end on August 1st, 2018. " +
            "There will be a discount of 30 percent making between 3 and 5 rentals. " +
            "Any person over 16 years of age will be eligible to participate. The promotion is subject to the availability of bikes.";
        public const int FAMILY_RENTAL_EFFECTIVE_DATE_YEAR = 2018;
        public const int FAMILY_RENTAL_EFFECTIVE_DATE_MONTH = 5;
        public const int FAMILY_RENTAL_EFFECTIVE_DATE_DAY = 1;
        public const int FAMILY_RENTAL_EXPIRATON_DATE_YEAR = 2018;
        public const int FAMILY_RENTAL_EXPIRATON_DATE_MONTH = 8;
        public const int FAMILY_RENTAL_EXPIRATON_DATE_DAY = 1;
        public const float FAMILY_RENTAL_DISCOUNT_PERCENT = 30;
        public const int FAMILY_RENTAL_MINIMUM_RENTALS = 3;
        public const int FAMILY_RENTAL_MAXIMUM_RENTALS = 5;

    }
}
