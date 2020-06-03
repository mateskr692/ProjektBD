using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Requests;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels.Requests
{
    public class CancelRequestViewModel : BaseViewModel
    {
        private RequestService RequestService;
        private RequestModel RequestModel;

        public CancelRequestViewModel( RequestModel requestModel)
        {
            this.RequestService = new RequestService();
            this.RequestModel = requestModel;

            this.SaveCommand = new RelayCommand( this.Save, this.CanSave );
            this.CancelCommand = new RelayCommand( this.Cancel );
        }

        #region FinishRequest Interface
        // ---------- Bindings ----------
        public string Result
        {
            get => this.RequestModel.Result;
            set => this.RequestModel.Result = value;
        }


        // --------- Commands ---------
        public RelayCommand SaveCommand { get; set; }
        public bool CanSave( object parameter ) => !string.IsNullOrEmpty( this.Result );
        public void Save( object parameter )
        {
            var response = this.RequestService.CancelRequest( this.RequestModel.Id, this.RequestModel.Result );
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
