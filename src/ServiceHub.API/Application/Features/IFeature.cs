using System;
using ServiceHub.API.Application.Consumers;
using ServiceHub.API.Application.Triggers;

namespace ServiceHub.API.Application.Features
{
	public interface IFeature
	{
		public IEnumerable<ITrigger> Triggers { get; }
        public IEnumerable<IConsumer> Consumers { get; }
        public string Name { get; }
        public void Apply();
    }
}

