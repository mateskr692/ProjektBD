using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models;
using Buisness.Contracts.Models.Activities;
using Buisness.Contracts.Models.Requests;
using Buisness.Core.Services;
using Common;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Views.Manager;
using Presentation.App.Views.Requests;

namespace Presentation.App.ViewModels.RequestActivities
{
    public class RequestActivitiesViewModel : BaseViewModel
    {
        private RequestModel RequestModel;
        private ActivityFilterModel ActivityFilter;
        private UserModel WorkerModel;

        private ActivityService ActivityService;

        public RequestActivitiesViewModel(RequestModel requestModel)
        {
            this.RequestModel = requestModel;
            this.ActivityFilter = new ActivityFilterModel();
            this.WorkerModel = new UserModel();

            this.ActivityService = new ActivityService();

            this.AddActivityCommand = new RelayCommand( this.AddActivity, this.CanAddActivity );
            this.SelectWorkerCommand = new RelayCommand( this.SelectWorker, this.CanSelectWorker );
            this.FinishActivityCommand = new RelayCommand( this.FinishActivity, this.CanFinishActivity );
            this.CancelActivityCommand = new RelayCommand( this.CancelActivity, this.CanCancelActivity );
        }

        #region RequestActivities Interface
        // ---------- Bindings -----------
        public string ActivityWorkerFilter { set { this.ActivityFilter.WorkerName = value; this.FilterActivities(); } }
        public string ActivityStatusFilter { set { this.ActivityFilter.Status = value; this.FilterActivities(); } }
        public string ActivityTypeFilter { set { this.ActivityFilter.Type = value; this.FilterActivities(); } }
        public string ActivityDateFilter { set { this.ActivityFilter.RegistrationDate = value; this.FilterActivities(); } }

        private List<ActivityInfoModel> _ActivityList;
        public List<ActivityInfoModel> ActivityList
        {
            get => this._ActivityList;
            set { this._ActivityList = value; this.OnPropertyChanged(); }
        }

        private ActivityInfoModel _ActivityListItem;
        public ActivityInfoModel ActivityListItem
        {
            get => this._ActivityListItem;
            set { this._ActivityListItem = value; this.OnActivitySelected(); }
        }

        public ActivityModel SelectedActivity { get; set; }
        public int? ActivitySequenceNo { get => this.SelectedActivity?.SequanceNumber; }
        public ActivityStatus? ActivityStatus { get => this.SelectedActivity?.StatusDescription; }
        public string ActivityStartDate { get => this.SelectedActivity?.StartDate.ToString(); }
        public string ActivityFinishDate { get => this.SelectedActivity?.FinishDate.ToString(); }
        public string ActivityWorkerName { get => this.SelectedActivity?.WorkerName; }
        public string ActivityDescription { get => this.SelectedActivity?.Description; }
        public string ActivityResult { get => this.SelectedActivity?.Result; }
        public string ActivityType { get => this.SelectedActivity?.ActivityTypeName; }

        // ---------- Commands -----------
        public RelayCommand FinishActivityCommand { get; set; }
        public bool CanFinishActivity( object parameter ) => this.RequestModel.Status == CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open ) &&
                                                             this.ActivityListItem != null && this.SelectedActivity.Status == CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open );
        public void FinishActivity( object parameter )
        {
            var window = new FinishRequestActivityWindow( this.SelectedActivity );
            window.ShowDialog();

            this.FilterActivities();
            this.SelectedActivity = null;
        }

        public RelayCommand CancelActivityCommand { get; set; }
        public bool CanCancelActivity( object parameter ) => this.RequestModel.Status == CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open ) &&
                                                             this.ActivityListItem != null && this.SelectedActivity.Status == CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open );
        public void CancelActivity( object parameter )
        {
            var window = new CancelRequestActivityWindow( this.SelectedActivity );
            window.ShowDialog();

            this.FilterActivities();
            this.SelectedActivity = null;
        }

        public RelayCommand AddActivityCommand { get; set; }
        public bool CanAddActivity( object paramter ) => this.RequestModel.Status == CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open );
        public void AddActivity( object parameter )
        {
            var window = new AddRequestActivityWindow( this.RequestModel );
            window.ShowDialog();

            this.FilterActivities();
        }

        public RelayCommand SelectWorkerCommand { get; set; }
        public bool CanSelectWorker( object paramter ) => this.ActivityListItem != null &&
                                                          this.SelectedActivity.Status == CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open ) &&
                                                          this.RequestModel.Status == CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open );
        public void SelectWorker( object parameter )
        {
            var window = new SelectWorkerWindow( ref this.WorkerModel );
            window.ShowDialog();

            var response = this.ActivityService.ChangeActivityWorker( this.SelectedActivity.Id, this.WorkerModel.UserName );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }

            this.FilterActivities();
            this.OnPropertyChanged( "ActivityWorkerName" );
        }

        // ---------- Implementations -----------
        private void FilterActivities()
        {
            var response = this.ActivityService.GetRequestActivities( this.ActivityFilter, this.RequestModel.Id );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.ActivityList = response.Data.ToList();
            this.ActivityListItem = null;
        }

        private void OnActivitySelected()
        {
            if ( this.ActivityListItem != null )
            {
                var response = this.ActivityService.GetActivity( this.ActivityListItem.Id );
                if ( response.Status == ResultStatus.Failed )
                {
                    this.ErrorMessage = string.Join( "\n", response.Errors );
                    return;
                }
                this.SelectedActivity = response.Data;
            }

            this.OnPropertyChanged( "ActivitySequenceNo" );
            this.OnPropertyChanged( "ActivityStatus" );
            this.OnPropertyChanged( "ActivityStartDate" );
            this.OnPropertyChanged( "ActivityFinishDate" );
            this.OnPropertyChanged( "ActivityWorkerName" );
            this.OnPropertyChanged( "ActivityDescription" );
            this.OnPropertyChanged( "ActivityResult" );
            this.OnPropertyChanged( "ActivityType" );

        }

        #endregion
    }
}
