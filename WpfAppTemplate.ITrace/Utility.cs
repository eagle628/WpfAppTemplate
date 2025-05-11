namespace $ext_safeprojectname$.ITrace
{
    internal static partial class Utility
    {
        public static LogLevel ConvertLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return LogLevel.Trace;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return LogLevel.Debug;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return LogLevel.Information;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return LogLevel.Warning;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return LogLevel.Error;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return LogLevel.Critical;
                case Microsoft.Extensions.Logging.LogLevel.None:
                default:
                    return LogLevel.None;
            }
        }
    }
}
