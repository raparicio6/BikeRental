using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class EmployeeData
    {
        public Person Person { get; }

        public long IdNumberInCompany { get; }

        public Money CurrentSalary { get; set; }

        public DateTime DateOfEntry { get; }

        public DateTime? DateOfExit { get; set;  }

        public EmployeeData(Person person, long idNumberInCompany, Money currentSalary, DateTime dateOfEntry)
        {
            this.Person = person;
            this.IdNumberInCompany = idNumberInCompany;
            this.CurrentSalary = currentSalary;
            this.DateOfEntry = dateOfEntry;
            this.DateOfExit = null;
        }

    }
}
