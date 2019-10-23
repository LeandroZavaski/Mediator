using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class DeleteCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }

        public DeleteCommand(Customer cuastomer)
        {
            Customer = cuastomer;
        }
    }
}
