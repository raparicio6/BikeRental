﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain
{
    public interface ISale
    {
        IClient Client { get; }
        Money Money { get; }
        bool IsPaid();
        Payment Payment { get; set; }

    }
}
