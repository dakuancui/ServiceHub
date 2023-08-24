using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceHub.API.Application.Features;

namespace ServiceHub.API.Application.Consumers
{
	public class FileConsumer : Consumer
	{
        private readonly ILogger<IFeature> _logger;

		public FileConsumer(ILogger<IFeature> logger)
		{
            _logger = logger;
		}


        public override async Task Consume(string fullPath)
        {
            if (!File.Exists(fullPath))
                return;

            _logger.LogInformation($"Starting read of {fullPath}");

            using (StreamReader sr = File.OpenText(fullPath))
            {
                string? s = null;
                int counter = 1;
                while ((s = await sr.ReadLineAsync()) != null)
                {
                    _logger.LogInformation($"Reading Line {counter} of the file {fullPath}");
                    counter++;
                }
            }

            _logger.LogInformation($"Completed read of {fullPath}");
        }

        public override void ConsumeHandler(object sender, FileSystemEventArgs e)
        {
            Consume(e.FullPath);
        }
    }
}

