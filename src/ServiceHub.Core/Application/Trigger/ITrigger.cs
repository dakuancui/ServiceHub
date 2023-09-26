namespace ServiceHub.Core.Application.Trigger
{
    public interface ITrigger
    {
        public void Start(object eventHandler, CancellationToken cancellationToken);
        public void Stop();
    }
}