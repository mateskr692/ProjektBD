using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Clients;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels.Manager
{
    public class EditClientViewModel : BaseViewModel
    {
        private ClientService ClientService;

        public EditClientViewModel( ClientModel clientModel)
        {
            this.clientModel = clientModel;
            this.ClientService = new ClientService();

            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }

        #region Client Interface
        // --------- Bindings ---------
        private ClientModel clientModel;
        public string Name
        {
            get => this.clientModel.Name;
            set => this.clientModel.Name = value;
        }
        public string FirstName
        {
            get => this.clientModel.FirstName;
            set => this.clientModel.FirstName = value;
        }
        public string LastName
        {
            get => this.clientModel.LastName;
            set => this.clientModel.LastName = value;
        }
        public string Contact
        {
            get => this.clientModel.Contact;
            set => this.clientModel.Contact = value;
        }

        // --------- Bindings ---------
        public RelayCommand SaveCommand { get; set; }
        public bool CanSave( object parameter ) => !string.IsNullOrEmpty( this.Name ) && !string.IsNullOrEmpty( this.FirstName ) && !string.IsNullOrEmpty( this.LastName );
        public void Save( object parameter)
        {
            var response = this.ClientService.UpdateClient( this.clientModel );
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
