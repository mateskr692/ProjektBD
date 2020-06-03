using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public interface IObjectTypeRepository : IRepository<ObjectType>
    {
        ObjectType GetObjectType( string code );
        IEnumerable<ObjectType> GetObjectTypes( string filter );
    }
}
