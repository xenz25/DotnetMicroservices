using Microsoft.AspNetCore.Mvc;
using PlatformService.Services;
using AutoMapper;
using System.Collections.Generic;
using PlatformService.Models;
using PlatformService.Dtos;
using Microsoft.AspNetCore.Http;
using PlatformService.SyncDataServices.Http;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private ICommandDataClient _commandClient;
        private readonly ILogger _logger;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandClient, ILogger<PlatformsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _commandClient = commandClient;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public ActionResult<IEnumerable<Platforms>> GetPlatforms()
        {
            return Ok(_repository.GetAllPlatform());
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public IActionResult GetPlatform([FromRoute] int id)
        {
            var platform = _repository.GetPlatformById(id);

            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> CreatePlatform([FromBody] PlatformCreateDto platform)
        {
            if (platform == null)
            {
                return BadRequest();
            }

            var platformInternal = _mapper.Map<Platforms>(platform);

            // _repository.CreatePlatform(platformInternal);
            // var result = _repository.SaveChages();

            var platformRead = _mapper.Map<PlatformReadDto>(platformInternal);

            try
            {
                if (true)
                {
                    var response = await _commandClient.SendPlatformToCommand(platformRead);
                    if (!response) throw new Exception();
                    return CreatedAtAction(nameof(GetPlatform), new { id = platformRead.Id }, platformRead);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Conflict();
        }

        // [HttpPost]
        // [Route("test")]
        // public IActionResult TestPaltform([FromBody] PlatformReadDto platform)
        // {
        //     _commandClient.SendPlatformToCommand(platform);
        //     return Ok();
        // }
    }
}