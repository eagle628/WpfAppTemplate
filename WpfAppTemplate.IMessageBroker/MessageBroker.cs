using System;
using System.Collections.Concurrent;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// 同期メッセージブローカクラス
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    internal sealed class MessageBroker<TMessage> : IPublisher<TMessage>, ISubscriber<TMessage>
    {
        /// <summary>
        /// 同期メッセージブローカコア実装
        /// </summary>
        private readonly MessageBrokerCore<TMessage> _core;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="core">同期メッセージブローカコア実装</param>
        public MessageBroker(MessageBrokerCore<TMessage> core)
        {
            _core = core;
        }
        /// <inheritdoc/>
        public void Publish(TMessage message)
        {
            _core.Publish(message);
        }
        /// <inheritdoc/>
        public IDisposable Subscribe(IMessageHandler<TMessage> handler)
        {
            return _core.Subscribe(handler);
        }
    }
    /// <summary>
    /// 同期メッセージブローカクラス
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    internal sealed class MessageBrokerCore<TMessage> : IPublisher<TMessage>, ISubscriber<TMessage>
    {
        /// <summary>
        ///イベントを 購読している関数の辞書
        /// </summary>
        private readonly ConcurrentDictionary<Guid, IMessageHandler<TMessage>> _handlers;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MessageBrokerCore()
        {
            _handlers = new ConcurrentDictionary<Guid, IMessageHandler<TMessage>>();
        }
        /// <inheritdoc/>
        public void Publish(TMessage message)
        {
            foreach (var handler in _handlers.Values)
            {
                handler.Handle(message);
            }
        }
        /// <inheritdoc/>
        public IDisposable Subscribe(IMessageHandler<TMessage> handler)
        {
            var key = Guid.NewGuid();
            _ = _handlers.TryAdd(key, handler);
            return new Subscription(() => _handlers.TryRemove(key, out _));
        }
    }
}
