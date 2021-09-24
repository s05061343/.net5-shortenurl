using Microsoft.AspNetCore.Authorization;
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
    [Route("/{value?}")]
    public class RedirectController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly string _version = typeof(RedirectController).Namespace.Split('.').LastOrDefault();
        public RedirectController(ILogger<RedirectController> logger)
        {
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RedirectURL(
            [FromServices] IShortenUrlManager _manager,
            string value)
        {
            var url = _manager.Translate(value);
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(url)) 
            {
                return this.Ok();
            }
            else 
            {
                return Redirect(url);
            }
        }
    }
}
