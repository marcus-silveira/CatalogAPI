using System.Collections.Concurrent;

namespace CatalogApi.Logging;

public class CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfig) : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, CustomerLogger> _loggers = new();

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
    }
    
    public void Dispose()
    {
        _loggers.Clear();
    }
}