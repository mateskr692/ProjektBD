using System.Windows;
using Presentation.App.Views;

namespace Presentation.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Window _currentwindow { get; set; }

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );
            var window = new LoginWindow();
            window.Show();
        }



    }
}
