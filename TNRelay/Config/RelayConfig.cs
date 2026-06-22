namespace TNRelay.Config
{
    /// <summary>
    /// Relay section
    /// </summary>
    public class RelayConfig
    {
        /// <summary>
        /// Endpoint to POST new alerts to
        /// </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// Api key for endpoint
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}