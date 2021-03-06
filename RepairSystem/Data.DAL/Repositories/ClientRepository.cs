﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository( RepairSystemContext ctx ) : base( ctx )
        {
        }

        public Client GetById( long Id )
        {
            return this.Context.Clients.Where( c => c.client_id == Id ).SingleOrDefault();
        }

        public IEnumerable<Client> GetClients( string nameFilter )
        {
            IQueryable<Client> clients = this.Context.Clients;
            if ( !string.IsNullOrEmpty( nameFilter ) )
                clients = clients.Where( c => c.name.Contains( nameFilter ) || c.first_name.Contains( nameFilter ) || c.last_name.Contains( nameFilter ) );

            return clients.OrderBy( c => c.name ).Take( 10 );
        }

    }
}
