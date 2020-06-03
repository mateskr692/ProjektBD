using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels
{
    public class ChangeUserPasswordViewModel : BaseViewModel
    {
        private UserService UserService;
        private string UserName { get; set; }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ChangeUserPasswordViewModel(string userName)
        {
            this.UserName = userName;
            this.UserService = new UserService();

            this.SaveCommand = new RelayCommand( this.Save );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }

        public void Save(object parameter)
        {
            var pb = parameter as PasswordBox;
            var password = pb.Password;

            var response = this.UserService.ChangeUserPassword( this.UserName, password );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }

            MessageBox.Show( "Password changed succesfully" );
            this.RaiseCloseEvent();
        }

        public void Cancel(object parameter)
        {
            this.RaiseCloseEvent();
        }


    }
}
