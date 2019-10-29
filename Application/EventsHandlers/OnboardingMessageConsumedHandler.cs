using Application.Events;
using Barigui.Prototipo.Events;
using Domain.Entities;
using Events.Domain;
using MediatR;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventsHandlers
{
    public class OnboardingMessageConsumedHandler : INotificationHandler<OnboardingMessageConsumed>
    {
        private readonly IWrite _write;
        private readonly IEventWriter _eventWriter;
        private readonly IReader _reader;


        public OnboardingMessageConsumedHandler(IWrite write, IEventWriter eventWriter, IReader reader)
        {
            _write = write;
            _eventWriter = eventWriter;
            _reader = reader;
        }

        public async Task Handle(OnboardingMessageConsumed notification, CancellationToken cancellationToken)
        {
            switch (notification.Message)
            {
                //Since lead creation is an action on its own, let it be the only available way to create one
                case NewCustomerCreated newLead:
                    var customer = await _reader.GetByIdAsync(newLead.EventId.CustomerId);
                    if (customer is null)
                    {
                        customer = new Customer();

                        await UpdateLeadAndRegisterEventAsync(customer, newLead);
                    }
                    break;
                case BaseCustomerEvent customerEvent:
                    var existingLead = await _reader.GetByIdAsync(customerEvent.EventId.CustomerId);

                    //If lead doesn't exist, ignore object update
                    if (existingLead is null)
                    {
                        await _eventWriter.SaveCustomerEventAsync(customerEvent.ToCustomerEvent());
                    }
                    else
                    {
                        await UpdateLeadAndRegisterEventAsync(existingLead, customerEvent);
                    }

                    break;
            }
        }

        private async Task UpdateLeadAndRegisterEventAsync(Customer customer, BaseCustomerEvent leadEvent)
        {
            await _eventWriter.SaveCustomerEventAsync(leadEvent.ToCustomerEvent());

            leadEvent.Handle(customer);

            await _write.Save(customer);
        }
    }
}
