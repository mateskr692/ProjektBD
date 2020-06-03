using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Objects;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Views.ObjectTypes;

namespace Presentation.App.ViewModels.ClientObjects
{
    public class EditClientObjectViewModel : BaseViewModel
    {
        private ObjectService ObjectService;
        private ObjectModel objectModel;
        private ObjectTypeModel objectTypeModel;

        public EditClientObjectViewModel( ObjectModel objectModel)
        {
            this.ObjectService = new ObjectService();
            this.objectModel = objectModel;
            this.objectTypeModel = new ObjectTypeModel();

            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
            this.SelectObjectTypeCommand = new RelayCommand( this.SelectObjectType );

        }

        #region AddClientObject Interface
        // --------- Bindings ---------
        public string ObjectName
        {
            get => this.objectModel.Name;
            set => this.objectModel.Name = value;
        }
        public string ObjectType
        {
            get => this.objectModel.Type;
            set { this.objectModel.Type = value; this.OnPropertyChanged(); }
        }


        // --------- Commands ---------
        public RelayCommand SaveCommand { get; set; }
        public bool CanSave( object parameter ) => !string.IsNullOrEmpty( this.ObjectName ) && this.objectTypeModel != null && this.ObjectType != null;
        public void Save( object parameter)
        {
            var response = this.ObjectService.UpdateClientObject( this.objectModel );
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

        public RelayCommand SelectObjectTypeCommand { get; set; }
        public void SelectObjectType( object parameter )
        {
            var window = new SelectObjectTypeWindow( ref this.objectTypeModel );
            window.ShowDialog();
            this.ObjectType = this.objectTypeModel.ObjectCode;
        }

        #endregion
    }
}
