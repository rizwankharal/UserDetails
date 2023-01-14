using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UserDetails.Data.Model;
using UserDetails.Logger;
using UserDetails.Model;
using UserDetails.Repository.Contracts;

namespace UserDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILoggerService _loggerService;
        private readonly IAuthentication _authentication;

        public AuthenticationController(IAuthentication authentication, ILoggerService loggerService)
        {
            _authentication = authentication;
            _loggerService = loggerService;
        }

        [HttpPost("GetToken")]

        public IActionResult GetToken([FromBody] AuthenticationModel Model)
        {
            _loggerService.LogInfo("Geting Tokens");
            try
            {
               var token = _authentication.Authenticate(Model.Name, Model.Email);

                if (token == null)
                {
                   return Unauthorized();
                }
                {
                    return Ok(token);
                }
              
             }
            catch(Exception ex)
            {
                _loggerService.LogError($"Method Name: GetToken: {ex}");
                return BadRequest(ex);
            }
            
        }

    }
}
