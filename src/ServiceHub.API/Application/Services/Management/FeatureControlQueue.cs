using System.Threading.Channels;

namespace ServiceHub.API.Application.Services.Management
{
    public sealed class FeatureControlQueue : IFeatureControlQueue
    {
        private readonly Channel<string> _commandQueue;
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public FeatureControlQueue(int capacity)
        {
            BoundedChannelOptions options = new(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _commandQueue = Channel.CreateBounded<string>(options);
        }

        public async Task<string> DequeueCommandAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            return await _commandQueue.Reader.ReadAsync(cancellationToken);
        }

        public async Task<bool> QueueCommandAsync(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentNullException(nameof(command));
            }

            await _commandQueue.Writer.WriteAsync(command);
            _signal.Release();
            QueuedCommand.Invoke(command, EventArgs.Empty);
            return true;
        }

        public event EventHandler QueuedCommand;
    }
}

