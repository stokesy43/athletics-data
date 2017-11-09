using StapeleyDigital.AthelticsData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StapeleyDigital.AthleticsData.Data
{
    public interface IMeetingRepository
    {
        Meeting GetMeeting(int id);

        IEnumerable<Meeting> GetMeetings();

        Meeting GetMeeting(string powerOf10MeetingId);

        void AddMeeting(Meeting meeting);

        bool Save();
        
    }
}
