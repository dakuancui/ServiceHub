using ServiceHub.API.Application.Models;
using ServiceHub.ServiceEngine.ServiceTypes.Singleton;

namespace ServiceHub.API.Application.Features.Services
{
    public class HealthLinkInterfaceService<P, T> : SingletonService where P : IProfile where T : IFeature 
    {
        public override Task DoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
