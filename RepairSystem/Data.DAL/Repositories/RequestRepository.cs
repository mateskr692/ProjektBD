using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.DAL.Repositories
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public IEnumerable<Request> GetRequests( string client, string manager, string obj, string status, DateTime? date )
        {
            IQueryable<Request> requests = this.Context.Requests;

            if ( date.HasValue )
            {
                DateTime d1 = date.Value.Date;
                DateTime d2 = d1.AddDays( 1 );
                requests = requests.Where( r => r.registration_date >= d1 && r.registration_date < d2 );
            }
            if ( !string.IsNullOrEmpty( status ) )
                requests = requests.Where( r => r.status == status );
            if ( !string.IsNullOrEmpty( obj ) )
                requests = requests.Where( r => r.Object.name.Contains( obj ) );
            if ( !string.IsNullOrEmpty( manager ) )
            {
                var words = manager.Split( null );
                requests = words.Count() > 1
                    ? requests.Where( r => r.Personel.first_name.Contains( words[0] ) && r.Personel.last_name.Contains( words[1] ) )
                    : requests.Where( r =>  r.Personel.first_name.Contains( manager ) || r.Personel.last_name.Contains( manager ) );
            }
            if ( !string.IsNullOrEmpty( client ) )
                requests = requests.Where( r => r.Object.Client.name.Contains( client ) || r.Object.Client.first_name.Contains( client ) || r.Object.Client.last_name.Contains( client ) );

            return requests.Take( 10 );
        }

        public Request GetRequest( long Id )
        {
            return this.Context.Requests.Include( r => r.Object )
                                        .Include( r => r.Object.Client )
                                        .Include( r => r.Personel)
                                        .Where( r => r.request_id == Id ).SingleOrDefault();
        }

        public void FinishRequest( long Id, string result )
        {
            var currentDate = DateTime.Now;
            var request = this.Context.Requests.Where( r => r.request_id == Id ).SingleOrDefault();
            request.result = result;
            request.finish_cancel_date = currentDate;
            request.status = "FIN";

            var openActivities = request.Activities.Where( a => a.status == "OPN" );
            foreach ( var activitie in openActivities )
            {
                activitie.status = "CAN";
                activitie.finish_cancel_date = currentDate;
            }

            this.Context.SaveChanges();
        }

        public void CancelRequest( long Id, string result )
        {
            var currentDate = DateTime.Now;
            var request = this.Context.Requests.Where( r => r.request_id == Id ).SingleOrDefault();
            request.result = result;
            request.finish_cancel_date = currentDate;
            request.status = "CAN";

            var openActivities = request.Activities.Where( a => a.status == "OPN" );
            foreach ( var activitie in openActivities )
            {
                activitie.status = "CAN";
                activitie.finish_cancel_date = currentDate;
            }

            this.Context.SaveChanges();
        }
    }
}
