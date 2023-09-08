namespace ServiceHub.API.Application.Consumers
{
    public abstract class Consumer : IConsumer
	{
		public Consumer()
		{
		}
		public abstract void ConsumeHandler(object sender, FileSystemEventArgs e);
    }
}

