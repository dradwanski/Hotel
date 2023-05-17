using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.Application.Dto;
using Hotel.Application.Services;
using Hotel.Database.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [Route("RoomType")]
    [ApiController]
    [Authorize (Roles = "Admin,Manager")]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IMapper _mapper;

        public RoomTypeController(IRoomTypeService roomTypeService, IMapper mapper)
        {
            _roomTypeService = roomTypeService;
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult> GetRoomTypesAsync()
        {
            return Ok(await _roomTypeService.GetRoomTypesAsync());
        }
        [HttpGet("Get/{Id}")]
        public async Task<ActionResult> GetRoomTypeByIdAsync([FromRoute] int id)
        {
            return Ok(await _roomTypeService.GetRoomTypeByIdAsync(id));
        }
        [HttpPost("AddRoomType")]
        public async Task<ActionResult> CreateRoomTypeAsync([FromBody] RoomTypeModel roomTypeModel)
        {
            var dto = _mapper.Map<RoomTypeDto>(roomTypeModel);
            var id = await _roomTypeService.CreateRoomTypeAsync(dto);
            return Created($"Get/{id}", null);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _roomTypeService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPut("Active/{Id}")]
        public async Task<ActionResult> ChangeActiveOfRoomTypeAsync([FromRoute] int id)
        {
            await _roomTypeService.ChangeActiveOfRoomTypeAsync(id);
            return Ok();
        }
        [HttpPut("Update/{Id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute]int id, [FromBody] RoomTypeModel roomTypeModel)
        {
            var dto = _mapper.Map<RoomTypeDto>(roomTypeModel);
            dto.Id = id;
            await _roomTypeService.UpdateAsync(dto);
            return Ok();
        }
    }
}
