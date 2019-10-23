using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class CreateCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }

        public CreateCommand(Customer customer)
        {
            Customer = customer;
        }
    }
}
