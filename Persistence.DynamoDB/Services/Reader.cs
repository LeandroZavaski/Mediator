using Amazon.DynamoDBv2;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Persistence.DynamoDB.Abstractions;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DynamoDB.Services
{
    public class Reader : DynamoDBTable, IReader
    {
        public Reader(IAmazonDynamoDB database, IOptions<AwsCredentials> awsCredentials) : base(database, awsCredentials)
        {
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            var document = await Table.GetItemAsync(id);
            return (document != null)
                ? JsonConvert.DeserializeObject<Customer>(document.ToJson())
                : null;
        }
    }
}
