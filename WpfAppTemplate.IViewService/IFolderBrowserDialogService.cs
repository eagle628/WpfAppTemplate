namespace $ext_safeprojectname$.IViewService
{
    /// <summary>
    /// フォルダ照会サービスファクトリIF
    /// </summary>
    public interface IFolderBrowserDialogService
    {
        /// <summary>
        /// フォルダ照会サービス作成
        /// </summary>
        /// <returns>フォルダ照会サービス</returns>
        IDirectoryDialog Create();
    }
}
