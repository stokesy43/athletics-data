using StapeleyDigital.AthelticsData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StapeleyDigital.AthleticsData.Data
{
    public interface IPerformanceRepository
    {
        void AddPerformance(Performance performance);

        IEnumerable<Performance> GetPerformances();

        Performance GetPerformance(int id);

        IEnumerable<Performance> GetPerformancesByAthlete(int AthleteId);
        
        IEnumerable<Performance> GetPerformancesByEvent(int EventId);

        IEnumerable<Performance> GetPerformancesByStandard(int StandardId);

        bool PerformanceExists(string powerOf10MeetingId, string powerOf10EventId);

        bool PerformanceExists(int meetingId, int eventId);

        bool Save();
    }
}
