using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts;
using Buisness.Contracts.Models.Activities;
using Buisness.Core.Mappers;
using Data.DAL;
using Data.DAL.UnitOfWork;

namespace Buisness.Core.Services
{
    public class ActivityService
    {
        const string InvalidDataErrorMessage = "Server recieved invalid data";
        const string NoFilterErrorMessage = "Filter not specified";
        const string ActivityTypeExistsErrorMessage = "Activity Type with code already exists";
        const string ActivityTypeNotExistsErrorMessage = "Activity Type with this code doesnt exist ";
        const string InvalidActivityErrorMessage = "Activity does not exist";
        const string InvalidWorkerErrorMessage = "Worker does not exist";
        const string InvalidRequestErrorMessage = "Request does not exist";

        public WResult<IEnumerable<ActivityTypeModel>> GetActivityTypes( string nameFilter )
        {
            using ( var uow = new UnitOfWork() )
            {
                var activityTypes = uow.ActivityTypes.GetActivityTypes( nameFilter );
                return new WResult<IEnumerable<ActivityTypeModel>>( ActivitiesMapper.Default.Map<IEnumerable<ActivityTypeModel>>( activityTypes ) );
            }
        }

        public WResult AddActivityType( ActivityTypeModel activityType )
        {
            if ( activityType == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var dbActivityType = uow.ActivityTypes.GetActivityType( activityType.ActivityCode );
                if ( dbActivityType != null )
                    return new WResult( ActivityTypeExistsErrorMessage );

                dbActivityType = new ActivityType();
                ActivitiesMapper.Default.Map( activityType, dbActivityType );

                uow.ActivityTypes.Add( dbActivityType );
                uow.Complete();
                return new WResult();
            }
        }

        public WResult UpdateActivityType( ActivityTypeModel activityType )
        {
            if ( activityType == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var dbActivityType = uow.ActivityTypes.GetActivityType( activityType.ActivityCode );
                if ( dbActivityType == null )
                    return new WResult( ActivityTypeNotExistsErrorMessage );

                ActivitiesMapper.Default.Map( activityType, dbActivityType );

                uow.Complete();
                return new WResult();
            }
        }

        public WResult<IEnumerable<ActivityInfoModel>> GetRequestActivities( ActivityFilterModel filters, long requestId )
        {
            if ( filters == null )
                return new WResult<IEnumerable<ActivityInfoModel>>( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                DateTime date;
                DateTime? parsedDate = null;
                if ( DateTime.TryParse( filters.RegistrationDate, out date ) )
                    parsedDate = date;

                var request = uow.Requests.GetRequest( requestId );
                if ( request == null )
                    return new WResult<IEnumerable<ActivityInfoModel>>( InvalidRequestErrorMessage );

                var activities = uow.Activities.GetRequestActivities( requestId, filters.WorkerName, filters.Status, parsedDate, filters.Type );
                return new WResult<IEnumerable<ActivityInfoModel>>( ActivitiesMapper.Default.Map<IEnumerable<ActivityInfoModel>>( activities ) );
            }
        }

        public WResult<IEnumerable<ActivityInfoModel>> GetWorkerActivities( WorkerActivityFilterModel filters )
        {
            if ( filters == null )
                return new WResult<IEnumerable<ActivityInfoModel>>( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                DateTime date;
                DateTime? parsedDate = null;
                if ( DateTime.TryParse( filters.RegistrationDate, out date ) )
                    parsedDate = date;

                var worker = uow.Users.GetByUsername( filters.WorkerId );
                if ( worker == null )
                    return new WResult<IEnumerable<ActivityInfoModel>>( InvalidWorkerErrorMessage );

                var activities = uow.Activities.GetWorkerActivities( filters.WorkerId, parsedDate, filters.ActivityType, filters.ActivityStatus );
                return new WResult<IEnumerable<ActivityInfoModel>>( ActivitiesMapper.Default.Map<IEnumerable<ActivityInfoModel>>( activities ) );
            }
        }

        public WResult<ActivityModel> GetActivity( long Id )
        {
            using ( var uow = new UnitOfWork() )
            {
                var activity = uow.Activities.GetActivity( Id );
                if ( activity == null )
                    return new WResult<ActivityModel>( InvalidActivityErrorMessage );

                return new WResult<ActivityModel>( ActivitiesMapper.Default.Map<ActivityModel>( activity ) );
            }
        }

        public WResult FinishActivity( long Id, string result )
        {
            if ( string.IsNullOrEmpty( result ) )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var activity = uow.Activities.GetActivity( Id );
                if ( activity == null )
                    return new WResult( InvalidActivityErrorMessage );

                uow.Activities.FinishActivity( Id, result );
                return new WResult();
            }
        }

        public WResult CancelActivity( long Id, string result )
        {
            if ( string.IsNullOrEmpty( result ) )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var activity = uow.Activities.GetActivity( Id );
                if ( activity == null )
                    return new WResult( InvalidActivityErrorMessage );

                uow.Activities.CancelActivity( Id, result );
                return new WResult();
            }
        }

        public WResult AddActivity( ActivityModel activityModel )
        {
            if ( activityModel == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var activity = ActivitiesMapper.Default.Map<Activity>( activityModel );

                uow.Activities.Add( activity );
                uow.Complete();
                return new WResult();
            }
        }

        public WResult ChangeActivityWorker( long activityId, string workerId )
        {
            if ( string.IsNullOrEmpty( workerId ) )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var activity = uow.Activities.GetActivity( activityId );
                if ( activity == null )
                    return new WResult( InvalidActivityErrorMessage );

                var worker = uow.Users.GetByUsername( workerId );
                if ( worker == null )
                    return new WResult( InvalidWorkerErrorMessage );

                activity.worker = workerId;
                uow.Complete();
                return new WResult();
            }
        }
    }
}
