using ServiceHub.API.Application.Triggers;

namespace ServiceHub.API.Application.Models.FeatureConfigurations
{
    //public abstract class Feature<T, O> : IFeature where T : ITrigger where O : IOutput
    //{
    //    private readonly List<T> _triggers;
    //    private readonly List<O> _outputs;

    //    public abstract bool IsEnabled { get; set; }

    //    public void AddTrigger<T>()
    //    {

    //    }
    //}

    public abstract class FeatureConfiguraiton : IFeatureConfiguraiton
    {
        public abstract bool IsEnabled { get; set; }
    }
}
