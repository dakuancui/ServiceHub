using System.Threading.Channels;

namespace ServiceHub.ServiceEngine.ServiceTypes.QueueService
{
    public sealed class QueueTaskService : IQueueTaskService
    {
        private readonly Channel<Func<CancellationToken, ValueTask>> _queue;
        private SemaphoreSlim _signal = new SemaphoreSlim(0);
        public QueueTaskService(int capacity)
        {
            BoundedChannelOptions options = new(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
        }

        public async ValueTask QueueBackgroundWorkItemAsync(
            Func<CancellationToken, ValueTask> workItem)
        {
            if (workItem is null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await _queue.Writer.WriteAsync(workItem);
            _signal.Release();
        }

        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);

            Func<CancellationToken, ValueTask>? workItem =
                await _queue.Reader.ReadAsync(cancellationToken);

            return workItem;
        }
    }
}

