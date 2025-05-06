using Microsoft.Win32;
using $ext_safeprojectname$.IViewService;

namespace $ext_safeprojectname$.ViewService
{
    /// <summary>
    /// ファイル照会サービスファクトリインスタンス
    /// </summary>
    public class WpfOpenFileDialogService : IOpenFileDialogService
    {
        /// <inheritdoc/>
        public IOpenFileDialog Create(string fileName = null, string defaultExt = null, string filter = null)
        {
            var dialog = new OpenFileDialog()
            {
                FileName = fileName,
                DefaultExt = defaultExt,
                Filter = filter,
            };
            return new Dialog(dialog);
        }
        /// <summary>
        /// ファイル照会サービスインスタンス
        /// </summary>
        private readonly struct Dialog : IOpenFileDialog
        {
            /// <summary>
            /// ダイアログインスタンス
            /// </summary>
            private readonly OpenFileDialog _dialog;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="dialog"></param>
            public Dialog(OpenFileDialog dialog) : this()
            {
                _dialog = dialog;
            }
            /// <inheritdoc/>
            public string FileName => _dialog.FileName;
            /// <inheritdoc/>
            public bool? ShowDialog()
            {
                return _dialog.ShowDialog();
            }
        }
    }
}
