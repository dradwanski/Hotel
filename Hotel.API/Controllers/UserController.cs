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
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterUser modelUser)
        {
            var dto =_mapper.Map<UserDto>(modelUser);
            await _userServices.RegisterUserAsync(dto);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginUser modelUser)
        {
            var dto = _mapper.Map<UserDto>(modelUser);
            return Ok(await _userServices.LoginUserAsync(dto));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetMe()
        {
            var viewModel = new UserModel();
            viewModel.Name = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            viewModel.UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            viewModel.Role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            viewModel.Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return Ok(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _userServices.GetUsers();
            var result = _mapper.Map<List<UserModel>>(users);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SetRole")]
        public async Task<ActionResult> SetRoleAsync([FromBody] SetRoleModel modelUser)
        {
            await _userServices.SetRoleAsync(modelUser.UserId, modelUser.RoleName);
            return Ok();
        }
    }
}
