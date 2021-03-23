using LoafAndStranger.DataAccess;
using LoafAndStranger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger.Controllers
{
    [Route("api/Loaves")]
    [ApiController]
    public class LoavesController : ControllerBase
    {
        LoafRepository _repo;
        public LoavesController()
        {
            _repo = new LoafRepository();
        }

        //GET to /api/loaves
        [HttpGet]
        public IActionResult GetAllLoaves()
        {
            return Ok(_repo.GetAll());
        }

        //POST to /api/loaves
        [HttpPost]
        public IActionResult AddALoaf(Loaf loaf)
        {
            _repo.Add(loaf);
            return Created($"api/Loaves/{loaf.Id}", loaf);
        }

        //GET to /api/loaves/{id}
        //GET to /api/loaves/4
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var loaf = _repo.Get(id);

            if (loaf == null)
            {
                return NotFound("This loaf id does not exist");
            }

            return Ok(loaf);
        }

        //PUT to /api/loaves/{id}/slice
        [HttpPut("{id}/slice")]
        public IActionResult SliceLoaf(int id)
        {
            //option 1:
            //task based api with crud repo
            var loaf = _repo.Get(id);

            loaf.Sliced = true;

            _repo.Update(loaf);

            //option 2:
            //task based api with task based repo
            _repo.Slice(id);

            return NoContent();
        }

        //DELETE /api/loaves/{id}
        [HttpDelete("{id}")]
        public IActionResult PurchaseLoaf(int id)
        {
            _repo.Remove(id);

            return Ok();
        }
    }
}
