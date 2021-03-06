﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
       Client GetById( long Id );
        IEnumerable<Client> GetClients( string nameFilter );
    }
}
