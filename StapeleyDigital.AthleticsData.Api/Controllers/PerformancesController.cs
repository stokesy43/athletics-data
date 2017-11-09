using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StapeleyDigital.AthleticsData.Data;
using StapeleyDigital.AthelticsData.Domain;
using StapeleyDigital.AthleticsData.Dto;

namespace StapeleyDigital.AthleticsData.Api.Controllers
{
    [Route("api/performances")]
    [Produces("application/json")]
    public class PerformancesController : Controller
    {
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IStandardRepository _standardRepository;
        private readonly IAthleteRepository _athleteRepository;
        private readonly ILogger<PerformancesController> _logger;
        private readonly IMapper _mapper;

        public PerformancesController(IPerformanceRepository performanceRepository,
            IMeetingRepository meetingRepository,
            IEventRepository eventRepository,
            IStandardRepository standardRepository,
            IAthleteRepository athleteRepository,
            ILogger<PerformancesController> logger,
            IMapper mapper)
        {
            _performanceRepository = performanceRepository;
            _meetingRepository = meetingRepository;
            _eventRepository = eventRepository;
            _standardRepository = standardRepository;
            _athleteRepository = athleteRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets performances
        /// </summary>
        /// <remarks>
        /// <![CDATA[
        /// Sample Request:
        /// 
        ///     GET /performances
        /// 
        /// ]]>
        /// 
        /// </remarks>
        /// <returns>A collection of performances</returns>
        /// <response code="200">Returns the collection of performances</response>        
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<PerformanceDto>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetPerformances()
        {
            try
            {
                var performanceEntities = _performanceRepository.GetPerformances();
                var results = _mapper.Map<IEnumerable<PerformanceDto>>(performanceEntities);

                return Ok(results);


            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting athletes", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }

        }

        /// <summary>
        /// Gets a performance using the id.  
        /// </summary>
        /// <remarks>
        /// <![CDATA[
        /// Sample Request:
        /// 
        ///     GET /performances/1
        /// 
        /// ]]>
        /// 
        /// </remarks>
        /// <param name="id">The identifier for the performance</param>
        /// <returns></returns>
        /// <response code="200">Returns the performance</response>
        /// <response code="404">If the performance is not found</response>
        [HttpGet("{id}", Name = "GetPerformance")]
        [ProducesResponseType(typeof(PerformanceDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPerformance(int id)
        {
            try
            {
                var performanceEntity = _performanceRepository.GetPerformance(id);

                if (performanceEntity == null)
                {
                    return NotFound();
                }

                var performanceResult = _mapper.Map<PerformanceDto>(performanceEntity);

                return Ok(performanceResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting performance with identifier {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        /// <summary>
        /// Adds a performance.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     
        ///     POST /performances
        ///     {
        ///     	"date": "13-Aug-2017",
        ///     	"athleteId": 1,
        ///     	"venue": "Warrington",
        ///     	"meetingId": "188893",
        ///     	"meetingName": "Warrington Open Series",
        ///     	"event": "75",
        ///     	"performanceValue": "12.6",
        ///     	"position": "2",
        ///     	"age", 9
        ///     	}
        /// </remarks>
        /// <param name="performance"></param>
        /// <returns>A newly-created performance object</returns>
        /// <response code="201">Returns the newly-crearted performance object</response>
        /// <response code="400">If the performance was incorrectly formed or failed validation.</response>
        /// <response code="404">If the athlete could not be found.</response>
        /// <response code="501">If the event for the performance is not support.</response>
        /// 
        [HttpPost()]
        [ProducesResponseType(typeof(PerformanceDto), 201)]
        [ProducesResponseType(501)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreatePerformance([FromBody] PerformanceForCreationDto performance)
        {
            try
            {
                // Check the input has been passed in
                if (performance == null)
                {
                    return BadRequest();
                }

                // Validate using the fluid validation rules
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                // Check if the event exists
                var eventEntity = _eventRepository.GetEvent(performance.Event);

                if (eventEntity == null)
                {
                    return StatusCode(501, "Event Not Supported");
                }

                // check if the athlete exists 
                var athleteEntity = _athleteRepository.GetAthlete(performance.AthleteId);

                if (athleteEntity== null)
                {
                    return NotFound();
                }

                // Check if the meeting exists already
                var meeting = _meetingRepository.GetMeeting(performance.MeetingId);

                if (meeting == null)
                {
                    // Create the meeting
                    meeting = new Meeting()
                    {
                        Name = performance.MeetingName,
                        PowerOf10Id = performance.MeetingId
                    };

                    _meetingRepository.AddMeeting(meeting);

                    if (!_meetingRepository.Save())
                    {
                        return StatusCode(500, "A problem happened while handling your request");
                    }
                }

                // Check if the performance exists already
                // combo of the event id and the meeting id
                if (_performanceRepository.PerformanceExists(meeting.Id, eventEntity.Id))
                {
                    return StatusCode(409, "Performance already registered with server");
                }

                // Map the dto to a domain entity
                var finalPerformance = _mapper.Map<Performance>(performance);
                finalPerformance.EventId = eventEntity.Id;
                finalPerformance.MeetingId = meeting.Id;

                var ageGroup = string.Empty;

                // from the age of the athlete at performance work out which age group they were in
                switch(performance.Age)
                {
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        ageGroup = "U11";
                        break;
                    case 11:
                    case 12:
                        ageGroup = "U13";
                        break;
                    case 13:
                    case 14:
                        ageGroup = "U15";
                        break;
                    case 15:
                    case 16:
                        ageGroup = "U17";
                        break;
                    case 17:
                    case 18:
                    case 19:
                        ageGroup = "U20";
                        break;
                    default:
                        ageGroup = "SEN";
                        break;
                }

                // Get all standards applicable for the athlete and performance
                var eventStandards = _standardRepository.GetStandards(performance.Date.Year, eventEntity.Id, ageGroup, athleteEntity.Gender);

                // Try converting the performance into a double 
                if (double.TryParse(performance.PerformanceValue, out var result))
                {
                    var standardsMet = eventStandards.Where(x => x.Value <= result).Select(x => x.Standard).OrderBy(x => x.Priority);

                    if (standardsMet.Any())
                    {
                        finalPerformance.StandardId = standardsMet.First().Id;
                    }                    
                }
                
                // Create the performance in the repository
                _performanceRepository.AddPerformance(finalPerformance);

                if (!_performanceRepository.Save())
                {
                    return StatusCode(500, "A problem happened while handling your request");
                }

                var createdPerformanceToReturn = _mapper.Map<PerformanceDto>(finalPerformance);

                return CreatedAtRoute("GetPerformance", new { id = createdPerformanceToReturn.Id },
                    createdPerformanceToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while creating performance.", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }


    }
}
