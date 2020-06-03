using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public User GetByUsername( string username )
        {
            return this.Context.Users.Include( u => u.Personel ).Where( u => u.username.Equals( username ) ).SingleOrDefault();
        }

        public IEnumerable<User> GetUsers( string nameFilter )
        {
            return this.Context.Users.Include( u => u.Personel )
                                     .Where( u => u.username.Contains( nameFilter ) || u.Personel.first_name.Contains( nameFilter ) || u.Personel.last_name.Contains( nameFilter ) )
                                     .Take( 10 );
        }

    }
}
