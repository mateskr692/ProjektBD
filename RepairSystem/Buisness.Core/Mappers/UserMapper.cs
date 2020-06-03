using AutoMapper;
using Buisness.Contracts.Models;
using Common;
using Data.DAL;

namespace Buisness.Core.Mappers
{
    internal static class UserMapper
    {
        public static readonly IMapper Default = new MapperConfiguration( cfg =>
        {
            cfg.CreateMap<User, UserInfoModel>()
                .ForMember( d => d.UserName, o => o.MapFrom( s => s.username ) )
                .ForMember( d => d.FullName, o => o.MapFrom( s => s.Personel.first_name + " " + s.Personel.last_name ))
                .ForMember( d => d.Role, o => o.MapFrom( s => CodesDictionary.RoleType( s.role ) ) );

            cfg.CreateMap<User, UserModel>()
               .ForMember( d => d.UserName, o => o.MapFrom( s => s.username ) )
               .ForMember( d => d.FirstName, o => o.MapFrom( s => s.Personel.first_name ) )
               .ForMember( d => d.LastName, o => o.MapFrom( s => s.Personel.last_name ) )
               .ForMember( d => d.Role, o => o.MapFrom( s => CodesDictionary.RoleType( s.role ) ) )
               .ForMember( d => d.Active, o => o.MapFrom( s => CodesDictionary.AccountStatus( s.active ) ) );

            cfg.CreateMap<UserModel, User>()
                .ForMember( d => d.username, o => o.MapFrom( s => s.UserName ) )
                .ForMember( d => d.role, o => o.MapFrom( s => CodesDictionary.RoleType( s.Role ) ) )
                .ForMember( d => d.active, o => o.MapFrom( s => CodesDictionary.AccountStatus( s.Active ) ) );

            cfg.CreateMap<UserModel, Personel>()
                .ForMember( d => d.username, o => o.MapFrom( s => s.UserName ) )
                .ForMember( d => d.first_name, o => o.MapFrom( s => s.FirstName ) )
                .ForMember( d => d.last_name, o => o.MapFrom( s => s.LastName ) );


        } ).CreateMapper();
    }
}
