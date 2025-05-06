using System.Windows;
using $ext_safeprojectname$.IViewService;

namespace $ext_safeprojectname$.ViewService
{
    /// <inheritdoc cref="IQuestionMessageService"/>
    public class WpfQuestionMessageService : IQuestionMessageService
    {
        /// <inheritdoc cref="IQuestionMessageService.Show(string)"/>/>
        public IViewService.MessageBoxResult Show(string text)
        {
            var result = Application.Current.Dispatcher.Invoke(() => MessageBox.Show(Utility.SearchParentWindow(), text, "QUESTION", MessageBoxButton.YesNoCancel, MessageBoxImage.Question));
            return Utility.Convert(result);
        }
    }
}
