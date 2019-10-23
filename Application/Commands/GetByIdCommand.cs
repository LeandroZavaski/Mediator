using Application.Commands.Interfaces;
using Domain.Entities;
using MediatR;
using System;

namespace Application.Commands
{
    public class GetByIdCommand : ICommand, IRequest<Customer>
    {
        public string Id { get; set; }

        public GetByIdCommand(string id)
        {
            Id = id;
        }
    }
}
