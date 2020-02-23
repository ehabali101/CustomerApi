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
                new Customer{ Id = new Guid("e5e133d6-b245-4ced-bde3-e047888de611"), Name = "Kalles Grustransporter AB", Address = "Cementvägen 8, 111 11 Södertälje " },
                new Customer{ Id = new Guid("91725592-44ff-4b58-ad30-5fd1e6933a42"), Name ="Johans Bulk AB", Address ="Balkvägen 12, 222 22 Stockholm" },
                new Customer{ Id = new Guid("3679d5bf-5314-4b47-8187-373151dc22ea"), Name = "Haralds Värdetransporter AB ", Address = "Budgetvägen 1, 333 33 Uppsala"}
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
