using AutoMapper;
using Buisness.Contracts.Models;
using Data.DAL;

namespace Buisness.Core.Mappers
{
    internal static class UserMapper
    {
        public static readonly IMapper Default = new MapperConfiguration( cfg =>
        {
            cfg.CreateMap<User, UserInfoModel>()
                .ForMember( d => d.UserName, o => o.MapFrom( s => s.username ) )
                .ForMember( d => d.Id, o => o.MapFrom( s => s.personel_id ) )
                .ForMember( d => d.Role, o => o.MapFrom( s => CodesDictionary.RoleType( s.role ) ) );

        } ).CreateMapper();
    }
}
