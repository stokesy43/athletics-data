using StapeleyDigital.AthelticsData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StapeleyDigital.AthleticsData.Data
{
    public interface IStandardRepository
    {
        Standard GetStandard(int id);

        IEnumerable<EventStandard> GetStandards(int year, int eventId, string ageGroup, string genderCode);

        IEnumerable<Standard> GetStandards();


                
    }
}
