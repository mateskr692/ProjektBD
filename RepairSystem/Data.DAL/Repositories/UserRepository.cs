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
            return this.Context.Users.Where( u => u.username.Equals( username ) ).SingleOrDefault();
        }
    }
}
