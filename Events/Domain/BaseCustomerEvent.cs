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
    }
}
