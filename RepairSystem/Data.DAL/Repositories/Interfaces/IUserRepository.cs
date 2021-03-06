﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername( string username );
        IEnumerable<User> GetUsers( string nameFilter );
        IEnumerable<User> GetWorkers( string nameFilter );
    }
}
