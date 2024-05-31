using Microsoft.Extensions.Configuration;
using System.IO;

namespace demo_Qr_Mqtt
{
    public static class ConfigHelper
    {
        private static IConfigurationRoot Configuration { get; }

        static ConfigHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Log to check if configuration is loaded
            Console.WriteLine("Configuration loaded.");
        }

        public static string GetAppSetting(string key)
        {
            var value = Configuration[key];
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine($"Warning: Configuration key '{key}' not found or empty.");
            }
            else
            {
                Console.WriteLine($"Configuration key '{key}': '{value}'");
            }
            return value;
        }

        public static string GetConnectionString(string name)
        {
            var value = Configuration.GetConnectionString(name);
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine($"Warning: Connection string '{name}' not found or empty.");
            }
            else
            {
                Console.WriteLine($"Connection string '{name}': '{value}'");
            }
            return value;
        }
    }
}
