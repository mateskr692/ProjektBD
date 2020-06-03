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
using Buisness.Contracts.Models.Objects;
using Presentation.App.ViewModels.Admin;
using Presentation.App.ViewModels.ClientObjects;

namespace Presentation.App.Views
{
    /// <summary>
    /// Logika interakcji dla klasy TypeCreate.xaml
    /// </summary>
    public partial class EditClientObjectsWindow : Window
    {
        EditClientObjectViewModel viewmodel;

        public EditClientObjectsWindow(ObjectModel objectModel)
        {
            this.InitializeComponent();

            this.viewmodel = new EditClientObjectViewModel( objectModel );
            this.DataContext = this.viewmodel;

            this.viewmodel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
