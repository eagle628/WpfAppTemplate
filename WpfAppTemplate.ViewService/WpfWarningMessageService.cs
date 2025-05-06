using System.Windows;
using $ext_safeprojectname$.IViewService;

namespace $ext_safeprojectname$.ViewService
{
    /// <inheritdoc cref="IWarningMessageService"/>
    public class WpfWarningMessageService : IWarningMessageService
    {
        /// <inheritdoc cref="IWarningMessageService.Show(string)"/>/>
        public IViewService.MessageBoxResult Show(string text)
        {
            var result = Application.Current.Dispatcher.Invoke(() => MessageBox.Show(Utility.SearchParentWindow(), text, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning));
            return Utility.Convert(result);
        }
    }
}
