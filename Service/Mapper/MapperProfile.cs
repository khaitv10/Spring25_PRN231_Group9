using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Child;
using BOs.RequestModels.DoseRecord;
using BOs.RequestModels.DoseSchedule;
using BOs.RequestModels.User;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.DoseSchedule;
using BOs.ResponseModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Child
            CreateMap<Child, ChildResponseModel>();

            CreateMap<Child, ChildDetailResModel>()
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender ?? ""))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note ?? ""))
            .ForMember(dest => dest.DoseRecords, opt => opt.MapFrom(src => src.DoseRecords.ToList()))
            .ForMember(dest => dest.DoseSchedules, opt => opt.MapFrom(src => src.DoseSchedules.ToList()));

            CreateMap<ChildCreateModel, Child>()
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Dob)))
                .ForMember(dest => dest.ParentId, opt => opt.Ignore());
            CreateMap<ChildUpdateModel, Child>()
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Dob)))
                .ForMember(dest => dest.ParentId, opt => opt.Ignore());

            //User
            CreateMap<User, UserInfoResponseModel>();
            CreateMap<StaffCreateModel, User>();

            // DoseRecord 
            CreateMap<DoseRecordCreateModel, DoseRecord>()
                .ForMember(dest => dest.DoseDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DoseDate)));


            CreateMap<DoseRecordUpdateModel, DoseRecord>()
                .ForMember(dest => dest.DoseDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DoseDate)));


            CreateMap<DoseRecord, DoseRecordResponseModel>()
                .ForMember(dest => dest.DoseDate, opt => opt.MapFrom(src => src.DoseDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? ""))
                .ForMember(dest => dest.DoseNumber, opt => opt.MapFrom(src => src.DoseNumber))
                .ForMember(dest => dest.VaccineName, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Name : "Unknown"))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Description : "Unknown"));


            // DoseSchedule 
            CreateMap<DoseSchedule, DoseScheduleResponseModel>();
            CreateMap<DoseScheduleCreateModel, DoseSchedule>();
            CreateMap<DoseScheduleUpdateModel, DoseSchedule>();
            CreateMap<DoseSchedule, DoseSchedulesRes>()
            .ForMember(dest => dest.VaccineName, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Name : "Unknown"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Description : "Unknown"));


        }

    }
}
