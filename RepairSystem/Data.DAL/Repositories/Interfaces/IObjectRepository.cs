using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public interface IObjectRepository : IRepository<Object>
    {
        IEnumerable<Object> GetClientObjects( long clientId, string nameFilter );
        Object GetById( long Id );
    }
}
