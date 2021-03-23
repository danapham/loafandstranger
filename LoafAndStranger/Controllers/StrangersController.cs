using LoafAndStranger.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger.Controllers
{
    [Route("api/Strangers")]
    [ApiController]
    public class StrangersController : ControllerBase
    {
        StrangersRepository _repo;

        public StrangersController(StrangersRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllStrangers()
        {
            return Ok(_repo.GetAll());
        }
    }
}
