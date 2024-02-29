using Hangfire;
using HangFireExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTestController : ControllerBase
    {
        private readonly IJobTestService _jobTestService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobTestController(IJobTestService jobTestService, IBackgroundJobClient backgroundJobClient)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpGet("/FireAndForgetJob")]
        public IActionResult CreateFireandForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet("/DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), TimeSpan.FromSeconds(60));
            return Ok();
        }


    }
}
