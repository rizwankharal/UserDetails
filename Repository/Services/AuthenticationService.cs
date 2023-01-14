using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using UserDetails.Data;
using System.Linq;
using UserDetails.Data.Model;
using UserDetails.Repository.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using UserDetails.Logger;

namespace UserDetails.Repository.Services
{
    public class AuthenticationService:IAuthentication
    {
        private readonly ILoggerService _loggerService;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public AuthenticationService(AppDbContext appDbContext, IConfiguration configuration, ILoggerService loggerService)
        {
            _appDbContext= appDbContext;
           
            _configuration= configuration;
            _loggerService = loggerService;
        }


        public AuthenticationModel  Authenticate(string Name, string Email)
        {
            _loggerService.LogInfo("Authenticating  Users by  :" + '-' + Name +'-'+ Email);
            AuthenticationModel auth = new AuthenticationModel();

            var user =  _appDbContext.Users.SingleOrDefault(x => x.Name == Name && x.Email == Email);

            if (user == null)
            {
                return  null;
            }
            else
            {
                try
                {
                var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name,Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                };

                    var authKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(

                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                        );

                    auth.Token = new JwtSecurityTokenHandler().WriteToken(token);
                }
                catch(Exception ex)
                {
                    _loggerService.LogError($"Method Name: Authenticate: {ex}");
                }
                

                return  auth;
            }
            
        }

       
    }
}
