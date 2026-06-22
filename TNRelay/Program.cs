using TNRelay.Config;

Config config = Config.Load();

Console.WriteLine(config.TrueNas.Sources[0].Name);