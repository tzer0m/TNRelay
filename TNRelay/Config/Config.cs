using Microsoft.Extensions.Configuration;

namespace TNRelay.Config
{
    /// <summary>
    /// Strongly-typed application configuration
    /// </summary>
    public class Config
    {
        /// <summary>
        /// TrueNAS settings
        /// </summary>
        public TrueNasConfig TrueNas { get; set; } = new();

        /// <summary>
        /// Postgres settings
        /// </summary>
        public PostgresConfig Postgres { get; set; } = new();

        /// <summary>
        /// Relay settings
        /// </summary>
        public RelayConfig Relay { get; set; } = new();

        /// <summary>
        /// Load config from appsettings.json, user secrets (Development only) and environment variables
        /// </summary>
        /// <returns>Loaded config</returns>
        public static Config Load()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            if (string.Equals(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"), "Development", StringComparison.OrdinalIgnoreCase))
                builder.AddUserSecrets<Config>();
            IConfigurationRoot configuration = builder.AddEnvironmentVariables().Build();

            Config config = new();
            configuration.Bind(config);
            return config;
        }
    }
}