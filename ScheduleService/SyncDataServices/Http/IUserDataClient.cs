using ScheduleService.Dtos;

namespace ScheduleService.SyncDataServices.Http
{
    public interface IUserDataClient
    {
        Task<IEnumerable<UserReadDto>?> GetUsers(IEnumerable<int> ids);
        Task<IEnumerable<UserReadDto>?> GetEligibleUsers(IEnumerable<int> ids);
        Task<UserReadDto?> GetUser(int id);
    }
}
