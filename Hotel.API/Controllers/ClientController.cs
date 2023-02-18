using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.Application.Dto;
using Hotel.Application.Services;
using Hotel.Database;
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
    }
}
