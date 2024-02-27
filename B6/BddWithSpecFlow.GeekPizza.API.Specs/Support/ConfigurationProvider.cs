using System;
using Microsoft.Extensions.Configuration;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    public static class ConfigurationProvider
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public static string GetSetting(string key)
        {
            var config = InitConfiguration();
            return config[key];
        }
    }
}
