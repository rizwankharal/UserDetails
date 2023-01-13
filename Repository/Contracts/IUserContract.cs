using System.Collections.Generic;
using System.Threading.Tasks;
using UserDetails.Model;

namespace UserDetails.Repository.Contracts
{
    public interface IUserContract
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int ID);

        Task<List<User>> SearchUser(string SearchUserValue);
    }
}
