using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Dtos;
using ScheduleService.Helpers;
using ScheduleService.Models;
using ScheduleService.SyncDataServices.Http;

namespace ScheduleService.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController(ScheduleHelper scheduleHelper, IMapper mapper) : ControllerBase
    {
        private readonly ScheduleHelper _scheduleHelper = scheduleHelper;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<ScheduleReadDto>> GetAllSchedules()
        {
            var schedules = await _scheduleHelper.GetAllSchedules();
            var userIds = schedules.Select(x => x.StudentId).ToList();
            var users = await _scheduleHelper.GetUserByIds(userIds);

            if (users == null)
            {
                return BadRequest();
            }

            var results = _scheduleHelper.MappingSchedules(schedules, users);

            return Ok(results);
        }

        [HttpPost("range-schedules")]
        public async Task<ActionResult<UserScheduleReadDto>> GetAllSchedulesByDateTime([FromBody] FilterRequestDto filter)
        {

            var schedules = await _scheduleHelper.GetAllSchedulesByDateTime(filter);
            var userIds = schedules.Select(x => x.StudentId).ToList();
            var users = await _scheduleHelper.GetUserByIds(userIds);

            if (users == null)
            {
                return BadRequest();
            }

            var results = _scheduleHelper.MappingSchedules(schedules, users);

            return Ok(results);
        }

        [HttpGet("{id}", Name = "GetScheduleById")]
        public async Task<ActionResult<ScheduleReadDto>> GetScheduleById(int id)
        {
            var schedule = await _scheduleHelper.GetScheduleById(id);

            if (schedule == null)
            {
                return NotFound();
            }

            var user = await _scheduleHelper.GetUserById(schedule.StudentId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = _scheduleHelper.MappingSchedule(schedule, user);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<UpdateScheduleCreateDto>> UpdateSchedule([FromBody] UpdateScheduleReadDto updatedSchedule)
        {
            var schedule = await _scheduleHelper.UpdateSchedule(updatedSchedule);

            if (schedule == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<UpdateScheduleCreateDto>(schedule));
        }

        [HttpGet("eligible-students")]
        public async Task<ActionResult<UserReadDto>> GetEligibleUsers()
        {
            var eligibleUsers = await _scheduleHelper.GetEligibleUsers();

            if (eligibleUsers == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(eligibleUsers));
        }

        [HttpPost("insert-schedule")]
        public async Task<ActionResult<ScheduleReadDto>> InsertSchedule([FromBody] ScheduleCreateDto schedule)
        {
            var scheduleModel = _mapper.Map<Schedule>(schedule);
            var userSchedule = await _scheduleHelper.InsertSchedule(scheduleModel);

            if (userSchedule == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<ScheduleReadDto>(userSchedule));
        }
    }
}
