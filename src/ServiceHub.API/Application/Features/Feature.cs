using System;
using ServiceHub.API.Application.Consumers;
using ServiceHub.API.Application.Triggers;

namespace ServiceHub.API.Application.Features
{
	public abstract class Feature : IFeature
	{
		public Feature()
		{
		}

		public abstract IEnumerable<ITrigger> Triggers { get; }
		public abstract IEnumerable<IConsumer> Consumers { get; }
        public abstract string Name { get; }
		public abstract void Apply();
    }
}

