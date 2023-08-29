using System;
namespace ServiceHub.API.Application.Services.MonitoringService
{
	public interface IMonitoringService
	{
		public ValueTask AddProfileAndRunFeatures();
    }
}

