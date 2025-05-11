using Microsoft.Extensions.DependencyInjection;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// DI登録のための拡張メソッド定義
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="IAsyncSubscriber{TMessage}"/>および<see cref="IAsyncSubscriber{TMessage}"/>をDIに登録するための拡張メソッド
        /// </summary>
        /// <param name="services">DIマネージャー</param>
        /// <returns>DIマネージャー</returns>
        public static IServiceCollection UseAsyncMessageBroker(this IServiceCollection services)
        {
            return services
                        .AddSingleton(typeof(AsyncMessageBrokerCore<>))
                        .AddSingleton(typeof(IAsyncSubscriber<>), typeof(AsyncMessageBroker<>))
                        .AddSingleton(typeof(IAsyncPublisher<>), typeof(AsyncMessageBroker<>));
        }
        /// <summary>
        /// <see cref="ISubscriber{TMessage}"/>および<see cref="ISubscriber{TMessage}"/>をDIに登録するための拡張メソッド
        /// </summary>
        /// <param name="services">DIマネージャー</param>
        /// <returns>DIマネージャー</returns>
        public static IServiceCollection UseMessageBroker(this IServiceCollection services)
        {
            return services
                           .AddSingleton(typeof(MessageBrokerCore<>))
                           .AddSingleton(typeof(ISubscriber<>), typeof(MessageBroker<>))
                           .AddSingleton(typeof(IPublisher<>), typeof(MessageBroker<>));
        }
    }
}
