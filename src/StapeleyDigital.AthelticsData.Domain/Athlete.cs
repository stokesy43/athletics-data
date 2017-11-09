using System.Collections.Generic;

namespace StapeleyDigital.AthelticsData.Domain
{
    public class Athlete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PowerOf10Id { get; set; }
        public string AgeGroup { get; set; }
        public string Gender { get; set; }
        
        public ICollection<Performance> Performances { get; set; }

        public ICollection<DeviceAthlete> DeviceAthletes { get; set; }
    }
}
