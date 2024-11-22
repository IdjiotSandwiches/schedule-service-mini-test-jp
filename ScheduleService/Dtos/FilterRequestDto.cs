namespace ScheduleService.Dtos
{
    public class FilterRequestDto
    {
        public required string Date { get; set; }
        public required string StartTime { get; set; }
        public required string EndTime { get; set; }
    }
}
