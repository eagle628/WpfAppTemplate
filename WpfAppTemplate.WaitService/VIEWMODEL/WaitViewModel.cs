using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using $ext_safeprojectname$.ITrace;

namespace $ext_safeprojectname$.WaitService.ViewModel
{
    /// <summary>
    /// WaitダイアログViewModel
    /// </summary>
    internal sealed class WaitViewModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// プロパティ変更イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// プロパティ変更イベントの発行
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// インメモリログストア
        /// </summary>
        private readonly IInMemoryLogStore _logStore;
        /// <summary>
        /// キャプション
        /// </summary>
        public string Caption { get; }
        /// <summary>
        /// 詳細情報
        /// </summary>
        public string Detail { get; private set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logStore">インメモリログストア</param>
        /// <param name="caption">キャプション</param>
        public WaitViewModel(IInMemoryLogStore logStore, string caption)
        {
            _logStore = logStore;

            Caption = caption;

            var record = _logStore.HeadLogRecord;
            Detail = $"LogLevel:{record.LogLevel}\nCategoryName:{record.CategoryName}\nMessage:{record.Message}\nat {record.DateTime}";

            _logStore.PropertyChanged += DetailUpdate;
        }
        /// <summary>
        /// 詳細情報更新イベントメソッド
        /// </summary>
        /// <param name="_">送信元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void DetailUpdate(object _, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IInMemoryLogStore.HeadLogRecord))
            {
                var record = _logStore.HeadLogRecord;
                Detail = $"LogLevel:{record.LogLevel}\nCategoryName:{record.CategoryName}\nMessage:{record.Message}\nat {record.DateTime}";
                OnPropertyChanged(nameof(Detail));
            }
        }
        /// <summary>
        /// <see cref="IDisposable"/>実装
        /// </summary>
        public void Dispose()
        {
            _logStore.PropertyChanged -= DetailUpdate;
        }
    }
}
