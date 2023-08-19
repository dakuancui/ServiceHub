namespace ServiceHub.API.Application.Features
{
    public abstract class Feature : IFeature
    {
        public abstract bool IsEnabled { get; set; }
    }
}
