using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        private UserModel userModel;
        private UserService userService;

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public List<UserRole> Roles { get; set; }
        public UserRole SelectedRole
        {
            get => this.userModel.Role;
            set => this.userModel.Role = value;
        }

        public List<AccountStatus> AccountStatuses { get; set; }
        public AccountStatus SelectedAccountStatus
        {
            get => this.userModel.Active;
            set => this.userModel.Active = value;
        }

        public string UserName
        {
            get => this.userModel.UserName;
            set => this.userModel.UserName = value;
        }
        public string FirstName
        {
            get => this.userModel.FirstName;
            set => this.userModel.FirstName = value;
        }
        public string LastName
        {
            get => this.userModel.LastName;
            set => this.userModel.LastName = value;
        }


        public EditUserViewModel( UserModel userModel)
        {
            this.userModel = userModel;
            this.userService = new UserService();

            this.Roles = new List<UserRole> { UserRole.Worker, UserRole.Manager, UserRole.Administrator };
            this.AccountStatuses = new List<AccountStatus> { AccountStatus.Active, AccountStatus.Inactive };

            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }

        public bool CanSave( object parameter )
        {
            return !string.IsNullOrEmpty( this.UserName ) &&
                   !string.IsNullOrEmpty( this.FirstName ) &&
                   !string.IsNullOrEmpty( this.LastName );
        }
        public void Save( object parameter )
        {
            var response = this.userService.UpdateUser( this.userModel );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }

            this.RaiseCloseEvent();
        }

        public void Cancel( object parameter )
        {
            this.RaiseCloseEvent();
        }
    }
}
