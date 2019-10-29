using Amazon.DynamoDBv2;
using Domain.Entities;
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
    public class EventWriter : DynamoDBTable, IEventWriter
    {
        public EventWriter(IAmazonDynamoDB database, IOptions<AwsCredentials> awsCredentials) : base(database, awsCredentials)
        {

        }

        public async Task SaveLeadEventAsync(CustomerEvent customerEvent)
        {
            var json = JsonConvert.SerializeObject(customerEvent);
            await Table.PutItemAsync(Amazon.DynamoDBv2.DocumentModel.Document.FromJson(json));
        }
    }
}
