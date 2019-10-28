using Amazon;
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
        private readonly IBrokerProducer _brokerProducer;
        
        public PublishKinesisHandler(IBrokerProducer brokerProducer, IOptions<AwsCredentials> awsCredentials)
        {
            _brokerProducer = brokerProducer;
        }

        protected override async Task Handle(PublishKinesis request, CancellationToken cancellationToken)
        {
            await _brokerProducer.ProduceAsync(request.TopicName, request.KeyName, request.Payload);
        }
    }
}
