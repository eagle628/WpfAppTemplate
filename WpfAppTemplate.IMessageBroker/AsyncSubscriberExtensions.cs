using System;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// <see cref="IAsyncSubscriber{TMessage}"/>に対する拡張メソッド
    /// </summary>
    public static partial class AsyncSubscriberExtensions
    {
        /// <summary>
        /// <see cref="IAsyncSubscriber{TMessage}"/>への購読拡張メソッド
        /// </summary>
        /// <typeparam name="TMessage">メッセージタイプ</typeparam>
        /// <param name="subscriber">インスタンス</param>
        /// <param name="handler">関数</param>
        /// <returns>購読破棄リソース</returns>
        public static IDisposable Subscribe<TMessage>(this IAsyncSubscriber<TMessage> subscriber, Func<TMessage, CancellationToken, ValueTask> handler)
        {
            return subscriber.Subscribe(new AnonymousAsyncMessageHandler<TMessage>(handler));
        }
        /// <summary>
        /// 無名非同期関数ハンドラクラス
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        private sealed class AnonymousAsyncMessageHandler<TMessage> : IAsyncMessageHandler<TMessage>
        {
            /// <summary>
            /// 実行非同期関数
            /// </summary>
            private readonly Func<TMessage, CancellationToken, ValueTask> _handler;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="handler">実行する非同期関数</param>
            public AnonymousAsyncMessageHandler(Func<TMessage, CancellationToken, ValueTask> handler)
            {
                _handler = handler;
            }
            /// <inheritdoc/>
            public ValueTask HandleAsync(TMessage message, CancellationToken cancellationToken)
            {
                return _handler.Invoke(message, cancellationToken);
            }
        }
    }
}
