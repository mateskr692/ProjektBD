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
using Buisness.Contracts.Models.Requests;
using Presentation.App.ViewModels.Requests;

namespace Presentation.App.Views.Requests
{
    /// <summary>
    /// Interaction logic for FinishRequestWindow.xaml
    /// </summary>
    public partial class CancelRequestWindow : Window
    {
        CancelRequestViewModel viewModel;

        public CancelRequestWindow( RequestModel requestModel )
        {
            this.InitializeComponent();
            this.viewModel = new CancelRequestViewModel( requestModel );
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
