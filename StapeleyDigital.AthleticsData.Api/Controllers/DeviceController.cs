using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StapeleyDigital.AthelticsData.Domain;
using StapeleyDigital.AthleticsData.Data;
using StapeleyDigital.AthleticsData.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StapeleyDigital.AthleticsData.Api.Controllers
{
    [Route("api/devices")]
    [Produces("application/json")]
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ILogger<DevicesController> _logger;
        private readonly IMapper _mapper;

        public DevicesController(IDeviceRepository deviceRepository, ILogger<DevicesController> logger, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetDevices()
        {
            var deviceEntities = _deviceRepository.GetDevices();
            var results = Mapper.Map<IEnumerable<DeviceDto>>(deviceEntities);

            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetDevice")]
        public IActionResult GetDevice(int id)
        {
            var device = _deviceRepository.GetDevice(id);

            if (device == null)
            {
                return NotFound();
            }

            var deviceResult = Mapper.Map<DeviceDto>(device);

            return Ok(deviceResult);
        }

        [HttpGet("{uniqueid}")]
        public IActionResult GetDeviceByUniqueId(string uniqueId)
        {
            var device = _deviceRepository.GetDevice(uniqueId);

            if (device == null)
            {
                return NotFound();
            }

            var deviceResult = Mapper.Map<DeviceDto>(device);

            return Ok(deviceResult);
        }
        
        /// <summary>
        /// Registers a device.  
        /// If the device already existed then nothing happens
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     
        ///     POST /devices
        ///     {
        ///         deviceId : "c119ab07-b8ce-e311-93fa-005056823267",
        ///         deviceName : "iPhone 6"
        ///     }
        /// 
        /// </remarks>
        /// <param name="device"></param>
        /// <returns>A newly-created device record</returns>
        /// <response code="201">Returns the newly-crearted device record</response>
        /// <response code="400">If the device was incorrectly formed or failed validation.</response>
        [HttpPost()]
        [ProducesResponseType(typeof(DeviceForCreationDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult CreateDevice([FromBody] DeviceForCreationDto device)
        {
            try
            {
                // Check the input has been passed in
                if (device == null)
                {
                    return BadRequest();
                }

                // Validate using the fluid validation rules
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                
                if (_deviceRepository.DeviceExists(device.DeviceId))
                {
                    return StatusCode(409, "Device already registered with server");                    
                }

                // Map the dto to a domain entity
                var finalDevice = _mapper.Map<Device>(device);
                
                // Create the school record in the repository
                _deviceRepository.AddDevice(finalDevice);


                if (!_deviceRepository.Save())
                {
                    return StatusCode(500, "A problem happened while handling your request");
                }

                var createdDeviceToReturn = _mapper.Map<DeviceDto>(finalDevice);

                return CreatedAtRoute("GetDevice", new { id = createdDeviceToReturn.Id },
                    createdDeviceToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while creating device.", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

    }

}
