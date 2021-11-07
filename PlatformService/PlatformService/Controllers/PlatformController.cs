using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.DTOs;
using PlatformService.Models;
using System.Collections.Generic;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> Get()
        {
            var platforms = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public ActionResult<PlatformReadDto> GetById(int id)
        {
            var platform = _repository.GetById(id);

            if(platform == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> Post(PlatformCreateDto platformDto)
        {
            var platform = _mapper.Map<Platform>(platformDto);
            _repository.Create(platform);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            return CreatedAtRoute(nameof(GetById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}
