namespace $ext_safeprojectname$.IViewService
{
    /// <summary>
    /// フォルダ照会サービス
    /// </summary>
    public interface IDirectoryDialog
    {
        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <returns>ダイアログ結果</returns>
        bool? ShowDialog();
        /// <summary>
        /// 選択ディレクトリPath
        /// </summary>
        string SelectedPath { get; }
    }
}
