using System;
using System.Collections.Generic;
using System.Text;
using StapeleyDigital.AthelticsData.Domain;

namespace StapeleyDigital.AthleticsData.Data
{
    public interface IAthleteRepository
    {
        IEnumerable<Athlete> GetAthletes();

        IEnumerable<Athlete> GetAthletesByDevice(string uniqueDeviceId);

        Athlete GetAthlete(int id);

        bool AthleteExists(int id);
    }
}
