using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Activities;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels.Admin
{
    public class EditActivityTypeViewModel : BaseViewModel
    {
        private ActivityTypeModel ActivityType;
        private ActivityService ActivityService;

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public string ActivityCode
        {
            get => this.ActivityType.ActivityCode;
            set => this.ActivityType.ActivityCode = value;
        }

        public string ActivityName
        {
            get => this.ActivityType.ActivityName;
            set => this.ActivityType.ActivityName = value;
        }

        public EditActivityTypeViewModel(ActivityTypeModel activityTypeModel)
        {
            this.ActivityType = activityTypeModel;
            this.ActivityService = new ActivityService();

            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }


        public bool CanSave( object parameter )
        {
            return !string.IsNullOrEmpty( this.ActivityCode ) &&
                   !string.IsNullOrEmpty( this.ActivityName );
        }
        public void Save( object parameter )
        {
            var response = this.ActivityService.UpdateActivityType( this.ActivityType );
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
