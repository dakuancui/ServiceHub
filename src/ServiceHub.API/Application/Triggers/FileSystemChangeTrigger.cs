using System;
using System.IO;
using ServiceHub.API.Application.Features;

namespace ServiceHub.API.Application.Triggers
{
	public class FileSystemChangeTrigger : ITrigger
	{
        private readonly ILogger<IFeature> _logger;
        private readonly string _directoryPath = @"/Users/DakuanC/Dakuan.asb/spikes/ServiceHub/temp";
        private readonly string _fileFilter = "*.*";
        private readonly FileSystemWatcher _watcher;
        //private readonly IServiceProvider _serviceProvider;

        public FileSystemChangeTrigger(ILogger<IFeature> logger
            //, IServiceProvider serviceProvider
            )
		{
            _logger = logger;
            //_serviceProvider = serviceProvider;
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);
            _watcher = new FileSystemWatcher(_directoryPath, _fileFilter);
        }


        public void Start(object customerAction)
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


            _watcher.EnableRaisingEvents = true;
            _watcher.IncludeSubdirectories = true;

            _logger.LogInformation($"File Watching has started for directory {_directoryPath}");
        }

        public Task Inbound()
        {
            throw new NotImplementedException();
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

        //public void OnCreated(object sender, FileSystemEventArgs e)
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var consumerService = scope.ServiceProvider.GetRequiredService<FileConsumer>();
        //        Task.Run(() => consumerService.Consume(e.FullPath));
        //    }
        //}

        //public void ConsumerEventHandler(object sender, FileSystemEventArgs e)
        //{
        //}
    }
}

