using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models;
using Buisness.Contracts.Models.Clients;
using Buisness.Contracts.Models.Requests;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Views.Admin;
using Presentation.App.Views.Manager;


namespace Presentation.App.ViewModels.Clients
{
    public class SelectClientViewModel : BaseViewModel
    {
        private ClientService ClientService;
        private ClientModel ClientModel;

        public SelectClientViewModel( ref ClientModel clientModel )
        {
            this.ClientService = new ClientService();
            this.ClientModel = clientModel;

            this.SelectClientCommand = new RelayCommand( this.SelectClient, this.CanSelectClient );
        }


        #region Cliients Interface
        // ---------- Bindings ----------
        private string _ClientsNameFilter;
        public string ClientsNameFilter { get => this._ClientsNameFilter; set { this._ClientsNameFilter = value; this.FilterClients(); } }

        private List<ClientInfoModel> _ClientList;
        public List<ClientInfoModel> ClientList
        {
            get => this._ClientList;
            set { this._ClientList = value; this.OnPropertyChanged(); }
        }
        private ClientInfoModel _ClientListItem;
        public ClientInfoModel ClientListItem
        {
            get => this._ClientListItem;
            set { this._ClientListItem = value; this.OnClientSelected(); }
        }

        public ClientModel SelectedClient { get; set; }
        public string ClientName { get => this.SelectedClient?.Name; }
        public string ClientFirstName { get => this.SelectedClient?.FirstName; }
        public string ClientLastName { get => this.SelectedClient?.LastName; }
        public string ClientContact { get => this.SelectedClient?.Contact; }


        // ---------- Commands ----------
        public RelayCommand SelectClientCommand { get; set; }
        public bool CanSelectClient( object parameter ) => this.SelectedClient != null;
        public void SelectClient( object parameters )
        {
            this.RaiseCloseEvent();
        }


        // ---------- Implementations ----------
        public void OnClientSelected()
        {
            if ( this.ClientListItem != null )
            {
                var response = this.ClientService.GetSingleClient( this.ClientListItem.Id );
                if ( response.Status == ResultStatus.Failed )
                {
                    this.ErrorMessage = string.Join( "\n", response.Errors );
                    return;
                }
                this.SelectedClient = response.Data;

                this.ClientModel.Contact = this.SelectedClient.Contact;
                this.ClientModel.FirstName = this.SelectedClient.FirstName;
                this.ClientModel.Id = this.SelectedClient.Id;
                this.ClientModel.LastName = this.SelectedClient.LastName;
                this.ClientModel.Name = this.SelectedClient.Name;
            }

            this.OnPropertyChanged( "ClientName" );
            this.OnPropertyChanged( "ClientFirstName" );
            this.OnPropertyChanged( "ClientLastName" );
            this.OnPropertyChanged( "ClientContact" );
        }

        public void FilterClients()
        {
            var response = this.ClientService.GetClientList( this.ClientsNameFilter );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.ClientList = response.Data.ToList();
        }
        #endregion

    }
}
