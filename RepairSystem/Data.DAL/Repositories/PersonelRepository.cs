using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public class PersonelRepository : GenericRepository<Personel>, IPersonelRepository
    {
        public PersonelRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public Personel Get( string username )
        {
            return this.Context.Personels.Where( p => p.username == username ).SingleOrDefault();
        }
    }
}
