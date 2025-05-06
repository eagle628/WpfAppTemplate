using System.Windows;

namespace $ext_safeprojectname$.IViewService
{
    /// <summary>
    /// インフォメーションアイコンメッセージサービス
    /// </summary>
    public interface IInfomationMessageService
    {
        /// <summary>
        /// メッセージボックスの表示
        /// </summary>
        /// <param name="text">表示テキスト</param>
        /// <returns>ダイアログリザルト</returns>
        MessageBoxResult Show(string text);
    }
}
