namespace ServiceHub.API.Application.Features
{
    public interface IFeature
    {
        public bool IsEnabled { get; set; }
    }
}
