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
using Presentation.App.ViewModels.RequestActivities;

namespace Presentation.App.Views.Manager
{
    /// <summary>
    /// Logika interakcji dla klasy ActivityCreateEdit.xaml
    /// </summary>
    public partial class AddRequestActivityWindow : Window
    {
        AddRequestActivityViewModel viewModel;

        public AddRequestActivityWindow( RequestModel requestModel)
        {
            this.InitializeComponent();
            this.viewModel = new AddRequestActivityViewModel( requestModel );
            this.DataContext = this.viewModel;
            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
