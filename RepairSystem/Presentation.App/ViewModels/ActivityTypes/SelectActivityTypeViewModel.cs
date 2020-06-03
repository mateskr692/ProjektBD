using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Activities;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels.ActivityTypes
{
    public class SelectActivityTypeViewModel : BaseViewModel
    {
        private ActivityTypeModel ActivityTypeModel;
        private ActivityService ActivityService;

        public SelectActivityTypeViewModel(ref ActivityTypeModel activityTypeModel)
        {
            this.ActivityTypeModel = activityTypeModel;
            this.ActivityService = new ActivityService();

            this.SelectActivityTypeCommand = new RelayCommand( this.SelectActivityType, this.CanSelectActivityType );
        }

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
        public RelayCommand SelectActivityTypeCommand { get; set; }
        public bool CanSelectActivityType( object parameter ) => this.ActivityTypesListItem != null;
        public void SelectActivityType( object parameter )
        {
            this.RaiseCloseEvent();
        }


        // ---------- Implementations ----------
        private void OnSelectNewActivityType()
        {
            if( this.ActivityTypesListItem != null )
            {
                this.ActivityTypeModel.ActivityCode = this.ActivityTypesListItem.ActivityCode;
                this.ActivityTypeModel.ActivityName = this.ActivityTypesListItem.ActivityName;
            }

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
    }
}
