namespace $ext_safeprojectname$.IViewService
{
    /// <summary>
    /// ファイル照会サービスファクトリ
    /// </summary>
    public interface IOpenFileDialogService
    {
        /// <summary>
        /// ファイル照会サービス生成
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <param name="DefaultExt">デフォルト拡張子</param>
        /// <param name="Filter">フィルタ</param>
        /// <returns>ファイル照会サービス</returns>
        IOpenFileDialog Create(string FileName = null, string DefaultExt = null, string Filter = null);
    }
}
