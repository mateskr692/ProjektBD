using Buisness.Contracts;
using Buisness.Contracts.Models;

namespace Buisness.Core.Services
{
    public interface IUserService
    {
        WResult<UserInfoModel> Login( UserLoginModel userLoginModel );

    }
}
