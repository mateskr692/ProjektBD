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
using Presentation.App.Security;
using Presentation.App.ViewModels;

namespace Presentation.App.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AdminMainWindow.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand, Role = "ADM")]
    public partial class AdminMainWindow : Window
    {
        AdminMainViewModel viewModel = new AdminMainViewModel();

        public AdminMainWindow()
        {
            this.InitializeComponent();

            this.viewModel = new AdminMainViewModel();
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
