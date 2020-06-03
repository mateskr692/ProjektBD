using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts;
using Buisness.Contracts.Models.Requests;
using Buisness.Core.Mappers;
using Common;
using Common.Enums;
using Data.DAL;
using Data.DAL.UnitOfWork;

namespace Buisness.Core.Services
{
    public class RequestService
    {
        const string InvalidDataErrorMessage = "Server recieved invalid data";
        const string NoFilterErrorMessage = "Filter not specified";
        const string InvalidRequestErrorMessage = "Requst does not exist";
        const string InvalidActivitytErrorMessage = "Activity does not exist";

        public WResult<IEnumerable<RequestInfoModel>> GetFilteredRequest( RequestFiltersModel filters )
        {
            if ( filters == null )
                return new WResult<IEnumerable<RequestInfoModel>>( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                DateTime date;
                DateTime? parsedDate = null;
                if ( DateTime.TryParse( filters.RegistrationDate, out date ) )
                    parsedDate = date;

                var requests = uow.Requests.GetRequests( filters.ClientName, filters.ManagerName, filters.ObjectName, filters.Status, parsedDate );

                return new WResult<IEnumerable<RequestInfoModel>>( RequestMapper.Default.Map<IEnumerable<RequestInfoModel>>( requests ) );
            }
        }

        public WResult<RequestModel> GetRequest( long Id )
        {
            using ( var uow = new UnitOfWork() )
            {
                var request = uow.Requests.GetRequest( Id );
                if ( request == null )
                    return new WResult<RequestModel>( InvalidRequestErrorMessage );

                return new WResult<RequestModel>( RequestMapper.Default.Map<RequestModel>( request ) );
            }
        }

        public WResult<RequestModel> GetActivityRequest( long ActivityId )
        {
            using ( var uow = new UnitOfWork() )
            {
                var activity = uow.Activities.GetActivity( ActivityId );
                if ( activity == null )
                    return new WResult<RequestModel>( InvalidActivitytErrorMessage );

                var request = uow.Requests.GetRequest( activity.request_id );
                return new WResult<RequestModel>( RequestMapper.Default.Map<RequestModel>( request ) );
            }
        }

        public WResult FinishRequest( long Id, string result )
        {
            if ( string.IsNullOrEmpty( result ) )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var request = uow.Requests.GetRequest( Id );
                if ( request == null )
                    return new WResult( InvalidRequestErrorMessage );

                uow.Requests.FinishRequest( Id, result );
                return new WResult();
            }
        }

        public WResult CancelRequest( long Id, string result )
        {
            if ( string.IsNullOrEmpty( result ) )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var request = uow.Requests.GetRequest( Id );
                if ( request == null )
                    return new WResult( InvalidRequestErrorMessage );

                uow.Requests.CancelRequest( Id, result );
                return new WResult();
            }
        }

        public WResult AddRequest( RequestModel requestModel )
        {
            if ( requestModel == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {           
                var request = RequestMapper.Default.Map<Request>( requestModel );

                uow.Requests.Add( request );
                uow.Complete();
                return new WResult();
            }
        }
    }
}
