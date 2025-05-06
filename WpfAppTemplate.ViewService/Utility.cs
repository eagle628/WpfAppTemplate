using System.Linq;
using System.Windows;

namespace $ext_safeprojectname$.ViewService
{
    /// <summary>
    /// メッセージサービスユーティリティ
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// コールしたアプリケーションの親Windowクラスを取得する
        /// </summary>
        /// <returns>親Windowクラス</returns>
        /// <remarks>WPFアプリケーション用</remarks>
        public static Window SearchParentWindow()
        {
            //アプリがActiveの時は、アクティブになっている最上位に出す。
            var parentWindow = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive);
            if (parentWindow is null)
            {
                //アプリがActiveになってないときは、しょうがないので、
                //MainWindoowをActive化して、それを親とする
                _ = Application.Current.MainWindow.Activate();
                parentWindow = Application.Current.MainWindow;
            }
            return parentWindow;
        }
        /// <summary>
        /// MessageBoxResultをIViewService.MessageBoxResultに変換する 
        /// </summary>
        /// <param name="result">GUIのダイアログ結果</param>
        /// <returns>独自のダイアログ結果</returns>
        public static IViewService.MessageBoxResult Convert(MessageBoxResult result)
        {
            // MessageBoxResultをIViewService.MessageBoxResultに変換する
            switch (result)
            {
                case MessageBoxResult.OK: return IViewService.MessageBoxResult.OK;
                case MessageBoxResult.Cancel: return IViewService.MessageBoxResult.Cancel;
                case MessageBoxResult.Yes: return IViewService.MessageBoxResult.Yes;
                case MessageBoxResult.No: return IViewService.MessageBoxResult.No;
                case MessageBoxResult.None:
                default:
                    return IViewService.MessageBoxResult.None;
            }
        }
    }
}
