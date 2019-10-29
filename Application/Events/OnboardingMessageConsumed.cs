using Barigui.Infrastructure.Events.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public class OnboardingMessageConsumed : MessageConsumed, INotification
    {
        public OnboardingMessageConsumed(MessageConsumed message) : base(message.Id, message.Message)
        {

        }
    }
}
