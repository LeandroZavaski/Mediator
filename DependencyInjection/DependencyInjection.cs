using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceProvider ConfigureServices(this IServiceCollection services, string serviceName)
        {
            return null;
        }

        public static void ConfigureOptions(this IServiceCollection services)
        {

        }

        public static void SetupConfigurationBuilder(this IConfigurationBuilder configurationBuilder)
        {

        }

        public static void RegisterModules(this ContainerBuilder builder)
        {

        }

        public static IConfigurationRoot GetConfiguration()
        {
            return null;
        }
    }
}
