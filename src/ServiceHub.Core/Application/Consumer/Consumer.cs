namespace ServiceHub.Core.Application.Consumer
{
    public abstract class Consumer : IConsumer
	{
		public Consumer() {}
		public abstract void ConsumeHandler(object sender, FileSystemEventArgs e);
    }
}

