using Application.Commands;
using Application.Services.Interfaces;
using Barigui;
using Domain.Entities;
using Google.Protobuf;
using MediatR;
using ProtoBuf;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;
        private const string _streamNameOnboarding = "TesteCustomerStream";

        public CustomerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ProcessBaseEvent(BaseEvent baseEvent)
        {
            await _mediator.Send(new PublishKinesis(_streamNameOnboarding, baseEvent.Subject, baseEvent));
        }
    }
}
