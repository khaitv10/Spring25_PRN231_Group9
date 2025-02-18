using BOs.RequestModels.Service;
using BOs.ResponseModels.Service;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.ServiceService
{
    public interface IServiceService
    {
        Task<List<ServiceResponseModel>> GetAllService(ServiceQueryModel query);
        Task<ServiceResponseModel> GetServiceById(int id);
        Task UpateServiceStatus(int id);
        Task<Result<ServiceResponseModel>> CreateService(ServiceCreateModel model);
        Task<Result<ServiceResponseModel>> UpdateService(int id, ServiceUpdateModel model);

    }
}
