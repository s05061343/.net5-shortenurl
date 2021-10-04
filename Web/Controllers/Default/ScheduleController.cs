using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedule;
using Schedule.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Controllers.Default
{
    [ApiController]
    [Route("[controller]/default/[action]")]
    public class ScheduleController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly string _version = typeof(ScheduleController).Namespace.Split('.').LastOrDefault();
        public ScheduleController(ILogger<ScheduleController> logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        public IActionResult Query(
            [FromServices] QuartzHostedService _service)
        {
            List<JobMetadata> list = _service._allJobSchedules;
            return this.Ok(new
            {
                jobs = list.Select(p => new
                {
                    p.JobName,
                    p.CronExpression
                })
            });
        }

        [HttpPost]
        public IActionResult StartService(
            [FromServices] QuartzHostedService _service)
        {
            _service.StartAsync(_service.CancellationToken).Wait();
            return this.Ok();
        }

        [HttpPost]
        public IActionResult StopService(
            [FromServices] QuartzHostedService _service)
        {
            _service.StopAsync(_service.CancellationToken).Wait();
            return this.Ok();
        }

        public class AddRequest
        {
            [Required]
            public string jobName { get; set; }
            [Required]
            public string cronExpression { get; set; }
        }

        [HttpPost]
        public IActionResult Add(
            [FromServices] QuartzHostedService _service,
            [FromServices] DbContext _dbContext,
            [FromBody] AddRequest _request)
        {
            var job = new JobMetadata(typeof(CheckExpiredJob), _request.cronExpression, _request.jobName);
            _service._allJobSchedules.Add(job);

            return this.Ok();
        }
    }
}
