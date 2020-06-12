using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Activities;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Security;
using Presentation.App.Views.Requests;
using Presentation.App.Views.Worker;

namespace Presentation.App.ViewModels
{
    [PrincipalPermission( SecurityAction.Demand, Role = "WRK" )]
    public class WorkerMainViewModel : BaseViewModel
    {
        private ActivityService ActivityService;
        private WorkerActivityFilterModel ActivityFilter;

        public WorkerMainViewModel()
        {
            this.ActivityService = new ActivityService();
            this.ActivityFilter = new WorkerActivityFilterModel();
            this.ActivityFilter.WorkerId = AuthenticationManager.UserName();

            this.CancelActivityCommand = new RelayCommand( this.CancelActivity, this.CanCancelActivity );
            this.FinishActivityCommand = new RelayCommand( this.FinishActivity, this.CanFinishActivity );
            this.ShowRequestCommand = new RelayCommand( this.ShowRequest, this.CanShowRequest );

            //this.OnPropertyChanged( "ActivityStatusFilter" );
        }

        #region RequestActivities Interface
        // ---------- Bindings -----------
        public string ActivityTypeFilter { set { this.ActivityFilter.ActivityType = value; this.FilterActivities(); } }
        public string ActivityDateFilter { set { this.ActivityFilter.RegistrationDate = value; this.FilterActivities(); } }
        public string ActivityStatusFilter { set { this.ActivityFilter.ActivityStatus = value; this.FilterActivities(); }
                                             get => this.ActivityFilter.ActivityStatus; }

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
        public string ActivityStartDate { get => this.SelectedActivity?.StartDate.ToString(); }
        public string ActivityType { get => this.SelectedActivity?.ActivityTypeName; }
        public string ActivityDescription { get => this.SelectedActivity?.Description; }


        // ---------- Commands -----------
        public RelayCommand FinishActivityCommand { get; set; }
        public bool CanFinishActivity( object parameter ) => this.ActivityListItem != null;
        public void FinishActivity( object parameter )
        {
            var window = new FinishRequestActivityWindow( this.SelectedActivity );
            window.ShowDialog();

            this.FilterActivities();
            this.SelectedActivity = null;
        }

        public RelayCommand CancelActivityCommand { get; set; }
        public bool CanCancelActivity( object parameter ) => this.ActivityListItem != null;
        public void CancelActivity( object parameter )
        {
            var window = new CancelRequestActivityWindow( this.SelectedActivity );
            window.ShowDialog();

            this.FilterActivities();
            this.SelectedActivity = null;
        }

        public RelayCommand ShowRequestCommand { get; set; }
        public bool CanShowRequest( object parameter ) => this.ActivityListItem != null;
        public void ShowRequest( object parameter )
        {
            var window = new ActivityRequestWindow( this.SelectedActivity );
            window.ShowDialog();
        }

        // ---------- Implementations -----------
        private void FilterActivities()
        {
            var response = this.ActivityService.GetWorkerActivities( this.ActivityFilter );
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
            this.OnPropertyChanged( "ActivityStartDate" );
            this.OnPropertyChanged( "ActivityDescription" );
            this.OnPropertyChanged( "ActivityType" );
        }

        #endregion
    }
}
