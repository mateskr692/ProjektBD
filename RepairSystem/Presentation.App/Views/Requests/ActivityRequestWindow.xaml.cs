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
using Buisness.Contracts.Models.Activities;
using Presentation.App.ViewModels.Requests;

namespace Presentation.App.Views.Worker
{
    /// <summary>
    /// Logika interakcji dla klasy ViewRequest.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand )]
    public partial class ActivityRequestWindow : Window
    {
        ActivityRequestViewModel viewModel;

        public ActivityRequestWindow( ActivityModel activityModel )
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewModel = new ActivityRequestViewModel( activityModel );
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
