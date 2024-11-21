using AutoMapper;
using ScheduleService.Dtos;
using ScheduleService.Models;

namespace ScheduleService.Profiles
{
    public class SchedulesProfile : Profile
    {
        public SchedulesProfile()
        {
            CreateMap<Schedule, ScheduleReadDto>();
            CreateMap<ScheduleCreateDto, Schedule>();
        }
    }
}
