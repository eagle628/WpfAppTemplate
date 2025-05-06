namespace $ext_safeprojectname$.IViewService
{
    /// <summary>
    /// ファイル照会サービス
    /// </summary>
    public interface IOpenFileDialog
    {
        /// <summary>
        /// ファイル照会ダイアログ表示
        /// </summary>
        /// <returns>ダイアログ結果</returns>
        bool? ShowDialog();
        /// <summary>
        /// 選択されたファイル名称
        /// </summary>
        string FileName { get; }
    }
}
