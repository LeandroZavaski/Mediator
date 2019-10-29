using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CustomerEvent
    {
        public string Id { get; set; }

        public string DeviceId { get; set; }

        public DateTime LastUpdate { get; set; }

        public string EventType { get; set; }

        public string Payload { get; set; }
    }
}
