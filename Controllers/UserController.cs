﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserDetails.Logger;
using UserDetails.Model;
using UserDetails.Repository.Contracts;

namespace UserDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserContract _userContract;
        private readonly ILoggerService _loggerService;
        private readonly IMemoryCache _memoryCache;

        public UserController(IUserContract userContract, ILoggerService loggerService, IMemoryCache memoryCache)
        {
            _userContract = userContract;
            _loggerService = loggerService;
            _memoryCache = memoryCache;
        }

        [HttpGet("AllUsers")]
        
        public async Task<IActionResult> GetAllUsersAction()
        {
            _loggerService.LogInfo("Fetching All Users");
            var CacheKey = "Users Cache Key";
            try
            {
                if (!_memoryCache.TryGetValue(CacheKey, out List<User> data))
                {
                    _loggerService.LogInfo("Fetching All Users From DB");
                    data = await _userContract.GetAllUsers();

                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {

                        AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromSeconds(60)

                    };

                    _memoryCache.Set(CacheKey, data, cacheExpiryOptions);

                 }
                else
                {
                    _loggerService.LogInfo("Fetching All Users From Cache");
                }
                    if (data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(data);
                    }


            }
            catch(Exception ex)
            {
                _loggerService.LogError($"Method Name: GetAllUsersAction: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UserByID/{Id}")]
        public async Task<IActionResult> GetUserByIdAction(int Id)
        {
            _loggerService.LogInfo("Fetching  Users by Id");
            try
            {
                var data = await _userContract.GetUserById(Id);

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(data);
                }
            }

            catch (Exception ex)
            {
                _loggerService.LogError($"Method Name: GetUserByIdAction: {ex}");
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("SearchUser")]
        public async Task<IActionResult> SearchUserAction(string SearchUserValue)
        {
            _loggerService.LogInfo("Seraching  Users :"+'-'+ SearchUserValue);
            try
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
            catch (Exception ex)
            {
                _loggerService.LogError($"Method Name: SearchUserAction: {ex}");
                return BadRequest(ex.Message);
            }
            
        }
        
    }
}
