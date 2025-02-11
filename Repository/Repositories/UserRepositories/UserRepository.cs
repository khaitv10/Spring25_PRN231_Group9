using BOs.Models;
using DAO;
using Repository.Enums;

namespace Repository.Repositories.UserRepositories
{
    public class UserRepository : GenericDAO<User>, IUserRepository
    {
        public async Task<List<User>> GetAllUserExceptAdmin()
        {
            var list = await Get(u => !u.Role.Equals(UserRolesEnums.Admin.ToString()));
            return list.ToList();
        }

        public async Task<User> GetUserById(int id)
        {
            return await GetSingle(u => u.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await GetSingle(u => u.Email.ToLower().Equals(email.ToLower()));
        }
    }
}
