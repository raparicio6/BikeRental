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

        public DateTime EntryDate { get; }

        public DateTime? ExitDate { get; set;  }

        public EmployeeData(Person person, long idNumberInCompany, Money currentSalary, DateTime entryDate)
        {
            this.Person = person;
            this.IdNumberInCompany = idNumberInCompany;
            this.CurrentSalary = currentSalary;
            this.EntryDate = entryDate;
            this.ExitDate = null;
        }

    }
}
