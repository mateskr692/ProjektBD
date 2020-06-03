using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness.Contracts;
using Buisness.Contracts.Models.Activities;
using Buisness.Contracts.Models.Objects;
using Buisness.Core.Mappers;
using Data.DAL;
using Data.DAL.UnitOfWork;
namespace Buisness.Core.Services
{
    public class ObjectService
    {
        const string InvalidDataErrorMessage = "Server recieved invalid data";
        const string NoFilterErrorMessage = "Filter not specified";
        const string ObjectTypeExistsErrorMessage = "Object Type with code already exists";
        const string ObjectTypeNotExistsErrorMessage = "Object Type with this code doesnt exist ";
        const string ClientDoesntExistErrorMessage = "Couldnt find a Client with specified Id";
        const string InvalidObjectOwnerErrorMessage = "the object does not belong to the specified person";

        public WResult<IEnumerable<ObjectTypeModel>> GetObjectTypes( string nameFilter )
        {
            if ( string.IsNullOrEmpty( nameFilter ) )
                return new WResult<IEnumerable<ObjectTypeModel>>( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var objectTypes = uow.ObjectTypes.GetObjectTypes( nameFilter );
                return new WResult<IEnumerable<ObjectTypeModel>>( ObjectMapper.Default.Map<IEnumerable<ObjectTypeModel>>( objectTypes ) );
            }
        }

        public WResult AddObjectType( ObjectTypeModel objectType )
        {
            if ( objectType == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var dbObjectType = uow.ObjectTypes.GetObjectType( objectType.ObjectCode );
                if ( dbObjectType != null )
                    return new WResult( ObjectTypeExistsErrorMessage );

                dbObjectType = new ObjectType();
                ObjectMapper.Default.Map( objectType, dbObjectType );

                uow.ObjectTypes.Add( dbObjectType );
                uow.Complete();
                return new WResult();
            }
        }

        public WResult UpdateObjectType( ObjectTypeModel objectType )
        {
            if ( objectType == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var dbObjectType = uow.ObjectTypes.GetObjectType( objectType.ObjectCode );
                if ( dbObjectType == null )
                    return new WResult( ObjectTypeNotExistsErrorMessage );

                ObjectMapper.Default.Map( objectType, dbObjectType );

                uow.Complete();
                return new WResult();
            }
        }

        public WResult<IEnumerable<ObjectModel>> GetClientObjects( long clientId, string nameFilter)
        {
            if ( string.IsNullOrEmpty( nameFilter ) )
                return new WResult<IEnumerable<ObjectModel>>( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var client = uow.Clients.GetById( clientId );
                if ( client == null )
                    return new WResult<IEnumerable<ObjectModel>>( ClientDoesntExistErrorMessage );

                var ClientObjects = uow.Objects.GetClientObjects( clientId, nameFilter );
                return new WResult<IEnumerable<ObjectModel>>( ObjectMapper.Default.Map<IEnumerable<ObjectModel>>( ClientObjects ) );
            }

        }

        public WResult AddClientObject( ObjectModel objectModel)
        {
            if ( objectModel == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var client = uow.Clients.GetById( objectModel.ClientId );
                if ( client == null )
                    return new WResult<IEnumerable<ObjectModel>>( ClientDoesntExistErrorMessage );

                client.Objects.Add( ObjectMapper.Default.Map<Data.DAL.Object>( objectModel ) );
                uow.Complete();
                return new WResult();
            }
        }

        public WResult UpdateClientObject( ObjectModel objectModel )
        {
            if ( objectModel == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var obj = uow.Objects.GetById( objectModel.Id );
                if( obj.client_id != objectModel.ClientId)
                    return new WResult<IEnumerable<ObjectModel>>( InvalidObjectOwnerErrorMessage );

                ObjectMapper.Default.Map( objectModel, obj );
                uow.Complete();
                return new WResult();
            }
        }
    }
}
