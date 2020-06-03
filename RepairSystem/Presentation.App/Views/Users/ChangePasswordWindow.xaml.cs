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
using Presentation.App.ViewModels;

namespace Presentation.App.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        ChangeUserPasswordViewModel viewModel;

        public ChangePasswordWindow( string userName )
        {
            this.viewModel = new ChangeUserPasswordViewModel( userName );
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
            this.InitializeComponent();
        }
    }
}
