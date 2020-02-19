using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Core.Models;
using CustomerApi.Persistence;
using CustomerApi.Core;
using AutoMapper;
using CustomerResources;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResource>>> GetCustomers()
        {
            // to craete database
            //await _unitOfWork.EnsureCreatedAsync();

            var customers = await _unitOfWork.Customers.GetCustomersAsync();
             return Ok(_mapper.Map<List<CustomerResource>>(customers));
        }


        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResource>> GetCustomer(Guid id)
        {
            var customer = await _unitOfWork.Customers.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<Customer, CustomerResource>(customer);
            return Ok(result);
        }

    }
}
