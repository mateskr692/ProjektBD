using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Buisness.Contracts.Models.Requests;
using Data.DAL;

namespace Buisness.Core.Mappers
{
    internal static class RequestMapper
    {
        public static readonly IMapper Default = new MapperConfiguration( cfg =>
        {
            cfg.CreateMap<Request, RequestInfoModel>()
                .ForMember( d => d.Id, o => o.MapFrom( s => s.request_id ) )
                .ForMember( d => d.Description, o => o.MapFrom( s => s.decription.Substring(0, s.decription.Length < 20 ? s.decription.Length : 20 ) ) )
                .ForMember( d => d.RegistrationDate, o => o.MapFrom( s => s.registration_date ) )
                .ForMember( d => d.Status, o => o.MapFrom( s => s.status ) );

            cfg.CreateMap<Request, RequestModel>()
                .ForMember( d => d.Id, o => o.MapFrom( s => s.request_id ) )
                .ForMember( d => d.Status, o => o.MapFrom( s => s.status ) )
                .ForMember( d => d.StartDate, o => o.MapFrom( s => s.registration_date ) )
                .ForMember( d => d.Description, o => o.MapFrom( s => s.decription ) )
                .ForMember( d => d.Result, o => o.MapFrom( s => s.result ) )
                .ForMember( d => d.FinishDate, o => o.MapFrom( s => s.finish_cancel_date ) )
                .ForMember( d => d.ObjectId, o => o.MapFrom( s => s.object_no ) )
                .ForMember( d => d.ClientName, o => o.MapFrom( s => s.Object.Client.name ) )
                .ForMember( d => d.ManagerId, o => o.MapFrom( s => s.manager ) )
                .ForMember( d => d.ManagerName, o => o.MapFrom( s => string.Join( " ", s.Personel.first_name, s.Personel.last_name ) ) )
                .ForMember( d => d.ObjectName, o => o.MapFrom( s => s.Object.name ) );

            cfg.CreateMap<RequestModel, Request>()
                .ForMember( d => d.decription, o => o.MapFrom( s => s.Description ) )
                .ForMember( d => d.manager, o => o.MapFrom( s => s.ManagerId ) )
                .ForMember( d => d.object_no, o => o.MapFrom( s => s.ObjectId ) )
                .ForMember( d => d.registration_date, o => o.MapFrom( s => s.StartDate ) );


        } ).CreateMapper();

    }
}
