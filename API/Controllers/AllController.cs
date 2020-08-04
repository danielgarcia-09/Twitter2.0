using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllController : ControllerBase
    {
        private readonly ApiRepository _repository;
        public AllController(ApiRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{user}")]
        public async Task<ActionResult<AllDTO>> All(string user)
        {
            var all = await _repository.All(user);
            if(all == null)
            {
                return NotFound();
            }
            return all;
        }

        [HttpGet("amigos/{user}")]
        public async Task<ActionResult<List<AmigosDTO>>> Amigos(string user)
        {
            var all = await _repository.Amigos(user);
            if (all == null)
            {
                return NotFound();
            }
            return all;
        }

        [HttpPost]
        public async Task<ActionResult> Publicar(ApiPublicacionDTO data)
        {
            if (ModelState.IsValid)
            {
                if(await _repository.Publicar(data))
                {
                    return NoContent();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost("{amigo}")]
        public async Task<ActionResult> addAmigo(ApiAmigosDTO data)
        {
            if (ModelState.IsValid)
            {
                if (await _repository.addAmigo(data))
                {
                    return NoContent();
                }
                return BadRequest();
            }
            return BadRequest();
        }

       
    }
}