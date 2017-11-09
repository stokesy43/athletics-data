using System.Collections.Generic;

namespace StapeleyDigital.AthelticsData.Domain
{
    public class Standard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int Priority { get; set; }

        public ICollection<EventStandard> EventStandards { get; set; }
    }
}
