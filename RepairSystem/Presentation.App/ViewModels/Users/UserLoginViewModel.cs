using System.Security.Claims;
using System.Windows.Controls;
using Buisness.Contracts.Models;
using Buisness.Core.Services;
using Common;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Security;

namespace Presentation.App.ViewModels
{
    public class UserLoginViewModel : BaseViewModel
    {
        private UserLoginModel LoginModel { get; set; }
        private UserService UserService { get; set; }

        public RelayCommand LoginCommand { get; set; }
        public UserRole? Role { get; set; }


        public string Login
        {
            get => this.LoginModel.Login;
            set { this.LoginModel.Login = value; this.OnPropertyChanged(); }
        }

        public UserLoginViewModel()
        {
            this.LoginModel = new UserLoginModel();
            this.UserService = new UserService();
            this.LoginCommand = new RelayCommand( this.Authenticate, this.CanAuthenticacte );
        }

        public void Authenticate( object parameter )
        {
            var pb = parameter as PasswordBox;
            this.LoginModel.Password = pb.Password;
            var response = this.UserService.ValidateUserCredentials( this.LoginModel );
            if(response.Status == ResultStatus.Failed)
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            AuthenticationManager.SignIn( response.Data.UserName, CodesDictionary.RoleType( response.Data.Role ) );
            this.Role = response.Data.Role;
            this.RaiseCloseEvent();
        }

        public bool CanAuthenticacte( object parameter )
        {
            return  !string.IsNullOrEmpty( this.Login );
        }
    }
}
