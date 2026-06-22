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
        /// Load config from appsettings.json
        /// </summary>
        /// <returns>Loaded config</returns>
        public static Config Load()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            Config config = new();
            configuration.Bind(config);
            return config;
        }
    }
}