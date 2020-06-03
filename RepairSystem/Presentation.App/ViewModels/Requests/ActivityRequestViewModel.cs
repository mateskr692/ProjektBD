using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts.Models.Activities;
using Buisness.Contracts.Models.Requests;
using Buisness.Core.Services;
using Common.Enums;
using Presentation.App.Commands;

namespace Presentation.App.ViewModels.Requests
{
    public class ActivityRequestViewModel : BaseViewModel
    {
        private RequestService RequestService;
        private RequestModel RequestModel;

        public ActivityRequestViewModel(ActivityModel activityModel)
        {
            this.RequestService = new RequestService();
            this.CloseCommand = new RelayCommand( this.Close );

            var response = this.RequestService.GetRequest( activityModel.RequestId );
            if( response.Status == ResultStatus.Failed )
            {
                this.RaiseCloseEvent();
            }
            this.RequestModel = response.Data;

        }

        public string RequestObject => this.RequestModel.ObjectName;
        public string RequestManager => this.RequestModel.ManagerName;
        public string RequestDescription => this.RequestModel.Description;

        public RelayCommand CloseCommand { get; set; }
        public void Close( object parameter )
        {
            this.RaiseCloseEvent();
        }

    }
}
