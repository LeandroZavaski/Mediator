using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class UpdateCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }

        public UpdateCommand(Customer customer)
        {
            Customer = customer;
        }
    }
}
