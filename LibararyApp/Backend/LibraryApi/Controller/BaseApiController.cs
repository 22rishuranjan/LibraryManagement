using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(ApiResponse<T> result)
        {
            if (result.Data == null && result.Status != 404) return Ok(result);//return NotFound();
            if (result.Success || result.Status == 404)
                return Ok(result);

            return BadRequest(result.Message);
        }
    }
}
