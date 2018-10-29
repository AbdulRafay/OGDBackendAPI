using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using APIClient.Contracts;
using APIClient.Impl;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieSearch;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static void RegisterSearchServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Do your setup here
            services.AddTransient<IMovieRatingAPI, ImplIMovieRatingAPI>();
            services.AddTransient<IVideoAPI, ImplVideoAPI>();
            services.AddAutoMapper();
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
            .AddJsonFile("searchsettings.json");
            configuration = builder.Build();
            services.AddOptions();
            services.Configure<SearchConfig>(configuration.GetSection("SearchConfig"));
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";                
                
            });
        }
    }
}
