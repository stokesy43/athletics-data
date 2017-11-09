using System;
using System.Collections.Generic;
using System.Text;
using StapeleyDigital.AthelticsData.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StapeleyDigital.AthleticsData.Data
{
    public class PerformanceRepository : IPerformanceRepository
    {
        private readonly AthleticsDataContext _context;

        public PerformanceRepository(AthleticsDataContext context)
        {
            _context = context;
        }

        public void AddPerformance(Performance performance)
        {
            _context.Performances.Add(performance);
        }

        public IEnumerable<Performance> GetPerformances() => _context.Performances
                .Include("Athlete")
                .Include("Event")
                .Include("Standard")
                .Include("Meeting")
                .ToList();

        public Performance GetPerformance(int id) => _context.Performances
                .Include("Athlete")
                .Include("Event")
                .Include("Standard")
                .Include("Meeting")
                .FirstOrDefault(x => x.Id == id);

        public IEnumerable<Performance> GetPerformancesByAthlete(int AthleteId) => _context.Performances
                .Include("Athlete")
                .Include("Event")
                .Include("Standard")
                .Include("Meeting")
                .Where(x => x.AthleteId == AthleteId);

        public IEnumerable<Performance> GetPerformancesByEvent(int EventId) => _context.Performances
                .Include("Athlete")
                .Include("Event")
                .Include("Standard")
                .Include("Meeting")
                .Where(x => x.EventId == EventId);

        public IEnumerable<Performance> GetPerformancesByStandard(int StandardId) => 
            _context.Performances
                .Include("Athlete")
                .Include("Event")
                .Include("Standard")
                .Include("Meeting")
                .Where(x => x.StandardId == StandardId);

        public bool PerformanceExists(string powerOf10MeetingId, string powerOf10EventId) => 
            _context.Performances.Any(x => x.Meeting.PowerOf10Id == powerOf10MeetingId && x.Event.PowerOf10Id == powerOf10EventId);

        public bool PerformanceExists(int meetingId, int eventId) => 
            _context.Performances.Any(x => x.MeetingId == meetingId && x.EventId == eventId);


        public bool Save() => (_context.SaveChanges() >= 0);
    }
}
