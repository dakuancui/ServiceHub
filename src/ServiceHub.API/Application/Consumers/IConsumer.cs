using System;
namespace ServiceHub.API.Application.Consumers
{
	public interface IConsumer
	{
		public Task Consume(string fullPath);
		public void ConsumeHandler(object sender, FileSystemEventArgs e);
	}
}

