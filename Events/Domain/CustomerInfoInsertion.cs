using Domain.Entities;
using Events.Domain;

namespace Barigui.Prototipo.Events.Domain
{
    public partial class CustomerInfoInsertion : BaseCustomerEvent
    {
        public override EventId EventId => Id;

        public override void Handle(Customer customer)
        {
            customer.Nome = Nome;
            customer.Email = Email;
            customer.Telefone = Telefone;
            customer.Idade = Idade;
        }
    }
}
