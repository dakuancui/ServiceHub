using Microsoft.Extensions.Logging;

namespace ServiceHub.Core.Logger
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
            if (categoryName.Contains("ServiceHub.API.Application.Features.", StringComparison.OrdinalIgnoreCase))
                return categoryName.Substring(categoryName.IndexOf("ServiceHub.API.Application.Features."));
            return "ServiceHub.Hosting";
        }
    }
}
