using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.API.ViewModels;
using Hotel.Application.Dto;
using Hotel.Application.Services;
using Microsoft.AspNetCore.Authorization;

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
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginUser modelUser)
        {
            var dto = _mapper.Map<UserDto>(modelUser);
            return Ok(await _userServices.LoginUser(dto));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetMe()
        {
            var viewModel = new UserModel();
            viewModel.Name = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            viewModel.UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            viewModel.Role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            viewModel.Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            return Ok();
        }

    }
}
