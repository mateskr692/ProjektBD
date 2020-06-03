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
using Buisness.Contracts.Models.Requests;
using Presentation.App.ViewModels.RequestActivities;

namespace Presentation.App.Views.Manager
{
    /// <summary>
    /// Logika interakcji dla klasy RequestActivities.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand )]
    public partial class RequestActivitiesWindow : Window
    {
        RequestActivitiesViewModel viewModel;

        public RequestActivitiesWindow( RequestModel requestModel)
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewModel = new RequestActivitiesViewModel( requestModel );
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
