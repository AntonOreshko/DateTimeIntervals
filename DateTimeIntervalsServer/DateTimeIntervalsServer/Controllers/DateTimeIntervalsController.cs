using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DateTimeIntervalsServer.Data.DomainModels;
using DateTimeIntervalsServer.Data.Dtos;
using DateTimeIntervalsServer.Data.Repositories;
using DateTimeIntervalsServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DateTimeIntervalsServer.Controllers
{
    [Authorize]
    [Route("api/intervals/{userid}/")]
    [ApiController]
    public class DateTimeIntervalsController : ControllerBase
    {
        private readonly IDateTimeIntervalsRepository _repo;

        private readonly IMapper _mapper;

        public DateTimeIntervalsController(IDateTimeIntervalsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetInterval")]
        public async Task<IActionResult> GetInterval(int id)
        {
            var intervalFromRepo = await _repo.GetInterval(id);

            var interval = _mapper.Map<DateTimeIntervalForReturnDto>(intervalFromRepo);

            return Ok(interval);
        }

        [HttpPost]
        public async Task<IActionResult> AddInterval(int userId,
            [FromBody] DateTimeIntervalForCreationDto intervalForCreation)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var interval = _mapper.Map<DateTimeInterval>(intervalForCreation);

            if (!DateTimeIntervalValidator.Validate(interval)) return BadRequest("Invalid interval");

            if (userFromRepo.Intervals == null) userFromRepo.Intervals = new List<DateTimeInterval>();

            userFromRepo.Intervals.Add(interval);

            if (await _repo.SaveAll())
            {
                var intervalForReturn = _mapper.Map<DateTimeIntervalForReturnDto>(interval);

                return CreatedAtRoute(nameof(GetInterval), new { id = interval.Id }, intervalForReturn);
            }

            return BadRequest("Couldn't add the interval");
        }

        [HttpGet, Route("all")]
        public async Task<IActionResult> GetIntervals(int userId)
        {
            var intervalsFromRepo = await _repo.GetIntervals(userId);

            var intervalsForReturn = _mapper.Map<IEnumerable<DateTimeIntervalForReturnDto>>(intervalsFromRepo);

            return Ok(intervalsForReturn);
        }

        [HttpPost, Route("intersection")]
        public async Task<IActionResult> GetIntervals(int userId,
            [FromBody] DateTimeIntervalForIntersectionDto intervalForIntersection)
        {
            var intervalsFromRepo = await _repo.GetIntervals(userId);

            var targetInterval = _mapper.Map<DateTimeInterval>(intervalForIntersection);

            if (!DateTimeIntervalValidator.Validate(targetInterval)) return BadRequest("Invalid interval");

            var result = IntersectionCreator.CreateIntersection(intervalsFromRepo, targetInterval);

            var intersection = _mapper.Map<IEnumerable<DateTimeIntervalForReturnDto>>(result);

            return Ok(intersection);
        }
    }
}