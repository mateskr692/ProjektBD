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
using Buisness.Contracts.Models.Objects;
using Presentation.App.ViewModels.Admin;

namespace Presentation.App.Views.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy TypeCreate.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand )]
    public partial class EditObjectTypeWindow : Window
    {
        EditObjectTypeViewModel viewmodel;

        public EditObjectTypeWindow(ObjectTypeModel ObjectModel)
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewmodel = new EditObjectTypeViewModel( ObjectModel );
            this.DataContext = this.viewmodel;

            this.viewmodel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
