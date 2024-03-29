﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.Application.Dto;
using Hotel.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [Route("Room")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }


        [HttpPost("AddRoom")]
        public async Task<ActionResult> CreateRoomAsync([FromBody] RoomModel roomModel)
        {
            var dto = _mapper.Map<RoomDto>(roomModel);
            var id = await _roomService.CreateRoomAsync(dto);
            return Created($"Get/{id}", null);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult> GetRoomsAsync([FromQuery]int pageNumber)
        {
            return Ok(await _roomService.GetRoomsAsync(pageNumber));
        }

        [HttpGet("GetByNumber/{roomNumber}")]
        public async Task<ActionResult> GetRoomByNumberAsync([FromRoute] int roomNumber)
        {
            return Ok(await _roomService.GetRoomByNumberAsync(roomNumber));
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetRoomByIdAsync([FromRoute] int id)
        {
            return Ok(await _roomService.GetRoomByIdAsync(id));
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] RoomModel roomModel)
        {
            var dto = _mapper.Map<RoomDto>(roomModel);
            dto.RoomId = id;
            await _roomService.UpdateAsync(dto);
            return Ok();
        }

    }
}
