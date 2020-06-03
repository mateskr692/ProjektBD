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
using Buisness.Contracts.Models.Clients;
using Presentation.App.ViewModels.Clients;

namespace Presentation.App.Views.Clients
{
    /// <summary>
    /// Interaction logic for SelectClientWindow.xaml
    /// </summary>
    public partial class SelectClientWindow : Window
    {
        SelectClientViewModel viewModel;

        public SelectClientWindow( ref ClientModel clientModel )
        {
            this.InitializeComponent();

            this.viewModel = new SelectClientViewModel(ref clientModel);
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
