using System;
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
    [Route("MethodOfPayment")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MethodOfPaymentController : ControllerBase
    {
        private readonly IMethodOfPaymentService _methodOfPaymentService;
        private readonly IMapper _mapper;

        public MethodOfPaymentController(IMethodOfPaymentService methodOfPaymentService, IMapper mapper)
        {
            _methodOfPaymentService = methodOfPaymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetMethodOfPaymentsAsync()
        {
            return Ok(await _methodOfPaymentService.GetMethodOfPaymentsAsync());
        }
        [HttpPost]
        public async Task<ActionResult> CreateMethodOfPaymentAsync([FromBody]MethodOfPaymentModel methodOfPaymentModel)
        {
            var dto =_mapper.Map<MethodOfPaymentDto>(methodOfPaymentModel);
            await _methodOfPaymentService.CreateMethodOfPaymentAsync(dto);
            return Ok();
        }
        [HttpDelete("{methodOfPaymentId}")]
        public async Task<ActionResult> DeleteMethodOfPaymentAsync([FromRoute] int methodOfPaymentId)
        {
            await _methodOfPaymentService.DeleteMethodOfPaymentAsync(methodOfPaymentId);
            return Ok();
        }

    }
}
