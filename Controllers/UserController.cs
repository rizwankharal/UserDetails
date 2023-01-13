using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserDetails.Repository.Contracts;

namespace UserDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserContract _userContract;

        public UserController(IUserContract userContract)
        {
            _userContract = userContract;
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsersAction()
        {
           var data=await _userContract.GetAllUsers();

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("UserByID/{Id}")]
        public async Task<IActionResult> GetUserByIdAction(int Id)
        {
            var data = await _userContract.GetUserById(Id);

            if (data == null)
            {
                return  NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
        [HttpGet("SearchUser")]
        public async Task<IActionResult> SearchUserAction(string SearchUserValue)
        {
            var data = await _userContract.SearchUser(SearchUserValue);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
        
    }
}
