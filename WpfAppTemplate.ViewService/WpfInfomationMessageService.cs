using System.Windows;
using $ext_safeprojectname$.IViewService;

namespace $ext_safeprojectname$.ViewService
{
    /// <inheritdoc cref="IInfomationMessageService"/>
    public class WpfInfomationMessageService : IInfomationMessageService
    {
        /// <inheritdoc cref="IInfomationMessageService.Show(string)"/>
        public IViewService.MessageBoxResult Show(string text)
        {
            var result = Application.Current.Dispatcher.Invoke(() => MessageBox.Show(Utility.SearchParentWindow(), text, "INFOMATION", MessageBoxButton.OK, MessageBoxImage.Information));
            return Utility.Convert(result);
        }
    }
}
