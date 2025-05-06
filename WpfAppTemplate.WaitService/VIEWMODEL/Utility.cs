using System.Linq;
using System.Windows;

namespace $ext_safeprojectname$.WaitService.ViewModel
{
    // <summary>
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
    }
}
