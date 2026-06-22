namespace TNRelay.Config
{
    /// <summary>
    /// Single TrueNAS source (Robert or Davos)
    /// </summary>
    public class TrueNasSource
    {
        /// <summary>
        /// Source name (used as dedupe key prefix)
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Base URL, e.g. https://robert.local
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// TrueNAS API key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}