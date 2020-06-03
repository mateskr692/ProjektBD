using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
using Presentation.App.ViewModels;

namespace Presentation.App.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ManagerMainWindow.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand, Role = "MAN" )]
    public partial class ManagerMainWindow : Window
    {
        ManagerMainViewModel viewModel;

        public ManagerMainWindow()
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewModel = new ManagerMainViewModel();
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
            this.viewModel.SignOut += delegate {
                var window = new LoginWindow();
                window.Show();
                this.Close();
            };
        }
    }
}
