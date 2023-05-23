using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using movie.Dto;
using movie.Interfaces;
using movie.Models;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private IActorRepository _actorRepository;
        private IMapper _mapper;

        public ActorController(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        [HttpGet] 
        public IActionResult GetActors() {
            var actors = _actorRepository.GetActors();
            var actorMapper = _mapper.Map<List<ActorDto>>(actors);
            return Ok(actorMapper);
        }

        [HttpGet("{id}")]
        public IActionResult GetActor(int id)
        {
            var actor = _actorRepository.GetActors().SingleOrDefault(a => a.Id == id);
            return Ok(actor);
        }
        [HttpPost] 
        public IActionResult CreateActor([FromBody] ActorDto actorDto)
        {
            var actorMapper = _mapper.Map<Actor>(actorDto);
            _actorRepository.CreateActor(actorMapper);
            return Ok(actorMapper);
        }

        [HttpDelete]
        public IActionResult DeleteActor(int id)
        {
           
            _actorRepository.DeleteActor(id);
            return Ok("ok");

        }

    }

    
}
