using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;
using $ext_safeprojectname$.ITrace;
using $ext_safeprojectname$.IWaitService;
using $ext_safeprojectname$.WaitService.View;

namespace $ext_safeprojectname$.WaitService.ViewModel
{
    /// <summary>
    /// 実行中画面をホスティングするクラス
    /// </summary>
    /// <remarks>WPF用</remarks>
    public sealed class WaitingDialogService : IWaitingDialogService
    {
        private readonly ILogger<WaitingDialogService> _logger;
        private readonly IInMemoryLogStore _logStore;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="logStore">インメモリログインスタンス</param>
        public WaitingDialogService(ILogger<WaitingDialogService> logger, IInMemoryLogStore logStore)
        {
            _logger = logger;
            _logStore = logStore;
        }
        /// <inheritdoc/>
        public async Task ExecuteAsync(string caption, Func<CancellationToken, Task> handler, CancellationToken ct)
        {
            _logger.LogDebug("Boot background task");
            var task = handler.Invoke(ct).ConfigureAwait(false);
            var activeWindow = Utility.SearchParentWindow();
            _logger.LogDebug($"Showdialog {nameof(WaitView)}");
            using (var vm = new WaitViewModel(_logStore, caption))
            {
                var w = new WaitView()
                {
                    Owner = activeWindow,
                    DataContext = vm,
                };
                _ = Application.Current.Dispatcher.InvokeAsync(() => w.ShowDialog());
                _logger.LogDebug($"Start waiting dialog");
                try
                {
                    await task;
                }
                catch (Exception ex)
                {
                    _logger.LogDebug($"Fail background task", ex);
                    throw;
                }
                finally
                {
                    _logger.LogDebug("Complete task");
                    await Application.Current.Dispatcher.InvokeAsync(() => w.Close());
                }
            }
            _logger.LogDebug($"Close {nameof(WaitView)}");
        }
        /// <inheritdoc/>
        public async Task ExecuteAsync<TMessage>(string caption, Func<TMessage, CancellationToken, Task> handler, TMessage message, CancellationToken ct)
        {
            await ExecuteAsync(caption, async token => await handler.Invoke(message, token).ConfigureAwait(false), ct);
        }
    }
}
