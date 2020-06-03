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
using Buisness.Contracts.Models.Objects;
using Presentation.App.ViewModels.ClientObjects;

namespace Presentation.App.Views.ClientObjects
{
    /// <summary>
    /// Interaction logic for SelectClientObjectWindow.xaml
    /// </summary>
    public partial class SelectClientObjectWindow : Window
    {
        SelectClientObjectViewModel viewModel;

        public SelectClientObjectWindow( ClientModel client, ref ObjectModel objectModel )
        {
            this.InitializeComponent();

            this.viewModel = new SelectClientObjectViewModel( client, ref objectModel );
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
