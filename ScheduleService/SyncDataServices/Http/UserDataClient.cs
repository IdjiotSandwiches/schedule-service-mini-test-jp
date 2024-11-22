using System.Net.Http.Json;
using ScheduleService.Dtos;

namespace ScheduleService.SyncDataServices.Http
{
    public class UserDataClient(HttpClient httpClient, IConfiguration configuration) : IUserDataClient
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;

        public async Task<UserReadDto?> GetUser(int id)
        {
            var response = await _httpClient.GetAsync($"/api/user/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserReadDto>();
        }

        public async Task<IEnumerable<UserReadDto>?> GetUsers(IEnumerable<int> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/user/find-users", ids);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<UserReadDto>>();
        }

        public async Task<IEnumerable<UserReadDto>?> GetEligibleUsers(IEnumerable<int> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/user/eligible-students", ids);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<UserReadDto>>();
        }
    }
}
