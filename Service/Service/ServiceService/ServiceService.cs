using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Service;
using BOs.ResponseModels.Service;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Repositories.ServiceRepository;
using Repository.Repositories.VaccineRepositories;
using Repository.ServiceVaccineRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.ServiceService
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IServiceVaccineRepository _serviceVaccineRepository;
        private readonly IMapper _mapper;
        public ServiceService(IMapper mapper, IServiceRepository serviceRepository, IVaccineRepository vaccineRepository, IServiceVaccineRepository serviceVaccineRepository)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository; 
            _vaccineRepository = vaccineRepository;
            _serviceVaccineRepository = serviceVaccineRepository;
        }

        public async Task<Result<ServiceResponseModel>> CreateService(ServiceCreateModel model)
        {
            //check vaccine
            List<ServiceVaccine> serviceVaccineList = new List<ServiceVaccine>();
            var totalDose = 0;
            foreach (var x in model.Vaccines) {
                var vaccine = await _vaccineRepository.GetVaccineById(x.Key);
                if (vaccine == null)
                {
                    return new Result<ServiceResponseModel>
                    {
                        Success = false,
                        Message = "Vaccine does not exist"
                    };
                }
                if (vaccine.Status == false) {
                    return new Result<ServiceResponseModel>
                    {
                        Success = false,
                        Message = "Vaccine is not available"
                    };
                }

                var serviceVaccince = new ServiceVaccine
                {
                    NumberOfDose = x.Value,
                    VaccineId = x.Key,

                };
                serviceVaccineList.Add(serviceVaccince);
                totalDose += x.Value;
            }
            //map 
            BOs.Models.Service service = _mapper.Map<BOs.Models.Service>(model);
            service.CreateAt = DateTime.Now;
            service.ServiceVaccines =  serviceVaccineList;
            service.TotalDoses = totalDose;
            //create
            try
            {
               await _serviceRepository.Insert(service);
               var res = await _serviceRepository.GetServiceById(service.Id);
                return new Result<ServiceResponseModel>
                {
                    Success = true,
                    Message = "Create Service Successfully",
                    Data = _mapper.Map<ServiceResponseModel>(res)
                };
            }
            catch (Exception ex)
            {
                return new Result<ServiceResponseModel>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<List<ServiceResponseModel>> GetAllService(ServiceQueryModel query)
        {
            var result = await _serviceRepository.FilterService(query);
            return _mapper.Map<List<ServiceResponseModel>>(result);
        }

        public async Task<ServiceResponseModel> GetServiceById(int id)
        {
            var result = await _serviceRepository.GetServiceById(id);
            return _mapper.Map<ServiceResponseModel>(result);
        }

        public async Task UpateServiceStatus(int id)
        {
            var result = await _serviceRepository.GetByID(id);
            if (result.Status == true) { result.Status = false; }
            else { result.Status = true; }
            result.UpdateAt = DateTime.Now;
            await _serviceRepository.Update(result);

        }

        public async Task<Result<ServiceResponseModel>> UpdateService(int id, ServiceUpdateModel model)
        {
            var service = await _serviceRepository.GetServiceById(id);
            var serviceVaccine = new List<ServiceVaccine>();
            if(service == null)
            {
                return new Result<ServiceResponseModel>
                {
                    Success = false,
                    Message = "Service does not exist"
                };
            }

            if (model.Name != null) {
                service.Name = model.Name;
            }
            if (model.Price != null)
            {
                service.Price = model.Price;
            }
            if(model.Vaccines != null)
            {
                 serviceVaccine = await _serviceVaccineRepository.GetServiceVaccineByServiceId(service.Id);

                List<ServiceVaccine> serviceVaccineList = new List<ServiceVaccine>();
                var totalDose = 0;
                foreach (var x in model.Vaccines)
                {
                    var vaccine = await _vaccineRepository.GetVaccineById(x.Key);
                    if (vaccine == null)
                    {
                        return new Result<ServiceResponseModel>
                        {
                            Success = false,
                            Message = "Vaccine does not exist"
                        };
                    }
                    if (vaccine.Status == false)
                    {
                        return new Result<ServiceResponseModel>
                        {
                            Success = false,
                            Message = "Vaccine is not available"
                        };
                    }

                    var serviceVaccince = new ServiceVaccine
                    {
                        NumberOfDose = x.Value,
                        VaccineId = x.Key,

                    };
                    serviceVaccineList.Add(serviceVaccince);
                    totalDose += x.Value;
                }
                service.TotalDoses = totalDose;
                service.ServiceVaccines = serviceVaccineList;   
            } 
            service.UpdateAt = DateTime.Now;
            try
            {

                await _serviceRepository.Update(service);
                await _serviceVaccineRepository.DeleteRange(serviceVaccine);
                var res = await _serviceRepository.GetServiceById(service.Id);
                return new Result<ServiceResponseModel>
                {
                    Success = true,
                    Message = "Update Service Successfully",
                    Data = _mapper.Map<ServiceResponseModel>(res)
                };
            }
            catch (Exception ex) {
                return new Result<ServiceResponseModel>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }

        }
    }
}
