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
using Buisness.Contracts.Models;
using Presentation.App.ViewModels.Users;

namespace Presentation.App.Views.Manager
{
    /// <summary>
    /// Logika interakcji dla klasy WorkerBrowse.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand )]
    public partial class SelectWorkerWindow : Window
    {
        SelectWorkerViewModel viewModel;

        public SelectWorkerWindow( ref UserModel workerModel )
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewModel = new SelectWorkerViewModel( ref workerModel );
            this.DataContext = this.viewModel;
            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
