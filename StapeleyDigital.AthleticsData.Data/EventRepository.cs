using System;
using System.Collections.Generic;
using System.Text;
using StapeleyDigital.AthelticsData.Domain;
using System.Linq;

namespace StapeleyDigital.AthleticsData.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly AthleticsDataContext _context;

        public EventRepository(AthleticsDataContext context)
        {
            _context = context;
        }

        public Event GetEvent(int id)
        {
            return _context.Events.FirstOrDefault(x => x.Id == id);
        }

        public Event GetEvent(string powerOf10EventId)
        {
            return _context.Events.FirstOrDefault(x => x.PowerOf10Id == powerOf10EventId);
        }

        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.ToList();
        }
    }
}
