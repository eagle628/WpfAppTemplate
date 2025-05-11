using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// 非同期メッセージブローカクラス
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    internal class AsyncMessageBroker<TMessage> : IAsyncPublisher<TMessage>, IAsyncSubscriber<TMessage>
    {
        /// <summary>
        /// 非同期メッセージブローカコア
        /// </summary>
        private readonly AsyncMessageBrokerCore<TMessage> _core;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="core">非同期メッセージブローカ</param>
        public AsyncMessageBroker(AsyncMessageBrokerCore<TMessage> core)
        {
            _core = core;
        }
        /// <inheritdoc/>
        public void Publish(TMessage message, CancellationToken cancellationToken = default)
        {
            _core.Publish(message, cancellationToken);
        }
        /// <inheritdoc/>
        public ValueTask PublishAsync(TMessage message, CancellationToken cancellationToken = default)
        {
            return _core.PublishAsync(message, cancellationToken);
        }
        /// <inheritdoc/>
        public IDisposable Subscribe(IAsyncMessageHandler<TMessage> asyncHandler)
        {
            return _core.Subscribe(asyncHandler);
        }
    }
    /// <summary>
    /// 非同期メッセージブローカコア実装
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    /// <remarks>DIInjectionを前提にするため、あえて作成</remarks>
    internal class AsyncMessageBrokerCore<TMessage> : IAsyncPublisher<TMessage>, IAsyncSubscriber<TMessage>
    {
        /// <summary>
        /// イベント購読している関数の辞書
        /// </summary>
        private readonly ConcurrentDictionary<Guid, IAsyncMessageHandler<TMessage>> _handlers;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AsyncMessageBrokerCore()
        {
            _handlers = new ConcurrentDictionary<Guid, IAsyncMessageHandler<TMessage>>();
        }
        /// <inheritdoc/>
        public void Publish(TMessage message, CancellationToken cancellationToken)
        {
            foreach (var handler in _handlers.Values)
            {
                handler.HandleAsync(message, cancellationToken).Forget();
            }
        }
        /// <inheritdoc/>
        public async ValueTask PublishAsync(TMessage message, CancellationToken cancellationToken)
        {
            foreach (var handler in _handlers.Values)
            {
                await handler.HandleAsync(message, cancellationToken);
            }
        }
        /// <inheritdoc/>
        public IDisposable Subscribe(IAsyncMessageHandler<TMessage> asyncHandler)
        {
            var key = Guid.NewGuid();
            _ = _handlers.TryAdd(key, asyncHandler);
            return new Subscription(() => _handlers.TryRemove(key, out _));
        }
    }
    /// <summary>
    /// Task関連拡張クラス
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// タスク実行の監視をやめる拡張メソッド
        /// </summary>
        /// <param name="task">タスク</param>
        public static async void Forget(this ValueTask task)
        {
            await task;
        }
    }
}
