using StapeleyDigital.AthelticsData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StapeleyDigital.AthleticsData.Data
{
    public interface IEventRepository
    {
        Event GetEvent(int id);

        IEnumerable<Event> GetEvents();

        Event GetEvent(string powerOf10EventId);
                
    }
}
