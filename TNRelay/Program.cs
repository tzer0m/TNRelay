using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using t0m.Ting;
using TNRelay;
using TNRelay.Config;
using TNRelay.Models;

Config config = Config.Load();

AlertStore alertStore = new(config.Postgres);

// Build a minimal DI container just to construct TingClient via AddTingClient
IConfiguration tingConfiguration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
    {
        ["Ting:BaseUrl"] = config.Relay.Endpoint,
        ["Ting:ApiKey"] = config.Relay.ApiKey
    }).Build();

ServiceCollection services = new();
services.AddTingClient(tingConfiguration);
services.AddLogging();
ServiceProvider provider = services.BuildServiceProvider();

TingClient tingClient = provider.GetRequiredService<TingClient>();

if (!Enum.TryParse(config.TrueNas.MinSeverity, ignoreCase: true, out AlertLevel minSeverity))
    throw new Exception($"Invalid MinSeverity value: \"{config.TrueNas.MinSeverity}\"");

int forwardedCount = 0;
int errorCount = 0;

foreach (TrueNasSource source in config.TrueNas.Sources)
{
    try
    {
        TrueNasClient client = new(source);
        List<TrueNasAlert> alerts = await client.GetAlertsAsync();

        foreach (TrueNasAlert alert in alerts)
        {
            if (alert.Dismissed)
                continue;

            if (!Enum.TryParse(alert.Level, ignoreCase: true, out AlertLevel alertLevel))
                continue;

            if (alertLevel < minSeverity)
                continue;

            if (await alertStore.HasBeenForwardedAsync(source.Name, alert.Uuid))
                continue;

            string title = $"TNRelay: {source.Name} {alertLevel}";
            string body = alert.Formatted ?? alert.Text ?? string.Empty;
            await tingClient.SendAsync(title, body);

            await alertStore.MarkForwardedAsync(source.Name, alert.Uuid);
            forwardedCount++;
        }
    }
    catch (Exception ex)
    {
        errorCount++;
        Console.WriteLine($"Error processing source \"{source.Name}\": {ex.Message}");
    }
}

Console.WriteLine($"TNRelay complete: {forwardedCount} alert(s) forwarded, {errorCount} source error(s)");
return errorCount;