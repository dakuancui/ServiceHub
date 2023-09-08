using ServiceHub.API.Application.Features;
using ServiceHub.API.Application.Models.FeatureConfigurations;

namespace ServiceHub.API.Application.Triggers
{
    public class FileSystemChangeTrigger : ITrigger, IDisposable
	{
        private readonly ILogger<IFeature<IFeatureConfiguraiton>> _logger;
        private readonly string _directoryPath;
        private readonly string _fileFilter;
        private readonly FileSystemWatcher _watcher;
        private bool _disposedValue;

        public FileSystemChangeTrigger(ILogger<IFeature<IFeatureConfiguraiton>> logger, string directoryPath, string fileFilter)
		{
            _logger = logger;
            _directoryPath = directoryPath;
            _fileFilter = fileFilter;
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);
            _watcher = new FileSystemWatcher(_directoryPath, _fileFilter);
        }


        public void Start(object customerAction, CancellationToken cancellationToken)
        {
            _watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            _watcher.Changed += OnChanged;
            _watcher.Created += (FileSystemEventHandler) customerAction;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.Error += OnError;

            if (!cancellationToken.IsCancellationRequested)
            {
                _watcher.EnableRaisingEvents = true;
            }

            _watcher.IncludeSubdirectories = true;
            _logger.LogInformation($"File Watching has started for directory {_directoryPath}");
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            _logger.LogInformation($"File error event {e.GetException().Message}");
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            _logger.LogInformation($"File rename event for file {e.FullPath}");
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"File deleted event for file {e.FullPath}");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FileSystemChangeTrigger() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _watcher.EnableRaisingEvents = false;
                }
                _disposedValue = true;
            }
        }

        public void Stop()
        {
            Dispose();
        }
    }
}

