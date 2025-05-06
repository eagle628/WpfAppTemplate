using System.Windows;

namespace $ext_safeprojectname$.IViewService
{
    /// <summary>
    /// クエスチョンメッセージサービス
    /// </summary>
    public interface IQuestionMessageService
    {
        /// <summary>
        /// メッセージボックス表示
        /// </summary>
        /// <param name="text">表示テキスト</param>
        /// <returns>ダイアログリザルト</returns>
        MessageBoxResult Show(string text);
    }
}
