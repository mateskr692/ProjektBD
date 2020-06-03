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
using Buisness.Contracts.Models;
using Presentation.App.ViewModels;

namespace Presentation.App.Views.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy UserEdit.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        AddUserViewModel viewModel;

        public AddUserWindow()
        {
            this.InitializeComponent();

            this.viewModel = new AddUserViewModel();
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
