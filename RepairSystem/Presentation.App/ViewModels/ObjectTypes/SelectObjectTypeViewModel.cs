using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Objects;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Views.Admin;

namespace Presentation.App.ViewModels.ObjectTypes
{
    public class SelectObjectTypeViewModel : BaseViewModel
    {
        private ObjectService ObjectService;
        private ObjectTypeModel objectType;

        public SelectObjectTypeViewModel( ref ObjectTypeModel objectType )
        {
            this.ObjectService = new ObjectService();
            this.objectType = objectType;

            this.SelectObjectTypeCommand = new RelayCommand( this.SelectObjectType, this.CanSelectObjectType );
        }

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
        public RelayCommand SelectObjectTypeCommand { get; set; }
        public bool CanSelectObjectType( object parameter ) => this.ObjectTypesListItem != null;
        public void SelectObjectType(object parameter)
        {
            this.RaiseCloseEvent();
        }


        // ---------- Implementations ----------
        private void OnSelectNewObjectType()
        {
            this.objectType.ObjectCode = this.ObjectTypesListItem?.ObjectCode;
            this.objectType.ObjectName = this.ObjectTypesListItem?.ObjectName;

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
