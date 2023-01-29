using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.Application.Dto;
using Hotel.Application.Services;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserService userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUser modelUser)
        {
            var dto =_mapper.Map<UserDto>(modelUser);
            await _userServices.RegisterUser(dto);
            return Ok();
        }
        
    }
}
