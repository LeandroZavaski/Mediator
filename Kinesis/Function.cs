using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Application.Events;
using Barigui.Prototipo.Events.Helpers;
using MediatR;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Kinesis
{
    public class Function
    {
        private readonly IMediator _mediator;

        public Function(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandlerAsync(KinesisEvent evnt, ILambdaContext context)
        {
            foreach (var message in evnt.Records)
            {
                await ProcessMessageAsync(message);
            }
        }

        private async Task ProcessMessageAsync(KinesisEvent.KinesisEventRecord message)
        {
            try
            {

                var deserializer = new Deserializer();

                var messageDeserialized = deserializer.Deserialize(message.Kinesis.Data.ToArray());

                var customerMessageConsumed = new OnboardingMessageConsumed(messageDeserialized);


                await _mediator.Publish(customerMessageConsumed);
            }
            catch (Exception E)
            {
                var base64 = Convert.ToBase64String(message.Kinesis.Data.ToArray());
            }
        }
    }
}
