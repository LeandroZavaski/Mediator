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
    public class GetByIdHandler : IRequestHandler<GetByIdCommand, Customer>
    {
        private readonly IReader _readRepository;

        public GetByIdHandler(IReader readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Customer> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetByIdAsync(request.Id);
        }
    }
}
