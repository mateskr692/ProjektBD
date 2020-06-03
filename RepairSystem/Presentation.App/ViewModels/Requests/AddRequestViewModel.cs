using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Clients;
using Buisness.Contracts.Models.Objects;
using Buisness.Contracts.Models.Requests;
using Buisness.Core.Services;
using Common;
using Common.Enums;
using Presentation.App.Commands;
using Presentation.App.Security;
using Presentation.App.Views.ClientObjects;
using Presentation.App.Views.Clients;

namespace Presentation.App.ViewModels.Requests
{
    public class AddRequestViewModel : BaseViewModel
    {
        private RequestService RequestService;

        private RequestModel RequestModel;
        private ClientModel ClientModel;
        private ObjectModel ObjectModel;

        public AddRequestViewModel()
        {
            this.RequestService = new RequestService();

            this.RequestModel = new RequestModel();
            this.RequestModel.StartDate = DateTime.Now;
            this.RequestModel.Status = CodesDictionary.ActivityType( ActivityStatus.Open );
            this.RequestModel.ManagerId = AuthenticationManager.UserName();

            this.ClientModel = new ClientModel();
            this.ObjectModel = new ObjectModel();

            this.SelectClientCommand = new RelayCommand( this.SelectClient );
            this.SelectClientObjectCommand = new RelayCommand( this.SelectClientObject, this.CanSelectClientObject );
            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }

        #region AddRequest Interface
        // --------- Bindings ---------
        public string Description
        {
            get => this.RequestModel.Description;
            set => this.RequestModel.Description = value;
        }
        public string ClientName => this.ClientModel?.Name;
        public string ObjectName => this.ObjectModel?.Name;


        // --------- Commands ---------
        public RelayCommand SelectClientCommand { get; set; }
        public void SelectClient( object parameter )
        {
            var window = new SelectClientWindow( ref this.ClientModel );
            window.ShowDialog();

            this.ObjectModel = new ObjectModel();
            this.OnPropertyChanged( "ClientName" );
            this.OnPropertyChanged( "ObjectName" );
        }

        public RelayCommand SelectClientObjectCommand { get; set; }
        public bool CanSelectClientObject( object parameter ) => this.ClientModel.Id != 0;
        public void SelectClientObject( object parameter )
        {
            var window = new SelectClientObjectWindow( this.ClientModel, ref this.ObjectModel );
            window.ShowDialog();

            this.RequestModel.ObjectId = this.ObjectModel.Id;
            this.OnPropertyChanged( "ObjectName" );
        }

        public RelayCommand SaveCommand { get; set; }
        public bool CanSave( object parameter ) => !string.IsNullOrEmpty( this.Description ) && this.ClientModel.Id != 0 && this.ObjectModel.Id != 0;
        public void Save( object parameter )
        {
            var response = this.RequestService.AddRequest( this.RequestModel );
            if( response.Status == ResultStatus.Failed )
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
