using HR.LeaveManagmant.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetAllLeaveTypes;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagmant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveTypeDto>>> Get()
        {
            return Ok(await _mediator.Send(new GetAllLeaveTypesQuary()));
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetLeaveTypeDetailsDto>> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetLeaveTypeDetailsQuary { Id = id}));
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLeaveTypeCommand command)
        {
            var CreatedId = await _mediator.Send(command);
            return Created(nameof(Get), $"LeaveType Created With Id {CreatedId}");
        }

        // PUT api/<LeaveTypesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateLeaveTypeCommand command)
        {
            command.Id = id;
            var LeaveType = await _mediator.Send(command);
            return Ok($"LeaveType Updated Sucessfully");
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var LeaveType = await _mediator.Send(new DeleteLeaveTypeCommand { Id = id});
            return Ok("LeaveType Deleted Sucessfully");
        }
    }
}
