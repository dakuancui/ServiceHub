namespace ServiceHub.API.Application.ServiceTypes.Singleton
{
    public abstract class SingletonService : ISingletonService
    {
        public abstract Task DoAsync();

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            await DoAsync();
        }
    }
}
