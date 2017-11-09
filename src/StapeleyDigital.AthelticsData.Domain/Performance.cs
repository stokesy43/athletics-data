using System;

namespace StapeleyDigital.AthelticsData.Domain
{
    public class Performance
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int EventId { get; set; }
        public int? StandardId { get; set; }
        public int MeetingId { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string PerformanceValue { get; set; }
        public string Position { get; set; }
        
        public virtual Athlete Athlete { get; set; }
        public virtual Event Event { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
