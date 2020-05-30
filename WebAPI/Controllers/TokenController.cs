using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homework1.Helpers;
using homework1.Models;
using Microsoft.AspNetCore.Mvc;

namespace homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtHelpers helper;
        public TokenController(JwtHelpers helper)
        {
            this.helper = helper;
        }

        // POST api/token
        [HttpPost("")]
        public IActionResult Post(LoginViewModel login)
        {
            if (ValidateLogin(login))
            {
                return Ok(new {
                    token = this.helper.GenerateToken(login.Username)
                });
            }
            else
            {
                return Forbid();
            }
        }

        private bool ValidateLogin(LoginViewModel login)
        {
            return true;
        }
    }
}