using AutoMapper;
using Buisness.Contracts.Models.Activities;
using Data.DAL;

namespace Buisness.Core.Mappers
{
    internal static class ActivitiesMapper
    {
        public static readonly IMapper Default = new MapperConfiguration( cfg =>
        {
            cfg.CreateMap<ActivityType, ActivityTypeModel>()
               .ForMember( d => d.ActivityCode, o => o.MapFrom( s => s.activity_code ) )
               .ForMember( d => d.ActivityName, o => o.MapFrom( s => s.activity_name ) )
               .ReverseMap();

            cfg.CreateMap<Activity, ActivityInfoModel>()
               .ForMember( d => d.Id, o => o.MapFrom( s => s.activity_id ) )
               .ForMember( d => d.RegistrationDate, o => o.MapFrom( s => s.registration_date ) )
               .ForMember( d => d.Status, o => o.MapFrom( s => s.status ) )
               .ForMember( d => d.ActivityType, o => o.MapFrom( s => s.activity_code ) )
               .ForMember( d => d.Description, o => o.MapFrom( s => s.decription.Substring( 0, s.decription.Length < 20 ? s.decription.Length : 20 ) ) );

            cfg.CreateMap<Activity, ActivityModel>()
               .ForMember( d => d.Id, o => o.MapFrom( s => s.activity_id ) )
               .ForMember( d => d.StartDate, o => o.MapFrom( s => s.registration_date ) )
               .ForMember( d => d.FinishDate, o => o.MapFrom( s => s.finish_cancel_date ) )
               .ForMember( d => d.ActivityTypeCode, o => o.MapFrom( s => s.activity_code ) )
               .ForMember( d => d.ActivityTypeName, o => o.MapFrom( s => s.ActivityType.activity_name ) )
               .ForMember( d => d.Description, o => o.MapFrom( s => s.decription ) )
               .ForMember( d => d.RequestId, o => o.MapFrom( s => s.request_id ) )
               .ForMember( d => d.Result, o => o.MapFrom( s => s.result ) )
               .ForMember( d => d.SequanceNumber, o => o.MapFrom( s => s.sequance_no ) )
               .ForMember( d => d.Status, o => o.MapFrom( s => s.status ) )
               .ForMember( d => d.WorkerId, o => o.MapFrom( s => s.worker ) )
               .ForMember( d => d.WorkerName, o => o.MapFrom( s => string.Join( " ", s.Personel.first_name, s.Personel.last_name ) ) );

            cfg.CreateMap<ActivityModel, Activity>()
               .ForMember( d => d.activity_code, o => o.MapFrom( s => s.ActivityTypeCode ) )
               .ForMember( d => d.decription, o => o.MapFrom( s => s.Description ) )
               .ForMember( d => d.registration_date, o => o.MapFrom( s => s.StartDate ) )
               .ForMember( d => d.request_id, o => o.MapFrom( s => s.RequestId ) )
               .ForMember( d => d.sequance_no, o => o.MapFrom( s => s.SequanceNumber ) )
               .ForMember( d => d.worker, o => o.MapFrom( s => s.WorkerId ) );


        } ).CreateMapper();
    }
}
