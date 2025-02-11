using BOs.Models;
using BOs.RequestModels.Child;
using BOs.ResponseModels.Child;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.ChildServices
{
    public interface IChildService
    {
        Task<List<ChildResponseModel>> GetAllChilds();
        Task<ChildResponseModel> GetChildById(int id);
        Task CreateChild(ChildCreateModel request, int userId);
        Task UpdateChild(int id, ChildUpdateModel request);
        Task DeleteChild(int id);

    }
}
