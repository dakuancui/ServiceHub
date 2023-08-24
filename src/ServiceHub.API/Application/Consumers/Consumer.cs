using System;
namespace ServiceHub.API.Application.Consumers
{
	public abstract class Consumer : IConsumer
	{
		public Consumer()
		{
		}

		public abstract Task Consume(string fullPath);

		public abstract void ConsumeHandler(object sender, FileSystemEventArgs e);
    }
}

