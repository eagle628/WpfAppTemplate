using $ext_safeprojectname$.IViewService;
using System.Windows.Forms;

namespace $ext_safeprojectname$.ViewService
{
    /// <summary>
    /// フォルダ照会サービスファクトリインスタンス
    /// </summary>
    public class FormFolderBrowserDialogService : IFolderBrowserDialogService
    {
        /// <inheritdoc/>
        public IDirectoryDialog Create()
        {
            //内部的にはFormのFolderBrowserDialogを使用する。.NET8以降でないとWPFのOpenFolderDialogは使用できない
            var dialog = new FolderBrowserDialog();
            return new Dialog(dialog);
        }
        /// <summary>
        /// ファイル照会サービスインスタンス
        /// </summary>
        private struct Dialog : IDirectoryDialog
        {
            /// <summary>
            /// ダイアログ
            /// </summary>
            private readonly FolderBrowserDialog _dialog;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="dialog">ダイアログ</param>
            public Dialog(FolderBrowserDialog dialog) : this()
            {
                _dialog = dialog;
            }
            /// <inheritdoc/>
            public string SelectedPath => _dialog.SelectedPath;
            /// <inheritdoc/>
            public bool? ShowDialog()
            {
                var result = _dialog.ShowDialog();
                switch (result)
                {
                    case DialogResult.OK: return true;
                    case DialogResult.Cancel: return false;
                    default: return null;
                }
            }
        }
    }
}
