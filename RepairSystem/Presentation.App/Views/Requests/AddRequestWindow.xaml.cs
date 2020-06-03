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
using Presentation.App.ViewModels.Requests;

namespace Presentation.App.Views.ManagerViews
{
    /// <summary>
    /// Interaction logic for AddRequestWindow.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand )]
    public partial class AddRequestWindow : Window
    {
        AddRequestViewModel viewModel;

        public AddRequestWindow()
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewModel = new AddRequestViewModel();
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
