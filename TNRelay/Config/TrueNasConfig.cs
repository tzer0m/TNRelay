namespace TNRelay.Config
{
    /// <summary>
    /// TrueNAS section
    /// </summary>
    public class TrueNasConfig
    {
        /// <summary>
        /// List of TrueNAS sources to poll
        /// </summary>
        public List<TrueNasSource> Sources { get; set; } = [];

        /// <summary>
        /// Minimum severity level to forward (inclusive)
        /// </summary>
        public string MinSeverity { get; set; } = "Critical";
    }
}