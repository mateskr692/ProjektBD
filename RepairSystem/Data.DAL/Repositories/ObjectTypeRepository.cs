using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public class ObjectTypeRepository : GenericRepository<ObjectType>, IObjectTypeRepository
    {
        public ObjectTypeRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public ObjectType GetObjectType( string code )
        {
            return this.Context.ObjectTypes.Where( o => o.object_code == code ).SingleOrDefault();
        }

        public IEnumerable<ObjectType> GetObjectTypes( string filter )
        {
            return this.Context.ObjectTypes.Where( o => o.object_code.Contains( filter ) || o.object_name.Contains( filter ) )
                                           .Take( 10 );
        }
    }
}
