using Application.Services;
using Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Xunit;

namespace Web.Test
{
    public class CustomerControllerTest
    {
        private readonly CustomerController _customerController;
        private readonly ICustomerService _customerService;
        private readonly IMediator _mediator;

        public CustomerControllerTest(IMediator mediator)
        {
            _mediator = mediator;
            _customerService = new CustomerService(_mediator);
            _customerController = new CustomerController(_mediator, _customerService);
        }

        #region GetById

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _customerController.Get(Guid.NewGuid().ToString());

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        #endregion
    }
}
