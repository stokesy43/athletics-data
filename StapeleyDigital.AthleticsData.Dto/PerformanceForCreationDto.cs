using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StapeleyDigital.AthleticsData.Dto
{
    public class PerformanceForCreationDto
    {
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public int AthleteId { get; set; }
        public string MeetingId { get; set; }
        public string MeetingName { get; set; }
        public int Age { get; set; }
        public string Event { get; set; }
        public string PerformanceValue { get; set; }      
        public string Position { get; set; }
        public string Round { get; set; }
    }
}
