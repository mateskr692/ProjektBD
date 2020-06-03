using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using Buisness.Contracts.Models;
using Buisness.Contracts.Models.Activities;
using Buisness.Contracts.Models.Objects;
using Buisness.Core.Services;
using Common;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Views.Admin;
using Presentation.App.Views.AdminViews;

namespace Presentation.App.ViewModels
{
    [PrincipalPermission( SecurityAction.Demand, Role = "ADM" )]
    public class AdminMainViewModel : BaseViewModel
    {
        private UserService UserService { get; set; }
        private ActivityService ActivityService { get; set; }
        private ObjectService ObjectService { get; set; }

        public AdminMainViewModel()
        {
            this.UsersList = new List<UserInfoModel>();
            this.UserService = new UserService();
            this.ActivityService = new ActivityService();
            this.ObjectService = new ObjectService();

            this.ChangeUserPasswordCommand = new RelayCommand( this.ChangeUserPassword, this.CanChangePassword );
            this.EditUserCommand = new RelayCommand( this.EditUser, this.CanEditUser );
            this.AddUserCommand = new RelayCommand( this.AddUser );

            this.EditActivityTypeCommand = new RelayCommand( this.EditActivityType, this.CanEditActivityType );
            this.AddActivityTypeCommand = new RelayCommand( this.AddActivityType );

            this.EditObjectTypeCommand = new RelayCommand( this.EditObjectType, this.CanEditObjectType );
            this.AddObjectTypeCommand = new RelayCommand( this.AddObjectType );

        }

        #region Users Interface
        // ---------- Bindings ----------
        private string _usersNameFilter;
        public string UsersNameFilter { get => this._usersNameFilter; set { this._usersNameFilter = value; this.FilterUsers(); } }

        private List<UserInfoModel> _UsersList;
        public List<UserInfoModel> UsersList
        {
            get => this._UsersList;
            set { this._UsersList = value; this.OnPropertyChanged(); }
        }
        private UserInfoModel _UsersListItem;
        public UserInfoModel UsersListItem
        {
            get => this._UsersListItem;
            set { this._UsersListItem = value; this.OnSelectNewUser(); }
        }

        private UserModel SelectedUser { get; set; }
        public string UserName { get => this.SelectedUser?.UserName; }
        public string FirstName { get => this.SelectedUser?.FirstName; }
        public string LastName { get => this.SelectedUser?.LastName; }
        public string AccountType { get => this.SelectedUser != null ? CodesDictionary.RoleType( this.SelectedUser.Role ) : ""; }
        public string AccountStatus { get => this.SelectedUser != null ? CodesDictionary.AccountStatus( this.SelectedUser.Active ) : ""; }


        // ---------- Commands ----------
        public RelayCommand ChangeUserPasswordCommand { get; set; }
        public bool CanChangePassword( object parameter ) => this.SelectedUser != null;
        public void ChangeUserPassword( object parameter )
        {
            var window = new ChangePasswordWindow( this.SelectedUser.UserName );
            window.ShowDialog();
        }

        public RelayCommand EditUserCommand { get; set; }
        public bool CanEditUser( object parameter ) => this.SelectedUser != null;
        public void EditUser( object parameter )
        {
            var window = new EditUserWindow( this.SelectedUser );
            window.ShowDialog();

            this.FilterUsers();
            this.UsersListItem = null;
            this.SelectedUser = null;
            this.OnSelectNewUser();
        }

        public RelayCommand AddUserCommand { get; set; }
        public void AddUser( object parameter )
        {
            var window = new AddUserWindow();
            window.ShowDialog();
        }


        // ---------- Implementations ----------
        private void OnSelectNewUser()
        {
            if ( this.UsersListItem != null )
            {
                var response = this.UserService.GetSingleUser( this.UsersListItem.UserName );
                if ( response.Status == ResultStatus.Failed )
                {
                    this.ErrorMessage = string.Join( "\n", response.Errors );
                    return;
                }
                this.SelectedUser = response.Data;
            }

            this.OnPropertyChanged( "UserName" );
            this.OnPropertyChanged( "FirstName" );
            this.OnPropertyChanged( "LastName" );
            this.OnPropertyChanged( "AccountType" );
            this.OnPropertyChanged( "AccountStatus" );
        }

        public void FilterUsers()
        {
            var response = this.UserService.GetUserList( this.UsersNameFilter );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.UsersList = response.Data.ToList();
        }
        #endregion

        #region ActivityTypes Interface
        // ---------- Bindings ----------
        private string _ActivityTypesFilter;
        public string ActivityTypesFilter { get => this._ActivityTypesFilter; set { this._ActivityTypesFilter = value; this.FilterActivityTypes(); } }

        private List<ActivityTypeModel> _ActivityTypesList;
        public List<ActivityTypeModel> ActivityTypesList
        {
            get => this._ActivityTypesList;
            set { this._ActivityTypesList = value; this.OnPropertyChanged(); }
        }
        private ActivityTypeModel _ActivityTypesListItem;
        public ActivityTypeModel ActivityTypesListItem
        {
            get => this._ActivityTypesListItem;
            set { this._ActivityTypesListItem = value; this.OnSelectNewActivityType(); }
        }

        public string ActivityCode { get => this.ActivityTypesListItem?.ActivityCode; }
        public string ActivityName { get => this.ActivityTypesListItem?.ActivityName; }


        // ---------- Commands ----------
        public RelayCommand EditActivityTypeCommand { get; set; }
        public bool CanEditActivityType( object parameter ) => this.ActivityTypesListItem != null;
        public void EditActivityType( object parameter )
        {
            var window = new EditActivityTypeWindow( this.ActivityTypesListItem );
            window.ShowDialog();

            this.FilterActivityTypes();
            this.ActivityTypesListItem = null;
            this.OnSelectNewActivityType();
        }

        public RelayCommand AddActivityTypeCommand { get; set; }
        public void AddActivityType( object parameter )
        {
            var window = new AddActivityTypeWindow();
            window.ShowDialog();
        }


        // ---------- Implementations ----------
        private void OnSelectNewActivityType()
        {
            this.OnPropertyChanged( "ActivityCode" );
            this.OnPropertyChanged( "ActivityName" );
        }

        public void FilterActivityTypes()
        {
            var response = this.ActivityService.GetActivityTypes( this.ActivityTypesFilter );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.ActivityTypesList = response.Data.ToList();
        }
        #endregion

        #region ObjectTypes Interface
        // ---------- Bindings ----------
        private string _ObjectTypesFilter;
        public string ObjectTypesFilter { get => this._ObjectTypesFilter; set { this._ObjectTypesFilter = value; this.FilterObjectTypes(); } }

        private List<ObjectTypeModel> _ObjectTypesList;
        public List<ObjectTypeModel> ObjectTypesList
        {
            get => this._ObjectTypesList;
            set { this._ObjectTypesList = value; this.OnPropertyChanged(); }
        }
        private ObjectTypeModel _ObjectTypesListItem;
        public ObjectTypeModel ObjectTypesListItem
        {
            get => this._ObjectTypesListItem;
            set { this._ObjectTypesListItem = value; this.OnSelectNewObjectType(); }
        }

        public string ObjectCode { get => this.ObjectTypesListItem?.ObjectCode; }
        public string ObjectName { get => this.ObjectTypesListItem?.ObjectName; }


        // ---------- Commands ----------
        public RelayCommand EditObjectTypeCommand { get; set; }
        public bool CanEditObjectType( object parameter ) => this.ObjectTypesListItem != null;
        public void EditObjectType( object parameter )
        {
            var window = new EditObjectTypeWindow( this.ObjectTypesListItem );
            window.ShowDialog();

            this.FilterObjectTypes();
            this.OnSelectNewObjectType();
        }

        public RelayCommand AddObjectTypeCommand { get; set; }
        public void AddObjectType( object parameter )
        {
            var window = new AddObjectTypeWindow();
            window.ShowDialog();
        }


        // ---------- Implementations ----------
        private void OnSelectNewObjectType()
        {
            this.OnPropertyChanged( "ObjectCode" );
            this.OnPropertyChanged( "ObjectName" );
        }

        public void FilterObjectTypes()
        {
            var response = this.ObjectService.GetObjectTypes( this.ObjectTypesFilter );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.ObjectTypesList = response.Data.ToList();
        }
        #endregion

    }
}
