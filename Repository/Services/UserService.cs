using System.Collections.Generic;
using System.Threading.Tasks;
using UserDetails.Data;
using UserDetails.Repository.Contracts;
using UserDetails.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserDetails.Data.ViewModel;

namespace UserDetails.Repository.Services
{
    public class UserService: IUserContract
    {
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int ID)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x=>x.Id==ID);
        }

        public async Task<List<User>> SearchUser(string SearchUserValue)
        {
            if (SearchUserValue == null)
            {
                return await _appDbContext.Users.ToListAsync();
            }
            else
            {
                var result = await _appDbContext.Users.
                Where
                (
                x => x.Name.Contains(SearchUserValue)

                || x.Email.Contains(SearchUserValue)
                || x.phone.Contains(SearchUserValue)
                || x.Address.Contains(SearchUserValue)
                || x.ZipCode.Contains(SearchUserValue)
                   ).ToListAsync();
                return result;
            }
            

        }

        public int AddUser(UserVM user)
        {
            var _user = new User()
            {
                Name = user.Name,
                Email = user.Email,
                phone = user.phone,
                Address = user.Address,
                DOB = user.DOB,
                ZipCode = user.ZipCode

            };
            _appDbContext.Users.Add(_user);
            _appDbContext.SaveChanges();

            return  _user.Id;
        }

    }
}
