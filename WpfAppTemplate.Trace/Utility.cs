namespace $ext_safeprojectname$.Trace
{
    internal static partial class Utility
    {
        public static ITrace.LogLevel ConvertLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace: 
                    return ITrace.LogLevel.Trace;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return ITrace.LogLevel.Debug;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return ITrace.LogLevel.Information;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return ITrace.LogLevel.Warning;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return ITrace.LogLevel.Error;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return ITrace.LogLevel.Critical;
                case Microsoft.Extensions.Logging.LogLevel.None:
                default:
                    return ITrace.LogLevel.None;
            }
        }
    }
}
