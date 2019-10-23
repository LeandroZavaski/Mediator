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
    public class CreateHandler : IRequestHandler<CreateCommand, Customer>
    {
        private readonly IWrite _writeRepository;

        public CreateHandler(IWrite writeRepository)
        {
            _writeRepository = writeRepository;
        }
        public async Task<Customer> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            return await _writeRepository.Save(request.Customer);
        }
    }
}
