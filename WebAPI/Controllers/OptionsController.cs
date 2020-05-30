using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homework1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly AppSettings settings;
        public OptionsController(IOptions<AppSettings> options)
        {
            this.settings = options.Value;
        }

        // GET api/options/5
        [HttpGet("{id}")]
        public ActionResult<AppSettings> GetAppSettings()
        {
            return this.settings;
        }
    }
}