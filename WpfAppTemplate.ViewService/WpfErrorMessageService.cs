using System.Windows;
using $ext_safeprojectname$.IViewService;

namespace $ext_safeprojectname$.ViewService
{
    /// <inheritdoc cref="IErrorMessageService"/>
    public class WpfErrorMessageService : IErrorMessageService
    {
        /// <inheritdoc cref="IErrorMessageService.Show(string)"/>
        public IViewService.MessageBoxResult Show(string text)
        {
            var result =　Application.Current.Dispatcher.Invoke(() => MessageBox.Show(Utility.SearchParentWindow(), text, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error));
            return Utility.Convert(result);
        }
    }
}
