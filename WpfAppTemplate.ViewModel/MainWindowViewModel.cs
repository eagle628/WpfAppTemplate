using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using R3;
using $ext_safeprojectname$.IViewService;
using $ext_safeprojectname$.ITrace;
using $ext_safeprojectname$.IWaitService;

namespace $ext_safeprojectname$.ViewModel
{
    public sealed class MainWindowViewModel : IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        private readonly ILogger<MainWindowViewModel> _logger;
        private readonly IWarningMessageService _warningMessageService;
        private readonly IOpenFileDialogService _openFileDialogService;
        private readonly IFolderBrowserDialogService _folderBrowserDialogService;
        private readonly IInMemoryLogStore _inMemoryLogStore;
        private readonly IWaitingDialogService _waitingDialogService;
        public ReactiveCommand ShowWarningCommand { get; }
        public ReactiveCommand TaskWaitCommand { get; }

        public IReadOnlyBindableReactiveProperty<ILogRecord[]> LogRecords { get; }
        public MainWindowViewModel(
            ILogger<MainWindowViewModel> logger,
            IWarningMessageService warningMessageService,
            IOpenFileDialogService openFileDialogService,
            IFolderBrowserDialogService folderBrowserDialogService,
            IInMemoryLogStore inMemoryLogStore,
            IWaitingDialogService waitingDialogService)
        {
            _logger = logger;
            _warningMessageService = warningMessageService;
            _openFileDialogService = openFileDialogService;
            _folderBrowserDialogService = folderBrowserDialogService;
            _inMemoryLogStore = inMemoryLogStore;
            _waitingDialogService = waitingDialogService;

            LogRecords = _inMemoryLogStore
                .ObservePropertyChanged(x => x.LogRecords)
                .ThrottleLast(TimeSpan.FromMilliseconds(200))
                .ToReadOnlyBindableReactiveProperty();
            LogRecords.AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
