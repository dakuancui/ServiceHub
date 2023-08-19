using ServiceHub.API.Application.Features;

namespace ServiceHub.API.Application.Models
{
    public class Profile : IProfile
    {
        public string Name => _name;
        private string _name { get; set; }
        public Profile(string name)
        {
            _name = name;
        }

        public HealthLinkInterfaceFeature HealthLinkInterfaceFeature { get; set; }
    }
}
