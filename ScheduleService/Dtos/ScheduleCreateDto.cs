using System.ComponentModel.DataAnnotations;

namespace ScheduleService.Dtos
{
    public class ScheduleCreateDto
    {
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}
