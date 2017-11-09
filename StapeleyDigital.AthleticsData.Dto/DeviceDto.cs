using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StapeleyDigital.AthleticsData.Dto
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }

        public ICollection<AthleteDto> Athletes { get; set; }
        = new List<AthleteDto>();
    }
}
