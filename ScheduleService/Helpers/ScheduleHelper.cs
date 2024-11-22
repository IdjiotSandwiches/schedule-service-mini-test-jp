using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleService.Controllers;
using ScheduleService.Data;
using ScheduleService.Dtos;
using ScheduleService.Models;
using ScheduleService.SyncDataServices.Http;

namespace ScheduleService.Helpers
{
    public class ScheduleHelper(AppDbContext dbContext, IUserDataClient userDataClient)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IUserDataClient _userDataClient = userDataClient;

        public async Task<IEnumerable<Schedule>> GetAllSchedules()
        {
            return await _dbContext.Schedules.ToListAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByDateTime(FilterRequestDto filter)
        {
            return await _dbContext.Schedules
                .Where(
                    x => 
                    x.Date == DateOnly.Parse(filter.Date) && 
                    x.StartTime == TimeOnly.Parse(filter.StartTime) &&
                    x.EndTime == TimeOnly.Parse(filter.EndTime))
                .ToListAsync();
        }

        public async Task<Schedule?> GetScheduleById(int id)
        {
            return await _dbContext.Schedules.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<UserReadDto>?> GetUserByIds(List<int> userIds)
        {
            IEnumerable<UserReadDto>? users = [];

            try
            {
                users = await _userDataClient.GetUsers(userIds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return users;
        }

        public IEnumerable<UserScheduleReadDto> MappingSchedules(IEnumerable<Schedule> schedules, IEnumerable<UserReadDto> users)
        {
            var map = schedules.Join(
                users,
                schedule => schedule.StudentId,
                user => user.Id,
                (schedule, user) => new UserScheduleReadDto
                {
                    Id = schedule.Id,
                    Name = user.Name,
                    NIM = user.NIM,
                    Date = schedule.Date,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                }
            );

            return map;
        }

        public async Task<UserReadDto?> GetUserById(int id)
        {
            UserReadDto? user = null;

            try
            {
                user = await _userDataClient.GetUser(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return user;
        }

        public UserScheduleReadDto MappingSchedule(Schedule schedule, UserReadDto user)
        {
            return new UserScheduleReadDto
            {
                Id = schedule.Id,
                Name = user.Name,
                NIM = user.NIM,
                Date = schedule.Date,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
            };
        }

        public async Task<UpdateScheduleCreateDto> UpdateSchedule(UpdateScheduleReadDto updatedSchedule)
        {
            var schedule = await _dbContext.Schedules.FirstAsync(x => x.Id == updatedSchedule.Id);

            schedule.Date = updatedSchedule.Date;
            schedule.StartTime = updatedSchedule.StartTime;
            schedule.EndTime = updatedSchedule.EndTime;

            await _dbContext.SaveChangesAsync();

            return new UpdateScheduleCreateDto
            {
                Date = updatedSchedule.Date,
                StartTime = updatedSchedule.StartTime,
                EndTime = updatedSchedule.EndTime,
            };
        }

        public async Task<IEnumerable<UserReadDto>> GetEligibleUsers()
        {
            var studentIds = await _dbContext.Schedules.Select(x => x.StudentId).ToListAsync();
            IEnumerable<UserReadDto>? users = [];

            try
            {
                users = await _userDataClient.GetEligibleUsers(studentIds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return users;
        }

        public async Task<Schedule?> InsertSchedule(Schedule schedule)
        {
            if (_dbContext.Schedules.Any(x => x.StudentId == schedule.StudentId))
            {
                return null;
            }
            await _dbContext.Schedules.AddAsync(schedule);
            await _dbContext.SaveChangesAsync();

            return schedule;
        }
    }
}
