namespace ServiceHub.API.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _rootPath;
        public FileLoggerProvider(string rootPath)
        {
            _rootPath = rootPath;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(Path.Combine(_rootPath, $"{categoryName.GetLogFileName()}.{DateTime.Today:yyyy-MM-dd}.log"));
        }

        public void Dispose()
        {
        }
    }
}
