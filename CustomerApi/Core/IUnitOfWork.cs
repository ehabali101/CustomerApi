using CustomerApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Core
{
    public interface IUnitOfWork
    {
        Task EnsureCreatedAsync();

        ICustomerRepository Customers { get; }

        Task Complete();
    }
}
