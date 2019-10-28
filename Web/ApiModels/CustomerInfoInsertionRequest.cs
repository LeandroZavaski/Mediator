using Barigui.Prototipo.Events;
using Google.Protobuf;

namespace Web.ApiModels
{
    public class CustomerInfoInsertionRequest : CustomerBaseRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Idade { get; set; }

        protected override IMessage GetEvent()
        {
            return new CustomerInfoInsertion()
            {
                Id = new EventId()
                {
                    CustomerId = CustomerId,
                    RequestId = RequestId
                },
                Nome = Nome,
                Email = Email,
                Telefone = Telefone,
                Idade = Idade,

            };
        }
    }
}
 