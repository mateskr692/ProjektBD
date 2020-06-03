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
using Buisness.Contracts.Models.Activities;
using Presentation.App.ViewModels.Admin;

namespace Presentation.App.Views.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy TypeCreate.xaml
    /// </summary>
    public partial class EditActivityTypeWindow : Window
    {
        EditActivityTypeViewModel viewmodel;

        public EditActivityTypeWindow(ActivityTypeModel activityModel)
        {
            this.InitializeComponent();

            this.viewmodel = new EditActivityTypeViewModel( activityModel );
            this.DataContext = this.viewmodel;

            this.viewmodel.CloseWindow += delegate {
                this.Close();
            };
        }
    }
}
