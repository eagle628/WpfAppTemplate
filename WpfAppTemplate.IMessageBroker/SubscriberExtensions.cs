using System;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// <see cref="ISubscriber{TMessage}"/>に対する拡張メソッド
    /// </summary>
    public static partial class SubscriberExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="subscriber"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IDisposable Subscribe<TMessage>(this ISubscriber<TMessage> subscriber, Action<TMessage> handler)
        {
            var handle = new AnonymousMessageHandler<TMessage>(handler);
            return subscriber.Subscribe(handle);
        }
        /// <summary>
        /// 無名同期関数ハンドラクラス
        /// </summary>
        /// <typeparam name="TMessage">メッセージタイプ</typeparam>
        private sealed class AnonymousMessageHandler<TMessage> : IMessageHandler<TMessage>
        {
            /// <summary>
            /// 実行する同期関数
            /// </summary>
            private readonly Action<TMessage> _handler;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="handler">実行する同期関数</param>
            public AnonymousMessageHandler(Action<TMessage> handler)
            {
                _handler = handler;
            }
            /// <inheritdoc/>
            public void Handle(TMessage message)
            {
                _handler.Invoke(message);
            }
        }
    }
}
