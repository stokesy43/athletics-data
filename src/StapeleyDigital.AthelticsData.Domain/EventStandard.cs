namespace StapeleyDigital.AthelticsData.Domain
{
    public class EventStandard
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int StandardId { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
        public double Value { get; set; }
        public string AgeGroup { get; set; }

        public Event Event { get; set; }
        public Standard Standard { get; set; }
    }
}
