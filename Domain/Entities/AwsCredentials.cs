using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AwsCredentials
    {
        public string Profile { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string TokenKey { get; set; }
        public string Region { get; set; }
    }
}
