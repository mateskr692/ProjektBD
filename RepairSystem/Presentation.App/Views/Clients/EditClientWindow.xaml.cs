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
using Buisness.Contracts.Models.Clients;
using Presentation.App.ViewModels;
using Presentation.App.ViewModels.Manager;

namespace Presentation.App.Views.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy UserEdit.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand )]
    public partial class EditClientWindow : Window
    {
        EditClientViewModel viewModel;

        public EditClientWindow( ClientModel clientModel )
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewModel = new EditClientViewModel( clientModel );
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
