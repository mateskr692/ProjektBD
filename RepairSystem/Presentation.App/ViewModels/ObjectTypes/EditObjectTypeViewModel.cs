using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Activities;
using Buisness.Contracts.Models.Objects;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels.Admin
{
    public class EditObjectTypeViewModel : BaseViewModel
    {
        private ObjectTypeModel ObjectType;
        private ObjectService ObjectService;

        public EditObjectTypeViewModel(ObjectTypeModel ObjectTypeModel)
        {
            this.ObjectType = ObjectTypeModel;
            this.ObjectService = new ObjectService();

            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }

        #region EditObjectType Interface
        // --------- Bindings ---------
        public string ObjectCode
        {
            get => this.ObjectType.ObjectCode;
            set => this.ObjectType.ObjectCode = value;
        }

        public string ObjectName
        {
            get => this.ObjectType.ObjectName;
            set => this.ObjectType.ObjectName = value;
        }

        // --------- Commands ---------
        public RelayCommand SaveCommand { get; set; }
        public bool CanSave( object parameter ) => !string.IsNullOrEmpty( this.ObjectCode ) && !string.IsNullOrEmpty( this.ObjectName );
        public void Save( object parameter )
        {
            var response = this.ObjectService.UpdateObjectType( this.ObjectType );
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
