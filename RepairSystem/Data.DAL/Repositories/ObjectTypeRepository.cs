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
    }
}
