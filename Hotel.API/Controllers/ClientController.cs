using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.API.ViewModels;
using Hotel.Application.Dto;
using Hotel.Application.Services;
using Hotel.Database;
using Hotel.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("Client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateClient([FromBody] ClientModel clientModel)
        {
            var clientDto = _mapper.Map<ClientDto>(clientModel);
            await _clientService.CreateClient(clientDto);
            return Ok();
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetClients([FromQuery] int? pageSize = 10, [FromQuery] int? pageNumber = 1)
        {
            var clients = await _clientService.GetClients(pageSize.Value, pageNumber.Value);
            return Ok(clients);
        }

        [HttpGet("GetBy")]
        public async Task<ActionResult> GetBy([FromQuery] string? phoneNumber, [FromQuery] string? mail, [FromQuery] string? name, [FromQuery] string? lastName, [FromQuery]int? pageSize = 10, [FromQuery] int? pageNumber = 1)
        {
            if (phoneNumber is not null)
            {
                var client = await _clientService.GetByClientByPhoneNumber(phoneNumber);
                return Ok(client);
            }

            if (mail is not null)
            {
                var client = await _clientService.GetClientByMail(mail);
                return Ok(client);
            }
            if (name is not null && lastName is not null)
            {
                var clients = await _clientService.GetClientsByNameAndLastName(name, lastName, pageSize.Value, pageNumber.Value);
                return Ok(clients);
            }
            if (name is not null)
            {
                var clients = await _clientService.GetClientsByName(name, pageSize.Value, pageNumber.Value);
                return Ok(clients);
            }
            if (lastName is not null)
            {
                var clients = await _clientService.GetClientsByLastName(lastName, pageSize.Value, pageNumber.Value);
                return Ok(clients);
            }
            
            return NotFound();

        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody]ClientModel userModel)
        {
            var dto = _mapper.Map<ClientDto>(userModel);
            dto.Id = id;
            await _clientService.Update(dto);
            return Ok();
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _clientService.Delete(id);
            return Ok();
        }
    }
}
