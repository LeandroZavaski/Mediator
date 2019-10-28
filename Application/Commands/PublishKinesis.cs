﻿using Google.Protobuf;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class PublishKinesis : IRequest
    {
        public string TopicName { get; internal set; }

        public string KeyName { get; internal set; }

        public IMessage Payload { get; internal set; }


        public PublishKinesis(string topicName, string keyName, IMessage payload)
        {
            TopicName = topicName;
            KeyName = keyName;
            Payload = payload;
        }
    }
}