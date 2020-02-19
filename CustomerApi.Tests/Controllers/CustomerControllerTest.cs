using AutoMapper;
using CustomerApi.Controllers;
using CustomerApi.Core;
using CustomerApi.Core.Models;
using CustomerApi.Core.Repositories;
using CustomerApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests.Controllers
{
    public class CustomerControllerTest
    {
        CustomersController _controller;
        private Mock<ICustomerRepository> _mockRepository;
        IMapper _mapper;

 
        public CustomerControllerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();

            _mockRepository = new Mock<ICustomerRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Customers).Returns(_mockRepository.Object);
            _controller = new CustomersController(mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task GetCustomers_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockRepository.Setup(service => service.GetCustomersAsync()).ReturnsAsync(
                new List<Customer>
                {
                    new Customer
                    {
                        Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                        Name = "Customer 1"
                    },
                    new Customer
                    {
                        Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                        Name = "Customer 2"
                    }
                }
            );

            // Act
            var result = await _controller.GetCustomers();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);   
            Assert.IsType<OkObjectResult>(okResult);
        }


        [Fact]
        public async Task GetCustomer_WhenCalled_ReturnsOkResult()
        {

            // Arrange
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            _mockRepository.Setup(repo => repo.GetCustomerAsync(id))
                .ReturnsAsync((new Customer
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Csutomer 1"
                }));

            // Act
            var result = await _controller.GetCustomer(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetCustomer_WhenCalled_ReturnsNotFound()
        {
            // Arrange
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var result = await _controller.GetCustomer(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }


    }
}
