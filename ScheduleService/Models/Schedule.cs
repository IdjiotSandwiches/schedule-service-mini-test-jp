using System.ComponentModel.DataAnnotations;

namespace ScheduleService.Models
{
    public class Schedule
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
