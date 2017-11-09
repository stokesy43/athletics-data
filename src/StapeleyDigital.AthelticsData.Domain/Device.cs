using System.Collections.Generic;

namespace StapeleyDigital.AthelticsData.Domain
{
    public class Device
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string BundleId { get; set; }
        public string BuildNumber { get; set; }
        public string AppVersion { get; set; }
        public string AppVersionReadable { get; set; }
        public bool Notifications { get; set; }

        public ICollection<DeviceAthlete> DeviceAthletes { get; set; }
    }
}
