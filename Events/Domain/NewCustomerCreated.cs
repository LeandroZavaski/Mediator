using Domain.Entities;
using Events.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barigui.Prototipo.Events
{
    public partial class NewCustomerCreated : BaseCustomerEvent
    {
        public override EventId EventId => Id;

        public override void Handle(Customer customer)
        {
            customer.Id = Id.CustomerId;
            customer.Nome = Nome;
            customer.Email = Email;
            customer.Telefone = Telefone;
            customer.Idade = Idade;
        }
    }
}
