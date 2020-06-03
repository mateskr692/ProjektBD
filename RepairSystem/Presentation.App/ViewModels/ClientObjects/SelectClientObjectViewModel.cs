using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Clients;
using Buisness.Contracts.Models.Objects;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Views;
using Presentation.App.Views.Admin;

namespace Presentation.App.ViewModels.ClientObjects
{
    public class SelectClientObjectViewModel : BaseViewModel
    {
        private ObjectService ObjectService;
        private ClientModel client;
        private ObjectModel ObjectModel;

        public SelectClientObjectViewModel( ClientModel client, ref ObjectModel objectModel )
        {
            this.client = client;
            this.ObjectModel = objectModel;
            this.ObjectService = new ObjectService();

            this.SelectClientObjectCommand = new RelayCommand( this.SelectClientObject, this.CanSelectClientObject );
        }

        #region ClientObjects Interface
        // ---------- Bindings ----------
        private string _ObjectsFilter;
        public string ObjectsFilter { get => this._ObjectsFilter; set { this._ObjectsFilter = value; this.FilterObjects(); } }

        private IEnumerable<ObjectModel> _ObjectsList;
        public IEnumerable<ObjectModel> ObjectsList
        {
            get => this._ObjectsList;
            set { this._ObjectsList = value; this.OnPropertyChanged(); }
        }
        private ObjectModel _ObjectsListItem;
        public ObjectModel ObjectsListItem
        {
            get => this._ObjectsListItem;
            set { this._ObjectsListItem = value; this.OnSelectClientObject(); }
        }
        public string ObjectName { get => this.ObjectsListItem?.Name; }
        public string ObjectType { get => this.ObjectsListItem?.Type; }


        // ---------- Commands ----------
        public RelayCommand SelectClientObjectCommand { get; set; }
        public bool CanSelectClientObject( object parameter ) => this.ObjectsListItem != null;
        public void SelectClientObject( object parameters )
        {
            this.RaiseCloseEvent();
        }


        // ---------- Implementations ----------
        private void OnSelectClientObject()
        {
            if ( this.ObjectsListItem != null )
            {
                this.ObjectModel.Id = this.ObjectsListItem.Id;
                this.ObjectModel.Name = this.ObjectsListItem.Name;
                this.ObjectModel.Type = this.ObjectsListItem.Type;
                this.ObjectModel.ClientId = this.ObjectsListItem.ClientId;
            }

            this.OnPropertyChanged( "ObjectName" );
            this.OnPropertyChanged( "ObjectType" );
        }

        public void FilterObjects()
        {
            var response = this.ObjectService.GetClientObjects( this.client.Id, this.ObjectsFilter );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.ObjectsList = response.Data.ToList();
        }
        #endregion
    }
}
