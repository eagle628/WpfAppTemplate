using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using $ext_safeprojectname$.ITrace;

namespace $ext_safeprojectname$.Trace
{
    /// <summary>
    /// <see cref="IInMemoryLogStore"/>をDIに登録するための拡張メソッド
    /// </summary>
    public static class InMemoryLoggerExtensions
    {
        /// <summary>
        /// インメモリなログストアおよびロガーを追加する
        /// </summary>
        /// <param name="builder">ロギングビルダ</param>
        /// <returns>ロギングビルダ</returns>
        public static ILoggingBuilder AddInMemoryLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();
            builder.Services
                .ConfigureInMemoryLogStore()
                .AddSingleton<InMemoryLogStore>()
                .AddSingleton<IInMemoryLogStore>(provider => provider.GetRequiredService<InMemoryLogStore>())
                .AddSingleton<IInternalInMemoryLogStore>(provider => provider.GetRequiredService<InMemoryLogStore>())
                .TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, InMemoryLoggerProvider>(x =>
                {
                    var store = x.GetRequiredService<IInternalInMemoryLogStore>();
                    return new InMemoryLoggerProvider(store);
                }));
            LoggerProviderOptions.RegisterProviderOptions<InMemoryLogStoreConfig, InMemoryLoggerProvider>(builder.Services);
            return builder;
        }
        /// <summary>
        /// インメモリログストアの設定情報の読み込み
        /// </summary>
        /// <param name="services">DIマネージャー</param>
        /// <returns>DIマネージャー</returns>
        private static IServiceCollection ConfigureInMemoryLogStore(this IServiceCollection services)
        {
            return services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>().GetSection("InMemoryLogStore").Get<InMemoryLogStoreConfig>();
                return config is null ? new InMemoryLogStoreConfig() : config;
            });
        }
    }
}
