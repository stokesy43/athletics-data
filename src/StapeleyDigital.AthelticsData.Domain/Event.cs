using System;
using System.Collections.Generic;

namespace StapeleyDigital.AthelticsData.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string PowerOf10Id { get; set; }

        public ICollection<EventStandard> EventStandards { get; set; }

    }
}
