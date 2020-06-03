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
using Buisness.Contracts.Models.Objects;
using Presentation.App.ViewModels.ObjectTypes;

namespace Presentation.App.Views.ObjectTypes
{
    /// <summary>
    /// Interaction logic for SelectObjectTypeWindow.xaml
    /// </summary>
    [PrincipalPermission( SecurityAction.Demand )]
    public partial class SelectObjectTypeWindow : Window
    {
        private SelectObjectTypeViewModel viewModel;

        public SelectObjectTypeWindow( ref ObjectTypeModel typeModel )
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.viewModel = new SelectObjectTypeViewModel( ref typeModel );
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
