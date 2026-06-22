namespace TNRelay.Models
{
    /// <summary>
    /// TrueNAS alert severity levels, in ascending order of severity
    /// </summary>
    public enum AlertLevel
    {
        Info = 0,
        Notice = 1,
        Warning = 2,
        Error = 3,
        Critical = 4,
        Alert = 5,
        Emergency = 6
    }
}