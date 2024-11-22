using System.ComponentModel.DataAnnotations;

namespace ScheduleService.Dtos
{
    public class UpdateScheduleCreateDto
    {
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
