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
using Common;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Views.Admin;
using Presentation.App.Views.Manager;
using Presentation.App.Views.ManagerViews;
using Presentation.App.Views.Requests;

namespace Presentation.App.ViewModels
{
    [PrincipalPermission( SecurityAction.Demand, Role = "MAN" )]
    public class ManagerMainViewModel : BaseViewModel
    {
        private ClientService ClientService;
        private RequestService RequestService;

        public ManagerMainViewModel()
        {
            this.ClientService = new ClientService();
            this.RequestService = new RequestService();
            this.RequestFilters = new RequestFiltersModel();

            this.ShowClientObjectsCommand = new RelayCommand( this.ShowClientObjects, this.CanShowClientObjects );
            this.EditClientCommand = new RelayCommand( this.EditClient, this.CanEditClient );
            this.AddClientCommand = new RelayCommand( this.AddClient );

            this.AddRequestCommand = new RelayCommand( this.AddRequest );
            this.FinishRequestCommand = new RelayCommand( this.FinishRequest, this.CanFinishRequest );
            this.CancelRequestCommand = new RelayCommand( this.CancelRequest, this.CanCancelRequest );
            this.ShowRequestActivitiesCommand = new RelayCommand( this.ShowRequestActivities, this.CanShowRequestActivities );
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
        public RelayCommand ShowClientObjectsCommand { get; set; }
        public bool CanShowClientObjects( object parameter ) => this.SelectedClient != null;
        public void ShowClientObjects( object parameters )
        {
            var window = new ClientObjectsWindow( this.SelectedClient );
            window.ShowDialog();
        }

        public RelayCommand EditClientCommand { get; set; }
        public bool CanEditClient( object parameter ) => this.SelectedClient != null;
        public void EditClient( object parameters )
        {
            var window = new EditClientWindow( this.SelectedClient );
            window.ShowDialog();

            this.FilterClients();
            this.ClientListItem = null;
            this.SelectedClient = null;
            this.OnClientSelected();
        }

        public RelayCommand AddClientCommand { get; set; }
        public void AddClient( object parameters )
        {
            var window = new AddClientWindow();
            window.ShowDialog();

            this.FilterClients();
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

        #region Requests Interface
        // ---------- Bindings ----------
        public RequestFiltersModel RequestFilters;
        public string RequestClientFilter { set { this.RequestFilters.ClientName = value; this.FilterRequests(); } }
        public string RequestManagerFilter { set { this.RequestFilters.ManagerName = value; this.FilterRequests(); } }
        public string RequestObjectFilter { set { this.RequestFilters.ObjectName = value; this.FilterRequests(); } }
        public string RequestStatusFilter { set { this.RequestFilters.Status = value; this.FilterRequests(); } }
        public string RequestDateFilter { set { this.RequestFilters.RegistrationDate = value; this.FilterRequests(); } }

        private List<RequestInfoModel> _RequestsList;
        public List<RequestInfoModel> RequestsList
        {
            get => this._RequestsList;
            set { this._RequestsList = value; this.OnPropertyChanged(); }
        }

        private RequestInfoModel _RequestListItem;
        public RequestInfoModel RequestsListItem
        {
            get => this._RequestListItem;
            set { this._RequestListItem = value; this.OnRequestSelected(); }
        }

        private RequestModel SelectedRequest;
        public string RequestClient { get => this.SelectedRequest?.ClientName; }
        public string RequestObject { get => this.SelectedRequest?.ObjectName; }
        public string RequestManager { get => this.SelectedRequest?.ManagerName; }
        public string RequestStart { get => this.SelectedRequest?.StartDate.ToString(); }
        public string RequestEnd { get => this.SelectedRequest?.FinishDate.ToString(); }
        public ActivityStatus? RequestStatus { get => this.SelectedRequest?.StatusDesctiption; }
        public string RequestDescription { get => this.SelectedRequest?.Description; }
        public string RequestResult { get => this.SelectedRequest?.Result; }


        // ---------- Commands ----------
        public RelayCommand FinishRequestCommand { get; set; }
        public bool CanFinishRequest( object parameter ) => this.RequestsListItem != null && this.SelectedRequest.Status == CodesDictionary.ActivityType( ActivityStatus.Open );
        public void FinishRequest( object parameter )
        {
            var window = new FinishRequestWindow( this.SelectedRequest );
            window.ShowDialog();

            this.FilterRequests();
            this.SelectedRequest = null;
        }

        public RelayCommand CancelRequestCommand { get; set; }
        public bool CanCancelRequest( object parameter ) => this.RequestsListItem != null && this.SelectedRequest.Status == CodesDictionary.ActivityType( ActivityStatus.Open );
        public void CancelRequest( object parameter )
        {
            var window = new CancelRequestWindow( this.SelectedRequest );
            window.ShowDialog();

            this.FilterRequests();
            this.SelectedRequest = null;
        }

        public RelayCommand ShowRequestActivitiesCommand { get; set; }
        public bool CanShowRequestActivities( object parameter ) => this.RequestsListItem != null;
        public void ShowRequestActivities( object parameter )
        {
            var window = new RequestActivitiesWindow( this.SelectedRequest );
            window.ShowDialog();
        }

        public RelayCommand AddRequestCommand { get; set; }
        public void AddRequest( object parameter )
        {
            var window = new AddRequestWindow();
            window.ShowDialog();

            this.FilterRequests();
        }


        // ---------- Implementations ----------
        public void FilterRequests()
        {
            var response = this.RequestService.GetFilteredRequest( this.RequestFilters );
            if ( response.Status == ResultStatus.Failed )
            {
                this.ErrorMessage = string.Join( "\n", response.Errors );
                return;
            }
            this.RequestsList = response.Data.ToList();
            this.RequestsListItem = null;
        }

        public void OnRequestSelected()
        {
            if( this.RequestsListItem != null)
            {
                var response = this.RequestService.GetRequest( this.RequestsListItem.Id );
                if ( response.Status == ResultStatus.Failed )
                {
                    this.ErrorMessage = string.Join( "\n", response.Errors );
                    return;
                }
                this.SelectedRequest = response.Data;
            }

            this.OnPropertyChanged( "RequestClient" );
            this.OnPropertyChanged( "RequestObject" );
            this.OnPropertyChanged( "RequestManager" );
            this.OnPropertyChanged( "RequestStart" );
            this.OnPropertyChanged( "RequestEnd" );
            this.OnPropertyChanged( "RequestStatus" );
            this.OnPropertyChanged( "RequestDescription" );
            this.OnPropertyChanged( "RequestResult" );
        }
        #endregion

    }
}
