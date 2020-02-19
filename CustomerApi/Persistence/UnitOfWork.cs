using CustomerApi.Core;
using CustomerApi.Core.Models;
using CustomerApi.Core.Repositories;
using CustomerApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private CustomerDbContext _context;
        public ICustomerRepository Customers { get; private set; }

        public UnitOfWork(CustomerDbContext context)
        {
            _context = context;

            Customers = new CustomerRepository(_context);
        }
    
        public async Task EnsureCreatedAsync()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await WriteCustomersAsync();
        }

        private async Task WriteCustomersAsync()
        {
            var customers = new List<Customer>()
            {
                new Customer{ Id = new Guid(), Name = "Kalles Grustransporter AB", Address = "Cementvägen 8, 111 11 Södertälje " },
                new Customer{ Id = new Guid(), Name ="Johans Bulk AB", Address ="Balkvägen 12, 222 22 Stockholm" },
                new Customer{ Id = new Guid(), Name = "Haralds Värdetransporter AB ", Address = "Budgetvägen 1, 333 33 Uppsala"}
            };

            _context.Customers.AddRange(customers);
            await _context.SaveChangesAsync();
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}
