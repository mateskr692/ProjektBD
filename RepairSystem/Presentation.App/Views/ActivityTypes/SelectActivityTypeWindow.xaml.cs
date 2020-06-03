using System;
using System.Collections.Generic;
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
using Buisness.Contracts.Models.Activities;
using Presentation.App.ViewModels.ActivityTypes;

namespace Presentation.App.Views.ActivityTypes
{
    /// <summary>
    /// Interaction logic for SelectActivityTypeWindow.xaml
    /// </summary>
    public partial class SelectActivityTypeWindow : Window
    {
        SelectActivityTypeViewModel viewModel;

        public SelectActivityTypeWindow( ref ActivityTypeModel activityTypeModel)
        {
            this.InitializeComponent();
            this.viewModel = new SelectActivityTypeViewModel( ref activityTypeModel );
            this.DataContext = this.viewModel;
            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
