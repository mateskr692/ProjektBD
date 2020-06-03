using AutoMapper;
using Buisness.Contracts.Models.Objects;
using Data.DAL;


namespace Buisness.Core.Mappers
{
    internal static class ObjectMapper
    {
        public static readonly IMapper Default = new MapperConfiguration( cfg =>
        {
            cfg.CreateMap<ObjectType, ObjectTypeModel>()
               .ForMember( d => d.ObjectCode, o => o.MapFrom( s => s.object_code ) )
               .ForMember( d => d.ObjectName, o => o.MapFrom( s => s.object_name ) )
               .ReverseMap();

            cfg.CreateMap<Object, ObjectModel>()
                .ForMember( d => d.ClientId, o => o.MapFrom( s => s.client_id ) )
                .ForMember( d => d.Id, o => o.MapFrom( s => s.object_no ) )
                .ForMember( d => d.Name, o => o.MapFrom( s => s.name ) )
                .ForMember( d => d.Type, o => o.MapFrom( s => s.object_code ) )
                .ReverseMap();

        } ).CreateMapper();
    }
}
