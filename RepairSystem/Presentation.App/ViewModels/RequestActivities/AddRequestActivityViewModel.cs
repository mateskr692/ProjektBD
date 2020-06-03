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
using Presentation.App.Views.ActivityTypes;
using Presentation.App.Views.Manager;

namespace Presentation.App.ViewModels.RequestActivities
{
    public class AddRequestActivityViewModel : BaseViewModel
    {
        private RequestModel RequestModel;
        private ActivityModel ActivityModel;
        private ActivityService ActivityService;

        private UserModel WorkerModel;
        private ActivityTypeModel ActivityTypeModel;

        public AddRequestActivityViewModel( RequestModel requestModel)
        {
            this.ActivityService = new ActivityService();
            this.RequestModel = requestModel;
            this.WorkerModel = new UserModel();
            this.ActivityTypeModel = new ActivityTypeModel();

            this.ActivityModel = new ActivityModel();
            this.ActivityModel.RequestId = this.RequestModel.Id;
            this.ActivityModel.StartDate = DateTime.Now;
            this.ActivityModel.Status = CodesDictionary.ActivityType( ActivityStatus.Open );

            this.SelecActivityTypeCommand = new RelayCommand( this.SelecActivityType );
            this.SelectWorkerCommand = new RelayCommand( this.SelectWorker );
            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }

        #region AddRequestActivity Interface
        // -------- Bindings ----------
        public int? ActivityNumber
        {
            get => this.ActivityModel.SequanceNumber;
            set => this.ActivityModel.SequanceNumber = value;
        }
        public string ActivityDescription
        {
            get => this.ActivityModel.Description;
            set => this.ActivityModel.Description = value;
        }
        public string ActivityWorkerName => string.Join( " ", this.WorkerModel.FirstName, this.WorkerModel.LastName );
        public string ActivityType => this.ActivityTypeModel.ActivityCode;

        // -------- Commands ----------
        public RelayCommand SelecActivityTypeCommand { get; set; }
        public void SelecActivityType( object parameter )
        {
            var window = new SelectActivityTypeWindow( ref this.ActivityTypeModel );
            window.ShowDialog();
            this.ActivityModel.ActivityTypeCode = this.ActivityTypeModel.ActivityCode;

            this.OnPropertyChanged( "ActivityType" );
        }

        public RelayCommand SelectWorkerCommand { get; set; }
        public void SelectWorker( object parameter )
        {
            var window = new SelectWorkerWindow( ref this.WorkerModel );
            window.ShowDialog();
            this.ActivityModel.WorkerId = this.WorkerModel.UserName;

            this.OnPropertyChanged( "ActivityWorkerName" );
        }

        public RelayCommand SaveCommand { get; set; }
        public bool CanSave( object parameter ) => !string.IsNullOrEmpty( this.ActivityDescription ) && !string.IsNullOrEmpty( this.ActivityTypeModel.ActivityCode );
        public void Save( object parameter )
        {
            var response = this.ActivityService.AddActivity( this.ActivityModel );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.RaiseCloseEvent();
        }

        public RelayCommand CancelCommand { get; set; }
        public void Cancel( object parameter )
        {
            this.RaiseCloseEvent();
        }

        #endregion
    }
}
