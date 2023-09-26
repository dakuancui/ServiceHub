namespace ServiceHub.Core.Application.Models.Statistic
{
    public record ProfileStatistics
	{
		public string Id { get; set; }
		public string ProfileName { get; set; }
		public string Status { get; set; }
		public IList<FeatureStatistics> FeaturesStatistics { get; set; }
	}
}

