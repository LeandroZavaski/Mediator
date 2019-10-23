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
    public class UpdateHandler : IRequestHandler<UpdateCommand, Customer>
    {
        private readonly IWrite _writeRepository;
        public UpdateHandler(IWrite writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<Customer> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            return await _writeRepository.Update(request.Customer);
        }
    }
}
