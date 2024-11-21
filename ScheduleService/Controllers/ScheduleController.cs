using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Helpers;

namespace ScheduleService.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        public ScheduleController(ScheduleHelper _scheduleHelper)
        {
            
        }
    }
}
