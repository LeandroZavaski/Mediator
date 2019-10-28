using Barigui;
using Google.Protobuf;
using Newtonsoft.Json;
using System;
//using Barigui;

namespace Web.ApiModels
{
    public abstract class CustomerBaseRequest
    {
        [JsonIgnore]
        public string RequestId { get; set; } = Guid.NewGuid().ToString();

        [JsonIgnore]
        public string CustomerId { get; set; }

        protected abstract IMessage GetEvent();

        public static explicit operator BaseEvent(CustomerBaseRequest x)
        {
            var baseEvent = new BaseEvent(x.GetEvent())
            {
                Subject = x.CustomerId
            };

            return baseEvent;
        }
    }
}
