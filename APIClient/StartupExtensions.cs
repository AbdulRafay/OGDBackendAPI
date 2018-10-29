using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using APIClient;
using APIClient.Contracts;
using APIClient.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RestSharp;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static void RegisterAPIClientServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Do your setup here
            services.AddTransient<IRestClient, RestClient>();
            IConfigurationBuilder builder = new ConfigurationBuilder()
             .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
             .AddJsonFile("apisettings.json");
            configuration = builder.Build();
            services.AddOptions();
            services.Configure<APIConfig>(configuration.GetSection("APIConfig"));

        }
    }
}