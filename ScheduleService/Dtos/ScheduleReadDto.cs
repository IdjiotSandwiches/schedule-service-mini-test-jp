using System.ComponentModel.DataAnnotations;

namespace ScheduleService.Dtos
{
    public class ScheduleReadDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int StudentId { get; set; }
    }
}
