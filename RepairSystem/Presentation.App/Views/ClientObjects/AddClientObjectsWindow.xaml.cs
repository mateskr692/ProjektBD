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
using Presentation.App.ViewModels.Admin;
using Presentation.App.ViewModels.ClientObjects;

namespace Presentation.App.Views.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy TypeCreate.xaml
    /// </summary>
    public partial class AddClientObjectsWindow : Window
    {
        AddClientObjectViewModel viewmodel;

        public AddClientObjectsWindow( ClientModel clientModel)
        {
            this.InitializeComponent();

            this.viewmodel = new AddClientObjectViewModel( clientModel );
            this.DataContext = this.viewmodel;

            this.viewmodel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
