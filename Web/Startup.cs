using System;
using System.Reflection;
using Amazon.DynamoDBv2;
using Application.CommandsHandlers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DynamoDB.Services;
using Persistence.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace Web
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static IConfiguration Configuration { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });

            services.AddOptions();
            services.AddMediatR(typeof(GetByIdHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteHandler).GetTypeInfo().Assembly);


            services.Configure<AwsCredentials>(Configuration.GetSection("AwsCredentials"));
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>(Configuration.GetAWSOptions(), ServiceLifetime.Transient);

            services.AddTransient<IReader, Reader>();
            services.AddTransient<IWrite, Write>();
            services.AddTransient<IRemove, Remove>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UsePathBase(Environment.GetEnvironmentVariable("ASPNETCORE_BASEPATH"));

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"../swagger/v1/swagger.json", "API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
