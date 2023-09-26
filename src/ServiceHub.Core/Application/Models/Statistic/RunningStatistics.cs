namespace ServiceHub.Core.Application.Models.Statistic
{
    public static class Statistics
	{
		public static Dictionary<string, ProfileStatistics> RunningStatistics { get; set; } = new Dictionary<string, ProfileStatistics>();
	}
}

