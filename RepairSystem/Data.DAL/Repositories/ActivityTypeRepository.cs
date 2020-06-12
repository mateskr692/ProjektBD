using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public class ActivityTypeRepository : GenericRepository<ActivityType>, IActivityTypeRepository
    {
        public ActivityTypeRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public ActivityType GetActivityType( string code )
        {
            return this.Context.ActivityTypes.Where( a => a.activity_code == code ).SingleOrDefault();
        }

        public IEnumerable<ActivityType> GetActivityTypes( string filter )
        {
            IQueryable<ActivityType> types = this.Context.ActivityTypes;
            if ( !string.IsNullOrEmpty( filter ) )
                types = types.Where( a => a.activity_code.Contains( filter ) || a.activity_name.Contains( filter ) );

            return types.OrderBy( a => a.activity_code );
        }
    }
}
