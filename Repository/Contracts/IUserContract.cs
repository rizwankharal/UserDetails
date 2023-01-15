using System.Collections.Generic;
using System.Threading.Tasks;
using UserDetails.Data.ViewModel;
using UserDetails.Model;

namespace UserDetails.Repository.Contracts
{
    public interface IUserContract
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int ID);
        int AddUser(UserVM user);
        Task<List<User>> SearchUser(string SearchUserValue);
    }
}
