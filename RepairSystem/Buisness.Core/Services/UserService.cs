using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Buisness.Contracts;
using Buisness.Contracts.Models;
using Buisness.Core.Mappers;
using Data.DAL;
using Data.DAL.UnitOfWork;

namespace Buisness.Core.Services
{
    public class UserService
    {
        const string InvalidDataErrorMessage = "Server recieved invalid data";
        const string UserDoesNotExistErrorMessage = "No user with this name exists";
        const string InvalidPasswordErrorMessage = "Invalid Password";
        const string NoFilterErrorMessage = "Filter not specified";
        const string userAlreadyExistsErrorMEssage = "userName already taken";


        public WResult<UserModel> ValidateUserCredentials( UserLoginModel userLoginModel )
        {

            if ( userLoginModel == null || string.IsNullOrEmpty(userLoginModel.Login) )
                return new WResult<UserModel>( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            using ( var sha256 = SHA256.Create() )
            {
                var user = uow.Users.GetByUsername( userLoginModel.Login );
                if ( user == null )
                    return new WResult<UserModel>( UserDoesNotExistErrorMessage );

                var hashPassword = sha256.ComputeHash( Encoding.Unicode.GetBytes( userLoginModel.Password ).Concat( user.password_salt ).ToArray() );
                return user.password.SequenceEqual( hashPassword )
                    ? new WResult<UserModel>( UserMapper.Default.Map<UserModel>( user ) )
                    : new WResult<UserModel>( InvalidPasswordErrorMessage );
            }

        }

        public WResult ChangeUserPassword( string userName, string newPassword )
        {
            if ( string.IsNullOrEmpty( userName ) )
                return new WResult( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            using ( var sha256 = SHA256.Create() )
            {
                var user = uow.Users.GetByUsername( userName );
                if ( user == null )
                    return new WResult( UserDoesNotExistErrorMessage );

                var rand = new Random();
                byte[] salt = new byte[ 4 ];
                rand.NextBytes( salt );
                var hashPassword = sha256.ComputeHash( Encoding.Unicode.GetBytes( newPassword ).Concat( salt ).ToArray() );

                user.password_salt = salt;
                user.password = hashPassword;

                uow.Complete();
                return new WResult();
            }
        }

        public WResult<IEnumerable<UserInfoModel>> GetUserList( string nameFilter )
        {
            if ( string.IsNullOrEmpty( nameFilter ) )
                return new WResult<IEnumerable<UserInfoModel>>( NoFilterErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var users = uow.Users.GetUsers( nameFilter );
                return new WResult<IEnumerable<UserInfoModel>>( UserMapper.Default.Map<IEnumerable<UserInfoModel>>( users ) );
            }
        }

        public WResult<UserModel> GetSingleUser( string userName )
        {
            if ( string.IsNullOrEmpty( userName ) )
                return new WResult<UserModel>( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var user = uow.Users.GetByUsername( userName );
                if ( user == null )
                    return new WResult<UserModel>( UserDoesNotExistErrorMessage );

                return new WResult<UserModel>( UserMapper.Default.Map<UserModel>( user ) );
            }
        }

        public WResult AddUser(UserModel user)
        {
            if( user == null)
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            using ( var sha256 = SHA256.Create() )
            {
                var dbUser = uow.Users.GetByUsername( user.UserName );
                if ( dbUser != null )
                    return new WResult( userAlreadyExistsErrorMEssage );

                var personel = new Personel();
                UserMapper.Default.Map( user, personel );
                uow.Personel.Add( personel );

                dbUser = new User();
                dbUser.Personel = personel;
                var rand = new Random();
                byte[] salt = new byte[ 4 ];
                rand.NextBytes( salt );
                var password = Guid.NewGuid().ToString();
                var hashPassword = sha256.ComputeHash( Encoding.Unicode.GetBytes( password ).Concat( salt ).ToArray() );

                UserMapper.Default.Map( user, dbUser );
                dbUser.password = hashPassword;
                dbUser.password_salt = salt;

                uow.Users.Add( dbUser );
                uow.Complete();
                return new WResult();
            }
        }

        public WResult UpdateUser(UserModel user)
        {
            if ( user == null )
                return new WResult( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            {
                var dbUser = uow.Users.GetByUsername( user.UserName );
                if ( dbUser == null )
                    return new WResult( UserDoesNotExistErrorMessage );

                UserMapper.Default.Map( user, dbUser );
                UserMapper.Default.Map( user, dbUser.Personel );
                uow.Complete();
                return new WResult();
            }
        }
    }
}
