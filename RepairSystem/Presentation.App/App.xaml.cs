using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Presentation.App.Views;
using Presentation.App.Views.Manager;

namespace Presentation.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Window _currentwindow { get; set; }

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );
            var window = new ManagerMainWindow();
            window.Show();
        }

        protected void Login()
        {
            //close the login window
            //read the returned userInfo
            //set it to application Context
            //show proper window according to the role type
        }

        protected void Logoff()
        {
            //close surrent window
            //deauthorize user - remove his credential from application context
            //show login window
        }


    }
}
