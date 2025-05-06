using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows.Threading;
using $safeprojectname$.View;
using $safeprojectname$.ViewModel;
using NLog.Extensions.Logging;
using R3;
using $safeprojectname$.IViewService;
using $safeprojectname$.ViewService;
using $safeprojectname$.Trace;
using $safeprojectname$.IWaitService;
using $safeprojectname$.WaitService.ViewModel;

namespace $safeprojectname$
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly ILogger<App> _logger;
        private readonly IErrorMessageService _errorMessageService;
        /// <summary>
        /// アプリケーションコンストラクタ
        /// </summary>
        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(ConfigureServiceCore)
                .ConfigureLogging(ConfigureLoggingCore)
                .ConfigureAppConfiguration(ConfigureAppConfigurationCore)
                .Build();

            //アプリケーションについて、Logger等を設定
            _logger = _host.Services.GetRequiredService<ILogger<App>>();
            _errorMessageService = _host.Services.GetRequiredService<IErrorMessageService>();
        }
        private void ConfigureServiceCore(HostBuilderContext context, IServiceCollection services)
        {
            _ = services
                    //メインウィンドウとそのViewModelをDIに登録
                    .AddSingleton(provider => new MainWindow() { DataContext = provider.GetRequiredService<MainWindowViewModel>() })
                    .AddSingleton<MainWindowViewModel>()
                    //MessageBoxService
                    .AddSingleton<IWarningMessageService, WpfWarningMessageService>()
                    .AddSingleton<IInfomationMessageService, WpfInfomationMessageService>()
                    .AddSingleton<IErrorMessageService, WpfErrorMessageService>()
                    .AddSingleton<IQuestionMessageService, WpfQuestionMessageService>()
                    //File, Folder ref Service
                    .AddSingleton<IOpenFileDialogService, WpfOpenFileDialogService>()
                    .AddSingleton<IFolderBrowserDialogService, FormFolderBrowserDialogService>()
                    //WaitService
                    .AddSingleton<IWaitingDialogService, WaitingDialogService>();
        }
        private void ConfigureLoggingCore(HostBuilderContext context, ILoggingBuilder logging)
        {
            _ = logging
                    .ClearProviders()
                    .SetMinimumLevel(LogLevel.Information)
                    .AddNLog(context.Configuration)
                    .AddInMemoryLogger();
#if DEBUG
            //DEBUG中は、Debug出力も有効にする
            _ = logging.AddDebug();
            //DEBUG中は、Debugレベルから出力する
            _ = logging.SetMinimumLevel(LogLevel.Debug);
#endif
        }
        private void ConfigureAppConfigurationCore(HostBuilderContext context, IConfigurationBuilder configuration)
        {
            _ = configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsetting.json", optional: true);
        }
        /// <summary>
        /// アプリケーションスタート時の処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected async override void OnStartup(StartupEventArgs e)
        {
            ObservableSystem.RegisterUnhandledExceptionHandler(ex => _logger.LogTrace(ex, "Unhandled Exception"));
            await _host.StartAsync();
            _host.Services.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }
        /// <summary>
        /// アプリケーション終了時の処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected async override void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.LogCritical(e.Exception.ToString());
            _errorMessageService.Show("アプリケーションでエラーが発生しました。\n" + e.Exception.Message);
            //Timeoutなし待機とする。
            _host.StopAsync().Wait();
            _host.Dispose();
        }
    }
}
