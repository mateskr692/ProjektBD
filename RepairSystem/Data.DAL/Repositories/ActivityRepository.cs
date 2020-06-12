using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Data.DAL.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public IEnumerable<Activity> GetRequestActivities( long requestId, string worker, string status, DateTime? date, string type )
        {
            IQueryable<Activity> activities = this.Context.Activities.Where( a => a.request_id == requestId );

            if ( date.HasValue )
            {
                DateTime d1 = date.Value.Date;
                DateTime d2 = d1.AddDays( 1 );
                activities = activities.Where( a => a.registration_date >= d1 && a.registration_date < d2 );
            }
            if ( !string.IsNullOrEmpty( status ) )
                activities = activities.Where( r => r.status.Contains( status ) );
            if ( !string.IsNullOrEmpty( type ) )
                activities = activities.Where( r => r.activity_code.Contains(type) || r.ActivityType.activity_name.Contains(type) );
            if ( !string.IsNullOrEmpty( worker ) )
            {
                var words = worker.Split( null );
                activities = words.Count() > 1
                    ? activities.Where( a => a.Personel.first_name.Contains( words[ 0 ] ) && a.Personel.last_name.Contains( words[ 1 ] ) )
                    : activities.Where( a => a.Personel.first_name.Contains( worker ) || a.Personel.last_name.Contains( worker ) );
            }

            return activities.OrderBy( a => a.registration_date);
        }

        public IEnumerable<Activity> GetWorkerActivities( string worker, DateTime? date, string type, string status )
        {
            IQueryable<Activity> activities = this.Context.Activities.Where( a => a.worker == worker );

            if ( date.HasValue )
            {
                DateTime d1 = date.Value.Date;
                DateTime d2 = d1.AddDays( 1 );
                activities = activities.Where( a => a.registration_date >= d1 && a.registration_date < d2 );
            }
            if ( !string.IsNullOrEmpty( type ) )
                activities = activities.Where( r => r.activity_code.Contains( type ) || r.ActivityType.activity_name.Contains( type ) );

            if ( !string.IsNullOrEmpty( status ) )
                activities = activities.Where( a => a.status.Contains( status ) );

            return activities.OrderBy( a => a.registration_date).Take( 10 );
        }

        public Activity GetActivity( long Id )
        {
            return this.Context.Activities.Include( a => a.ActivityType )
                                          .Include( a => a.Personel )
                                          .Where( a => a.activity_id == Id ).SingleOrDefault();
        }

        public void FinishActivity( long Id, string result )
        {
            var currentDate = DateTime.Now;
            var activity = this.Context.Activities.Where( a => a.activity_id == Id ).SingleOrDefault();
            activity.result = result;
            activity.finish_cancel_date = currentDate;
            activity.status = "FIN";

            this.Context.SaveChanges();
        }

        public void CancelActivity( long Id, string result )
        {
            var currentDate = DateTime.Now;
            var activity = this.Context.Activities.Where( a => a.activity_id == Id ).SingleOrDefault();
            activity.result = result;
            activity.finish_cancel_date = currentDate;
            activity.status = "CAN";

            this.Context.SaveChanges();
        }
    }
}
