namespace ServiceHub.API.Logger
{
    public class FileLogger : ILogger
    {
        private string _filePath;
        private static object _lock = new object();

        public FileLogger(string path)
        {
            _filePath = path;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(_filePath, $"{DateTime.UtcNow.ToString("yyyy-MM-dd:HH:mm:ss.ffff")} {logLevel}:{formatter(state, exception)} {Environment.NewLine}");
                }
            }
        }
    }
}
