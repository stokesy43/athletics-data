using Microsoft.EntityFrameworkCore;
using StapeleyDigital.AthelticsData.Domain;

namespace StapeleyDigital.AthleticsData.Data
{
    public class AthleticsDataContext : DbContext
    {
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<DeviceAthlete> DeviceAthletes { get; set; }
        public DbSet<EventStandard> EventStandards { get; set; }

        public AthleticsDataContext(DbContextOptions<AthleticsDataContext> options) : base(options)
        {
         
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //    modelBuilder.Entity<DeviceAthlete>()
            //.HasKey(da => new { da.DeviceId, da.AthleteId });

            modelBuilder.Entity<DeviceAthlete>()
                .HasOne(da => da.Device)
                .WithMany(d => d.DeviceAthletes)
                .HasForeignKey(da => da.DeviceId);

            modelBuilder.Entity<DeviceAthlete>()
                .HasOne(da => da.Athlete)
                .WithMany(d => d.DeviceAthletes)
                .HasForeignKey(da => da.AthleteId);

        }
    }
}
