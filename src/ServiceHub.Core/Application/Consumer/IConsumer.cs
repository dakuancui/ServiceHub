namespace ServiceHub.Core.Application.Consumer
{
    public interface IConsumer
	{
		public void ConsumeHandler(object sender, FileSystemEventArgs e);
	}
}

