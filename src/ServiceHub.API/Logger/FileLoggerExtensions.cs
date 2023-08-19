namespace ServiceHub.API.Logger
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddLogRootPath(this ILoggerFactory factory, string fileRootPath)
        {
            factory.AddProvider(new FileLoggerProvider(fileRootPath));
            return factory;
        }

        public static string GetLogFileName(this string categoryName)
        {
            if (categoryName.Contains("features", StringComparison.OrdinalIgnoreCase))
                return categoryName.Substring(categoryName.IndexOf("Features.Services."));
            return "ServiceHub.Hosting";
        }
    }
}
