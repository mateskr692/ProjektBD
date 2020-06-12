using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public interface IActivityRepository : IRepository<Activity>
    {
        IEnumerable<Activity> GetRequestActivities( long requestId, string worker, string status, DateTime? date, string type );
        IEnumerable<Activity> GetWorkerActivities( string worker, DateTime? date, string description, string status );
        Activity GetActivity( long Id );
        void FinishActivity( long Id, string result );
        void CancelActivity( long Id, string result );
    }
}
