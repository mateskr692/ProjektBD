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
using Presentation.App.ViewModels.Admin;

namespace Presentation.App.Views.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy TypeCreate.xaml
    /// </summary>
    public partial class AddActivityTypeWindow : Window
    {
        AddActivityTypeViewModel viewmodel;

        public AddActivityTypeWindow()
        {
            this.InitializeComponent();

            this.viewmodel = new AddActivityTypeViewModel();
            this.DataContext = this.viewmodel;

            this.viewmodel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
