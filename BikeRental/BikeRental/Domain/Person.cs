using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class Person
    {
        public long IdNumber { get; }

        public string Name { get; }

        public string LastName { get; }

        public string Phone { get; }

        public string Email { get; }

        public IList<Role> Roles { get; }

        public Person(long idNumber, string name, string lastName, string phone, string email)
        {
            this.IdNumber = idNumber;
            this.Name = name;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;
            this.Roles = new List<Role>();
        }

        public void AddRole(Role role)
        {
            this.Roles.Add(role);
        }

        public Role GetRoleByName(string roleName)
        {
            return this.Roles.Where(role => role.GetName() == roleName).First();
        }

    }
}
