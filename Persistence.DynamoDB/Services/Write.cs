﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
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
    public class Write : DynamoDBTable, IWrite
    {
        public Write(IAmazonDynamoDB database, IOptions<AwsCredentials> awsCredentials) : base(database, awsCredentials)
        {
        }

        public async Task<Customer> Save(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var document = await Table.UpdateItemAsync(Document.FromJson(json));
            return (document != null)
                ? JsonConvert.DeserializeObject<Customer>(document.ToJson())
                : null;
        }

        public async Task<Customer> Update(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var document = await Table.UpdateItemAsync(Document.FromJson(json));
            return (document != null)
                ? JsonConvert.DeserializeObject<Customer>(document.ToJson())
                : null;
        }
    }
}
