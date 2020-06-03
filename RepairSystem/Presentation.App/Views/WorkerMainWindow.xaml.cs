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

namespace Presentation.App.Views.Worker
{
    /// <summary>
    /// Logika interakcji dla klasy WorkerMainWindow.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand, Role = "WRK" )]
    public partial class WorkerMainWindow : Window
    {
        WorkerMainViewModel viewModel;

        public WorkerMainWindow()
        {
            this.InitializeComponent();
            this.viewModel = new WorkerMainViewModel();
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
