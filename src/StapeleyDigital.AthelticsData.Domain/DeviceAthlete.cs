namespace StapeleyDigital.AthelticsData.Domain
{
    public class DeviceAthlete
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int DeviceId { get; set; }
        public bool Notifications { get; set; }

        public Athlete Athlete { get; set; }
        public Device Device { get; set; }
    }
}
