using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels.Users
{
    public class SelectWorkerViewModel : BaseViewModel
    {
        private UserModel WorkerModel;
        private UserService UserService;

        public SelectWorkerViewModel( ref UserModel userModel)
        {
            this.WorkerModel = userModel;
            this.UserService = new UserService();

            this.SelectWorkerCommand = new RelayCommand( this.SelectWorker, this.CanSelectWorker );
        }

        #region SelectWorker Interface
        // ---------- Bindings ---------- 
        private string _WorkerNameFilter;
        public string WorkerNameFilter { get => this._WorkerNameFilter; set { this._WorkerNameFilter = value; this.FilterWorkers(); } }

        private List<UserInfoModel> _WorkerList;
        public List<UserInfoModel> WorkerList
        {
            get => this._WorkerList;
            set { this._WorkerList = value; this.OnPropertyChanged(); }
        }
        private UserInfoModel _WorkerListItem;
        public UserInfoModel WorkerListItem
        {
            get => this._WorkerListItem;
            set { this._WorkerListItem = value; this.OnSelectNewWoker(); }
        }

        private UserModel SelectedWorker { get; set; }
        public string WorkerFirstName { get => this.SelectedWorker?.FirstName; }
        public string WorkerLastName { get => this.SelectedWorker?.LastName; }


        // ---------- Commands ---------- 
        public RelayCommand SelectWorkerCommand { get; set; }
        public bool CanSelectWorker( object parameter ) => this.WorkerListItem != null;
        public void SelectWorker( object parameters )
        {
            this.RaiseCloseEvent();
        }


        // ---------- Implementations ---------- 
        private void FilterWorkers()
        {
            var response = this.UserService.GetUserList( this.WorkerNameFilter );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.WorkerList = response.Data.ToList();
        }

        public void OnSelectNewWoker()
        {
            if ( this.WorkerListItem != null )
            {
                var response = this.UserService.GetSingleUser( this.WorkerListItem.UserName );
                if ( response.Status == ResultStatus.Failed )
                {
                    this.ErrorMessage = string.Join( "\n", response.Errors );
                    return;
                }
                this.SelectedWorker = response.Data;

                this.WorkerModel.Active = this.SelectedWorker.Active;
                this.WorkerModel.FirstName = this.SelectedWorker.FirstName;
                this.WorkerModel.LastName = this.SelectedWorker.LastName;
                this.WorkerModel.Role = this.SelectedWorker.Role;
                this.WorkerModel.UserName = this.SelectedWorker.UserName;
            }

            this.OnPropertyChanged( "WorkerFirstName" );
            this.OnPropertyChanged( "WorkerLastName" );
        }

        #endregion
    }
}
