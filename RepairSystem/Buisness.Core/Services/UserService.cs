using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Buisness.Contracts;
using Buisness.Contracts.Models;
using Buisness.Core.Mappers;
using Data.DAL.UnitOfWork;

namespace Buisness.Core.Services
{
    public class UserService : IUserService
    {
        const string InvalidDataErrorMessage = "Server recieved invalid data";
        const string UserDoesNotExistErrorMessage = "No user with this name exists";
        const string InvalidPasswordErrorMessage = "Invalid Password";

        public WResult<UserInfoModel> Login( UserLoginModel userLoginModel )
        {
            if ( userLoginModel == null || string.IsNullOrEmpty(userLoginModel.Login) || string.IsNullOrEmpty(userLoginModel.Password) )
                return new WResult<UserInfoModel>( InvalidDataErrorMessage );

            using ( var uow = new UnitOfWork() )
            using ( var sha256 = SHA256.Create() )
            {
                var user = uow.Users.GetByUsername( userLoginModel.Login );
                if ( user == null )
                    return new WResult<UserInfoModel>( UserDoesNotExistErrorMessage );

                var hashPassword = sha256.ComputeHash( Encoding.UTF8.GetBytes( userLoginModel.Password ).Concat( user.password_salt ).ToArray() );
                return user.password.SequenceEqual( hashPassword )
                    ? new WResult<UserInfoModel>( UserMapper.Default.Map<UserInfoModel>( user ) )
                    : new WResult<UserInfoModel>( InvalidPasswordErrorMessage );
            }

        }
    }
}
