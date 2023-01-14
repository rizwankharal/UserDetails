using UserDetails.Data.Model;
using UserDetails.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserDetails.Repository.Contracts
{
    public interface IAuthentication
    {
        AuthenticationModel Authenticate(string Name, string Email);
    }
}
