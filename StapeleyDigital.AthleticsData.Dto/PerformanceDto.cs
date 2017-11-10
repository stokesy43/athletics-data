using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StapeleyDigital.AthleticsData.Dto
{
    public class PerformanceDto
    {
        public int Id { get; set; }        
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string AthleteName { get; set; }
        public string EventName { get; set; }
        public string StandardName { get; set; }
        public string MeetingName { get; set; }
        public string PerformanceValue { get; set; }
        public string Round { get; set; }
    }
}
