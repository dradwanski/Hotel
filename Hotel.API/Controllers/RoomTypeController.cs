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
        public async Task<ActionResult> GetRoomTypes()
        {
            return Ok(await _roomTypeService.GetRoomTypes());
        }
        [HttpGet("Get/{Id}")]
        public async Task<ActionResult> GetRoomTypeById([FromRoute] int id)
        {
            return Ok(await _roomTypeService.GetRoomTypeById(id));
        }
        [HttpPost("AddRoomType")]
        public async Task<ActionResult> CreateRoomType([FromBody] RoomTypeModel roomTypeModel)
        {
            var dto = _mapper.Map<RoomTypeDto>(roomTypeModel);
            var id = await _roomTypeService.CreateRoomType(dto);
            return Created($"Get/{id}", null);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _roomTypeService.Delete(id);
            return NoContent();
        }
        [HttpPut("Active/{Id}")]
        public async Task<ActionResult> ChangeActiveOfRoomType([FromRoute] int id)
        {
            await _roomTypeService.ChangeActiveOfRoomType(id);
            return Ok();
        }
        [HttpPut("Update/{Id}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody] RoomTypeModel roomTypeModel)
        {
            var dto = _mapper.Map<RoomTypeDto>(roomTypeModel);
            dto.Id = id;
            await _roomTypeService.Update(dto);
            return Ok();
        }
    }
}
