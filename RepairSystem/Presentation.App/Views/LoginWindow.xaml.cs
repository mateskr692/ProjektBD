using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common.Enums;
using Presentation.App.Security;
using Presentation.App.ViewModels;
using Presentation.App.Views.Admin;
using Presentation.App.Views.Manager;
using Presentation.App.Views.Worker;

namespace Presentation.App.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserLoginViewModel ViewModel { get; set; }
        public LoginWindow()
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.ViewModel = new UserLoginViewModel();
            this.ViewModel.CloseWindow += delegate { this.OnClose( null, null ); this.Close(); };
            this.DataContext = this.ViewModel;
        }

        public void OnClose( object sender, CancelEventArgs e )
        {
            switch( this.ViewModel.Role)
            {
                case UserRole.Administrator:
                    {
                        var window = new AdminMainWindow();
                        window.Show();
                        break;
                    }
                case UserRole.Manager:
                    {
                        var window = new ManagerMainWindow();
                        window.Show();
                        break;
                    }
                case UserRole.Worker:
                    {
                        var window = new WorkerMainWindow();
                        window.Show();
                        break;
                    }
                default: break;

            }
        }
    }
}
