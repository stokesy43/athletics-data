using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StapeleyDigital.AthleticsData.Data;
using StapeleyDigital.AthleticsData.Dto;

namespace StapeleyDigital.AthleticsData.Api.Controllers
{
    [Route("api/athletes")]
    [Produces("application/json")]
    public class AthletesController : Controller
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<AthletesController> _logger;
        private readonly IMapper _mapper;

        public AthletesController(IAthleteRepository athleteRepository,
            IDeviceRepository deviceRepository,
            IPerformanceRepository performanceRepository,
            IEventRepository eventRepository,
            ILogger<AthletesController> logger,
            IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _deviceRepository = deviceRepository;
            _performanceRepository = performanceRepository;
            _eventRepository = eventRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets athletes
        /// </summary>
        /// <remarks>
        /// <![CDATA[
        /// Sample Request:
        /// 
        ///     GET /athletes?uniqueDeviceId=abc123
        /// 
        /// ]]>
        /// 
        /// </remarks>
        /// <returns>A collection of athletes</returns>
        /// <response code="200">Returns the collection of athletes</response>
        /// <response code="400">If the device id supplied was not found</response>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<AthleteDto>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetAthletes(string uniqueDeviceId = null)
        {
            try
            {
                if (uniqueDeviceId != null)
                {
                    // Validate the device exists 
                    if (!_deviceRepository.DeviceExists(uniqueDeviceId))
                    {
                        return NotFound();
                    }

                    var athleteEntities = _athleteRepository.GetAthletesByDevice(uniqueDeviceId);
                    var results = _mapper.Map<IEnumerable<AthleteDto>>(athleteEntities);

                    return Ok(results);
                }
                else
                {
                    var athleteEntities = _athleteRepository.GetAthletes();
                    var results = _mapper.Map<IEnumerable<AthleteDto>>(athleteEntities);

                    return Ok(results);
                }


            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting athletes", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }

        }

        /// <summary>
        /// Gets an athlete using the id.  
        /// </summary>
        /// <remarks>
        /// <![CDATA[
        /// Sample Request:
        /// 
        ///     GET /athletes/1
        /// 
        /// ]]>
        /// 
        /// </remarks>
        /// <param name="id">The identifier for the athlete</param>
        /// <returns></returns>
        /// <response code="200">Returns the athlete</response>
        /// <response code="404">If the athlete is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AthleteDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetAthlete(int id)
        {
            try
            {
                var athleteEntity = _athleteRepository.GetAthlete(id);

                if (athleteEntity == null)
                {
                    return NotFound();
                }

                var athleteResult = _mapper.Map<AthleteDto>(athleteEntity);

                return Ok(athleteResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting athlete with identifier {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        /// <summary>
        /// Gets performances for an athlete
        /// </summary>
        /// <remarks>
        /// <![CDATA[
        /// Sample Request:
        /// 
        ///     GET /athletes/1/performances
        /// 
        /// ]]>
        /// 
        /// </remarks>
        /// <param name="id">The Identifier for the athlete</param>
        /// <param name="eventId">Optional Event Filter</param>
        /// <returns>A collection of performances</returns>
        /// <response code="200">Returns the collection of performances</response>
        /// <response code="404">The athlete could not be found</response>
        [HttpGet("{id:int}/performances")]
        [ProducesResponseType(typeof(IEnumerable<PerformanceDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPerformancesByAthlete(int id, int? eventId = null)
        {
            try
            {
                var athleteEntity = _athleteRepository.GetAthlete(id);

                if (athleteEntity == null)
                {
                    return NotFound();
                }

                var performanceEntities = _performanceRepository.GetPerformancesByAthlete(id);

                if (eventId.HasValue)
                {
                    var eventEntity = _eventRepository.GetEvent(eventId.Value);

                    if (eventEntity==null)
                    {
                        return NotFound($"Event {eventId} does not exist");
                    }

                    performanceEntities = performanceEntities.Where(x => x.EventId == eventId.Value);
                }

                var results = _mapper.Map<IEnumerable<PerformanceDto>>(performanceEntities);

                return Ok(results);


            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting performances for athlete {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }

        }

    }
}
