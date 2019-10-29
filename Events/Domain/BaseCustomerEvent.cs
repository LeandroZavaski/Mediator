using System;
using Barigui.Infrastructure.Events.Interfaces;
using Barigui.Prototipo.Events;
using Domain.Entities;

namespace Events.Domain
{
    public abstract class BaseCustomerEvent : IAggregateEvent
    {
        public abstract EventId EventId { get; }

        public abstract void Handle(Customer customer);

        public string GetId() => EventId.CustomerId;


        public void Handle(object entity)
        {
            var customer = entity as Customer;
            customer.Id = EventId.CustomerId;
            Handle(customer);
        }

        public CustomerEvent ToCustomerEvent()
        {
            var lead = new Customer();
            Handle(lead);
            var evnt = new CustomerEvent()
            {
                Id = EventId.CustomerId + Guid.NewGuid().ToString("N"),
                DeviceId = "",
                EventType = GetType().ToString(),
                LastUpdate = DateTime.UtcNow,
                Payload = ToString()
            };
            return evnt;
        }
    }
}
