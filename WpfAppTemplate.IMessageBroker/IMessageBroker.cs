using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// 同期関数のハンドリングIF
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IMessageHandler<TMessage>
    {
        /// <summary>
        /// 同期関数の実行
        /// </summary>
        /// <param name="message">メッセージ</param>
        void Handle(TMessage message);
    }
    /// <summary>
    /// 同期メッセージ発行IF
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    public interface IPublisher<TMessage>
    {
        /// <summary>
        /// 同期的にイベント発行する
        /// </summary>
        /// <param name="message">発行するイベントパラメータ</param>
        void Publish(TMessage message);
    }
    /// <summary>
    /// 同期メッセージ購読IF
    /// </summary>
    /// <typeparam name="TMessage">メッセージタイプ</typeparam>
    public interface ISubscriber<TMessage>
    {
        /// <summary>
        /// イベントの購読
        /// </summary>
        /// <param name="handler">購読する関数</param>
        /// <returns>購読破棄管理オブジェクト</returns>
        IDisposable Subscribe(IMessageHandler<TMessage> handler);
    }
}
