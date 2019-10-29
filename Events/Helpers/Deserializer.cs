using Barigui.Infrastructure.Events.Deserializer;
using Barigui.Infrastructure.Events.Events;
using Barigui.Infrastructure.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barigui.Prototipo.Events.Helpers
{
    public class Deserializer : BaseDeserializer, IDeserializer
    {
        public MessageConsumed Deserialize(byte[] bytes)
        {
            var @event = base.EventDeserialize(bytes);
            return new MessageConsumed(@event.GetId(), @event);
        }
    }
}
