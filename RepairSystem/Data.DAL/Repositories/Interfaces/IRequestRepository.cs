using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public interface IRequestRepository : IRepository<Request>
    {
        IEnumerable<Request> GetRequests( string client, string manager, string obj, string status, DateTime? date );
        Request GetRequest( long Id );
        void FinishRequest( long Id, string result );
        void CancelRequest( long Id, string result );
    }
}
