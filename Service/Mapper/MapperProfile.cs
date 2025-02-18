using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Child;
using BOs.RequestModels.Service;
using BOs.RequestModels.User;
using BOs.RequestModels.Vaccine;
using BOs.RequestModels.VaccineStock;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Service;
using BOs.ResponseModels.User;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels.VaccineStock;
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


            //Vaccine
            CreateMap<VaccineStock, VaccineStockResponseModel>();
            CreateMap<VaccineStockCreateModel, Vaccine>();
            CreateMap<VaccineStockUpdateModel, Vaccine>();
            CreateMap<Vaccine, VaccineInfoResponseModel>();
            CreateMap<VaccineCreateModel, Vaccine>();
            CreateMap<BOs.Models.Service, ServiceResponseModel>()
                .ForMember(dest => dest.Vaccine, opt => opt.MapFrom(src => src.ServiceVaccines
                    .Select(sv => new ServiceVaccineResponseModel
                    {
                        NumberOfDose = sv.NumberOfDose,
                        VaccineInfo = new VaccineInfoResponseModel
                        {
                            Id = sv.Vaccine.Id,
                            Name = sv.Vaccine.Name,
                            Description = sv.Vaccine.Description,
                            Origin = sv.Vaccine.Origin,
                            MinAge = sv.Vaccine.MinAge,
                            MaxAge = sv.Vaccine.MaxAge,
                            Status = sv.Vaccine.Status
                        }
                    }).ToList()));
            CreateMap<ServiceCreateModel, BOs.Models.Service>();


            //User
            CreateMap<User, UserInfoResponseModel>();

            CreateMap<StaffCreateModel, User>();

            // DoseRecord 
            CreateMap<DoseRecord, DoseRecordRes>()
            .ForMember(dest => dest.VaccineName, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Name : "Unknown"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Description : "Unknown"));

            // DoseSchedule 
            CreateMap<DoseSchedule, DoseSchedulesRes>()
            .ForMember(dest => dest.VaccineName, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Name : "Unknown"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Vaccine != null ? src.Vaccine.Description : "Unknown"));

        }

    }
}
