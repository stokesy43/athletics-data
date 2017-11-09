using System;
using System.Collections.Generic;
using System.Text;
using StapeleyDigital.AthelticsData.Domain;
using System.Linq;

namespace StapeleyDigital.AthleticsData.Data
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly AthleticsDataContext _context;

        public MeetingRepository(AthleticsDataContext context)
        {
            _context = context;
        }


        public void AddMeeting(Meeting meeting)
        {
            _context.Meetings.Add(meeting);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Meeting GetMeeting(int id)
        {
            return _context.Meetings.FirstOrDefault(x => x.Id == id);
        }

        public Meeting GetMeeting(string powerOf10MeetingId)
        {
            return _context.Meetings.FirstOrDefault(x => x.PowerOf10Id == powerOf10MeetingId);
        }

        public IEnumerable<Meeting> GetMeetings()
        {
            return _context.Meetings.ToList();
        }
    }
}
