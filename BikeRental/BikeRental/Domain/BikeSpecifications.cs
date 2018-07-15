using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public class BikeSpecifications
    {
        public string Brand { get; }

        public string Model { get; }

        public string Color { get; }

        public BikeSpecifications(string brand, string model, string color)
        {
            this.Brand = brand;
            this.Model = model;
            this.Color = color;
        }

    }
}
