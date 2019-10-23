using Application.Commands;
using Domain.Entities;
using MediatR;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandsHandlers
{
    public class DeleteHandler : IRequestHandler<DeleteCommand, Customer>
    {
        private readonly IRemove _removeRepository;

        public DeleteHandler(IRemove removeRepository)
        {
            _removeRepository = removeRepository;
        }
        public async Task<Customer> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await _removeRepository.Delete(request.Customer);
        }
    }
}
