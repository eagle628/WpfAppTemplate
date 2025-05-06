using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace $ext_safeprojectname$.WaitService.View
{
    /// <summary>
    /// WaitView.xaml の相互作用ロジック
    /// </summary>
    public partial class WaitView : Window
    {
        public WaitView()
        {
            InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Window Style
            ChangeWindowStyle();
            //Key operate
            var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }
        #region WindowStyle        
#pragma warning disable IDE1006 // 命名スタイル
        // from winuser.h
        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int WS_SYSMENU = 0x80000;
#pragma warning restore IDE1006 // 命名スタイル

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int value);

        private void ChangeWindowStyle()
        {
            //WaitDialogなので、最大化、最小化、閉じるを無効にする。
            IntPtr handle = new WindowInteropHelper(this).Handle;
            int style = GetWindowLong(handle, GWL_STYLE);
            style = style & (~WS_MAXIMIZEBOX) & (~WS_MINIMIZEBOX) & (~WS_SYSMENU);
            _ = SetWindowLong(handle, GWL_STYLE, style);
        }

        #endregion

        #region Ignore Keyed Close
#pragma warning disable IDE1006 // 命名スタイル
        // from winuser.h
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int VK_F4 = 0x73;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_CLOSE = 0xF060;
#pragma warning restore IDE1006 // 命名スタイル

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //WaitDialogなので、閉じるKey操作を無効にする。
            //Alt + F4の場合
            if ((msg == WM_SYSKEYDOWN) &&
                (wParam.ToInt32() == VK_F4))
            {
                handled = true;
                return IntPtr.Zero;
            }
            //タスクバーから閉じる場合
            if ((msg == WM_SYSCOMMAND) &&
                (wParam.ToInt32() == SC_CLOSE))
            {
                handled = true;
                return IntPtr.Zero;
            }

            return IntPtr.Zero;
        }
        #endregion
    }
}
