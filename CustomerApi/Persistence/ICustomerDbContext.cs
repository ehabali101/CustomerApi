using CustomerApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Persistence
{
    public interface ICustomerDbContext
    {
        DbSet<Customer> Customers { get; set; }
    }
}
