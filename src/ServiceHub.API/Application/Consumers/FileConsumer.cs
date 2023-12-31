﻿using ServiceHub.Core.Application.Consumer;
using ServiceHub.Core.Application.Feature;
using ServiceHub.Core.Application.Models.FeatureConfiguration;

namespace ServiceHub.API.Application.Consumers
{
    public class FileConsumer : Consumer
	{
        private readonly ILogger<IFeature<IFeatureConfiguraiton>> _logger;

		public FileConsumer(ILogger<IFeature<IFeatureConfiguraiton>> logger)
		{
            _logger = logger;
		}


        public async Task Consume(string fullPath)
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

