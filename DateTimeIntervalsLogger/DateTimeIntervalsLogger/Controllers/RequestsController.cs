using DateTimeIntervals.Dtos.Dtos;
using DateTimeIntervalsLogger.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DateTimeIntervalsLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestRepository _repo;

        public RequestsController(IRequestRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost]
        public IActionResult AddRequest([FromBody]RequestDto requestDto)
        {
            _repo.AddRequestData(requestDto);

            return StatusCode(201);
        }
    }
}