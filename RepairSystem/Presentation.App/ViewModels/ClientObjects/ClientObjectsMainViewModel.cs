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

namespace Presentation.App.ViewModels.Manager
{
    public class ClientObjectsMainViewModel : BaseViewModel
    {
        private ObjectService ObjectService;
        private ClientModel client;

        public ClientObjectsMainViewModel( ClientModel client)
        {
            this.client = client;
            this.ObjectService = new ObjectService();

            this.EditObjectCommand = new RelayCommand( this.EditObject, this.CanEditObject );
            this.AddObjectCommand = new RelayCommand( this.AddObject );
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
        public RelayCommand EditObjectCommand { get; set; }
        public bool CanEditObject( object parameter ) => this.ObjectsListItem != null;
        public void EditObject( object parameters )
        {
            var window = new EditClientObjectsWindow( this.ObjectsListItem );
            window.ShowDialog();
        }

        public RelayCommand AddObjectCommand { get; set; }
        public void AddObject( object parameters )
        {
            var window = new AddClientObjectsWindow(this.client);
            window.ShowDialog();
        }


        // ---------- Implementations ----------
        private void OnSelectClientObject()
        {
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
