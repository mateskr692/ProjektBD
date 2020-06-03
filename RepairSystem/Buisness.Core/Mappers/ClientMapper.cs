using AutoMapper;
using Buisness.Contracts.Models;
using Buisness.Contracts.Models.Clients;
using Common;
using Data.DAL;

namespace Buisness.Core.Mappers
{
    internal static class ClientMapper
    {
        public static readonly IMapper Default = new MapperConfiguration( cfg =>
        {
            cfg.CreateMap<Client, ClientInfoModel>()
                .ForMember( d => d.Id, o => o.MapFrom( s => s.client_id ) )
                .ForMember( d => d.Name, o => o.MapFrom( s => s.name ) )
                .ForMember( d => d.FullName, o => o.MapFrom( s => s.first_name + " " + s.last_name ) );

            cfg.CreateMap<Client, ClientModel>()
                .ForMember( d => d.Id, o => o.MapFrom( s => s.client_id ) )
                .ForMember( d => d.Name, o => o.MapFrom( s => s.name ) )
                .ForMember( d => d.FirstName, o => o.MapFrom( s => s.first_name ) )
                .ForMember( d => d.LastName, o => o.MapFrom( s => s.last_name ) )
                .ForMember( d => d.Contact, o => o.MapFrom( s => s.phone_no ) )
                .ReverseMap();

        } ).CreateMapper();
    }
}
