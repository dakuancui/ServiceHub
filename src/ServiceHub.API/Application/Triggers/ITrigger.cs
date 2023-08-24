namespace ServiceHub.API.Application.Triggers
{
    public interface ITrigger
    {
        public void Start(object eventHandler);
    }
}