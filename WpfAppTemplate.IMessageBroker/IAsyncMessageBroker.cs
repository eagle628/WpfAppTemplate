using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// 非同期関数のハンドリングIF
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IAsyncMessageHandler<TMessage>
    {
        /// <summary>
        /// 非同期関数の実行
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="cancellationToken">キャンセルソース</param>
        /// <returns>タスク</returns>
        ValueTask HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
    /// <summary>
    /// 非同期メッセージ発行IF
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    public interface IAsyncPublisher<TMessage>
    {
        /// <summary>
        /// 同期的にイベント発行する
        /// </summary>
        /// <param name="message">発行するイベントパラメータ</param>
        /// <param name="cancellationToken">イベントキャンセルトークン</param>
        void Publish(TMessage message, CancellationToken cancellationToken = default);
        /// <summary>
        /// 非同期的にイベント発行する
        /// </summary>
        /// <param name="message">発行するイベントパラメータ</param>
        /// <param name="cancellationToken">イベントキャンセルトークン</param>
        /// <returns>非同期タスク</returns>
        ValueTask PublishAsync(TMessage message, CancellationToken cancellationToken = default);
    }
    /// <summary>
    /// 非同期メッセージ購読IF
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    public interface IAsyncSubscriber<TMessage>
    {
        /// <summary>
        /// イベントの購読
        /// </summary>
        /// <param name="asyncHandler">購読する関数</param>
        /// <returns>購読破棄管理オブジェクト</returns>
        IDisposable Subscribe(IAsyncMessageHandler<TMessage> asyncHandler);
    }
}
