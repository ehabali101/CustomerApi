using CustomerApi.Core.Models;
using CustomerApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private ICustomerDbContext _context;

        public CustomerRepository(ICustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> CustomerExists(Guid id)
        {
            return await _context.Customers.AnyAsync(e => e.Id == id);
        }
    }
}
