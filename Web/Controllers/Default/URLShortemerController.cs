using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.ShortenUrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Request;

namespace Web.Controllers.Default
{
    [ApiController]
    [Route("[controller]/default/[action]")]
    public class URLShortemerController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly string _version = typeof(URLShortemerController).Namespace.Split('.').LastOrDefault();
        public URLShortemerController(ILogger<URLShortemerController> logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        public IActionResult Shorten(
            [FromServices] IShortenUrlManager _manager,
            [FromServices] IHttpContextAccessor contextAccessor,
            [FromBody] ShortenUrlRequest request)
        {
            var shorturl = _manager.Shorten(request.url);
            var result = $"https://{contextAccessor.HttpContext.Request.Host}/{shorturl}";
            return Ok(new
            {
                data = result
            });
        }
    }
}
