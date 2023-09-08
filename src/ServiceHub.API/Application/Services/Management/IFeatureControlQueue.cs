namespace ServiceHub.API.Application.Services.Management
{
    public interface IFeatureControlQueue
    {
        Task<bool> QueueCommandAsync(string command);

        Task<string> DequeueCommandAsync(CancellationToken cancellationToken);
        public event EventHandler QueuedCommand;
    }
}

