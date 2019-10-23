using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MediatR;
using Persistence.DynamoDB.Services;
using Persistence.Interfaces;

namespace DependencyInjection
{
    public class InjectionContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Reader>().As<IReader>().InstancePerLifetimeScope();
            builder.RegisterType<Write>().As<IWrite>().InstancePerLifetimeScope();
            builder.RegisterType<Remove>().As<IRemove>().InstancePerLifetimeScope();

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            

        }
    }
}
