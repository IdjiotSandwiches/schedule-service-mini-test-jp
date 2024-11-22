namespace ScheduleService.Dtos
{
    public class UserScheduleReadDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string NIM { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
