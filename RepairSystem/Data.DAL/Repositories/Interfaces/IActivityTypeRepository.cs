using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public interface IActivityTypeRepository : IRepository<ActivityType>
    {
        IEnumerable<ActivityType> GetActivityTypes( string filter );
        ActivityType GetActivityType( string code );
    }

}
