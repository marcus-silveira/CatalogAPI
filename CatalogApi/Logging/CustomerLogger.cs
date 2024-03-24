namespace CatalogApi.Logging;

public class CustomerLogger : ILogger
{
    private readonly CustomLoggerProviderConfiguration _loggerConfig;
    private readonly string _loggerName;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration loggerConfig)
    {
        _loggerName = name;
        _loggerConfig = loggerConfig;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        SaveLogFile(message);
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public void SaveLogFile(string message)
    {
        const string path = @"C:\dev\CatalogApi\CatalogApi\Logging\log.txt";
        using var streamWriter = new StreamWriter(path, true);
        try
        {
            streamWriter.WriteLine(message);
            streamWriter.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}