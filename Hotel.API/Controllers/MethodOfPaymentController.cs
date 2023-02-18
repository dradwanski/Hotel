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
        public async Task<ActionResult> GetMethodOfPayments()
        {
            return Ok(await _methodOfPaymentService.GetMethodOfPayments());
        }
        [HttpPost]
        public async Task<ActionResult> CreateMethodOfPayment([FromBody]MethodOfPaymentModel methodOfPaymentModel)
        {
            var dto =_mapper.Map<MethodOfPaymentDto>(methodOfPaymentModel);
            await _methodOfPaymentService.CreateMethodOfPayment(dto);
            return Ok();
        }
        [HttpDelete("{methodOfPaymentId}")]
        public async Task<ActionResult> DeleteMethodOfPayment([FromRoute] int methodOfPaymentId)
        {
            await _methodOfPaymentService.DeleteMethodOfPayment(methodOfPaymentId);
            return Ok();
        }

    }
}
