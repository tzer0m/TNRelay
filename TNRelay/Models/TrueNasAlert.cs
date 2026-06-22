using System.Text.Json.Serialization;

namespace TNRelay.Models
{
    /// <summary>
    /// A single alert returned from TrueNAS's /api/v2.0/alert/list endpoint
    /// </summary>
    public class TrueNasAlert
    {
        /// <summary>
        /// Unique alert identifier (stable across occurrences of the same issue)
        /// </summary>
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; } = string.Empty;

        /// <summary>
        /// Severity level: INFO, NOTICE, WARNING, ERROR, CRITICAL, ALERT
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// Human-readable alert message
        /// </summary>
        [JsonPropertyName("formatted")]
        public string? Formatted { get; set; }

        /// <summary>
        /// Raw alert text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Whether the alert has been dismissed in the TrueNAS UI
        /// </summary>
        [JsonPropertyName("dismissed")]
        public bool Dismissed { get; set; }
    }
}