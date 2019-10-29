using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Application.Commands;
using Barigui.Infrastructure.Broker.Interfaces;
using Domain.Entities;
using Google.Protobuf;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandsHandlers
{
    public class PublishKinesisHandler : AsyncRequestHandler<PublishKinesis>
    {
        private readonly AwsCredentials _awsCredentials;
        protected IAmazonKinesis _client { get; }

        public PublishKinesisHandler(IOptions<AwsCredentials> awsCredentials)
        {
            _awsCredentials = awsCredentials.Value;
            _client = new AmazonKinesisClient(_awsCredentials.AccessKey, _awsCredentials.SecretKey, _awsCredentials.TokenKey, RegionEndpoint.USEast1);
        }

        protected override async Task Handle(PublishKinesis request, CancellationToken cancellationToken)
        {
            await ProduceAsync(request.TopicName, request.KeyName, request.Payload);
        }

        private async Task ProduceAsync(string topic, string key, IMessage value)
        {
            try
            {

                using (var stream = new MemoryStream())
                {
                    value.WriteTo(stream);

                    var putRecordRequest = new PutRecordRequest()
                    {
                        StreamName = topic,
                        PartitionKey = key,
                        Data = stream
                    };


                    var result = await _client.PutRecordAsync(putRecordRequest);

                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        var base64 = Convert.ToBase64String(value.ToByteArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
