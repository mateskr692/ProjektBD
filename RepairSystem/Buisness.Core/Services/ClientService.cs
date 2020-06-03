using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Buisness.Contracts;
using Buisness.Contracts.Models;
using Buisness.Contracts.Models.Clients;
using Buisness.Core.Mappers;
using Data.DAL;
using Data.DAL.UnitOfWork;

namespace Buisness.Core.Services
{
    public class ClientService
    {
        const string InvalidDataErrorMessage = "Server recieved invalid data";
        const string ClientDoesNotExistErrorMessage = "No Client with this name exists";
        const string NoFilterErrorMessage = "Filter not specified";
        const string ClientAlreadyExistsErrorMEssage = "ClientName already taken";

        public WResult<IEnumerable<ClientInfoModel>> GetClientList( string nameFilter )
        {
            if ( string.IsNullOrEmpty( nameFilter ) )
                return new WResult<IEnumerable<ClientInfoModel>>( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var Clients = uow.Clients.GetClients( nameFilter );
                return new WResult<IEnumerable<ClientInfoModel>>( ClientMapper.Default.Map<IEnumerable<ClientInfoModel>>( Clients ) );
            }
        }

        public WResult<ClientModel> GetSingleClient( long Id )
        {
            using ( var uow = new UnitOfWork() )
            {
                var Client = uow.Clients.GetById( Id );
                if ( Client == null )
                    return new WResult<ClientModel>( ClientDoesNotExistErrorMessage );

                return new WResult<ClientModel>( ClientMapper.Default.Map<ClientModel>( Client ) );
            }
        }

        public WResult AddClient( ClientModel Client )
        {
            if ( Client == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var dbClient = uow.Clients.GetById( Client.Id );
                if ( dbClient != null )
                    return new WResult( ClientAlreadyExistsErrorMEssage );

                dbClient = new Client();

                ClientMapper.Default.Map( Client, dbClient );
                uow.Clients.Add( dbClient );
                uow.Complete();
                return new WResult();
            }
        }

        public WResult UpdateClient( ClientModel Client )
        {
            if ( Client == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var dbClient = uow.Clients.GetById( Client.Id );
                if ( dbClient == null )
                    return new WResult( ClientDoesNotExistErrorMessage );

                ClientMapper.Default.Map( Client, dbClient );
                uow.Complete();
                return new WResult();
            }
        }
    }
}
