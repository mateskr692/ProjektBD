using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public class ObjectRepository : GenericRepository<Object>, IObjectRepository
    {
        public ObjectRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public IEnumerable<Object> GetClientObjects( long clientId, string nameFilter)
        {
            return this.Context.Clients.Where( c => c.client_id == clientId ).SingleOrDefault()
                               .Objects.Where( o => o.name.Contains( nameFilter ) || o.object_code.Contains( nameFilter ) || o.ObjectType.object_name.Contains( nameFilter ) )
                               .Take( 10 );
        }

        public Object GetById( long Id )
        {
            return this.Context.Objects.Where( o => o.object_no == Id ).SingleOrDefault();
        }
    }
}
