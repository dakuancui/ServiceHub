using System;
using ServiceHub.API.Application.Consumers;
using ServiceHub.API.Application.Models;
using ServiceHub.API.Application.Triggers;

namespace ServiceHub.API.Application.Features
{
	//public class GeneralFeature : Feature
	//{
 //       private readonly IEnumerable<Profile> _profiles;

	//	public GeneralFeature(IEnumerable<Profile> profiles)
	//	{
 //           _profiles = profiles;
	//	}

 //       public override IEnumerable<ITrigger> Triggers => throw new NotImplementedException();

 //       public override IEnumerable<IConsumer> Consumers => throw new NotImplementedException();

 //       public override string Name => throw new NotImplementedException();

 //       public override void Apply()
 //       {
 //           //throw new NotImplementedException();
 //           //_triggers.First().Start((FileSystemEventHandler)_consumers.First().ConsumeHandler);
 //           foreach (var profile in _profiles)
 //           {
 //               foreach (var featureConfig in profile.FeatureConfiguraitons)
 //               {
 //                   if (featureConfig.IsEnabled)
 //                   {
 //                       var type = Type.GetType(featureConfig.FeatrueName);
 //                       if (type != null)
 //                       {
 //                           var instance = Activator.CreateInstance(type);
 //                           if (instance != null)
 //                           {
 //                               var feature = (IFeature)instance;
 //                               feature.Apply();
 //                           }
 //                       }
 //                   }
 //               }
 //           }

 //       }
 //   }
}

