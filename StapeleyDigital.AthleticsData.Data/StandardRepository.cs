using System;
using System.Collections.Generic;
using System.Text;
using StapeleyDigital.AthelticsData.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StapeleyDigital.AthleticsData.Data
{
    public class StandardRepository : IStandardRepository
    {
        private readonly AthleticsDataContext _context;

        public StandardRepository(AthleticsDataContext context)
        {
            _context = context;
        }

        public Standard GetStandard(int id)
        {
            return _context.Standards.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<EventStandard> GetStandards(int year, int eventId, string ageGroup, string genderCode)
        {
            return _context.EventStandards
                .Include("Standard")
                .Where(x => x.Year == year 
                && x.EventId == eventId 
                && x.AgeGroup == ageGroup
                && x.Gender == genderCode);
        }

        public IEnumerable<Standard> GetStandards()
        {
            return _context.Standards.ToList();
        }
    }
}
