using System;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IWaitService
{
    /// <summary>
    /// 実行中画面をホスティングするサービス
    /// </summary>
    public interface IWaitingDialogService
    {
        /// <summary>
        /// 実行中画面のホスト
        /// </summary>
        /// <param name="caption">ホスト画面のキャプション</param>
        /// <param name="handler">ホスト画面中に実行する非同期関数</param>
        /// <param name="ct">ホスト画面中に実行する非同期関数の<see cref="CancellationToken"/></param>
        /// <returns>タスク</returns>
        Task ExecuteAsync(string caption, Func<CancellationToken, Task> handler, CancellationToken ct);
        /// <summary>
        /// 実行中画面のホスト(引数あり)
        /// </summary>
        /// <param name="caption">ホスト画面のキャプション</param>
        /// <param name="handler">ホスト画面中に実行する非同期関数</param>
        /// <param name="message"><paramref name="handler"/>への引数</param>
        /// <param name="ct">ホスト画面中に実行する非同期関数の<see cref="CancellationToken"/></param>
        /// <returns>タスク</returns>
        Task ExecuteAsync<TMessage>(string caption, Func<TMessage, CancellationToken, Task> handler, TMessage message, CancellationToken ct);
    }
}
