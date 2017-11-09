using System;
using System.Collections.Generic;
using System.Linq;
using StapeleyDigital.AthelticsData.Domain;
using Microsoft.EntityFrameworkCore;

namespace StapeleyDigital.AthleticsData.Data
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly AthleticsDataContext _context;

        public AthleteRepository(AthleticsDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Athlete> GetAthletes()
        {
            return _context.Athletes.OrderBy(a => a.Name).ToList();
        }

        public IEnumerable<Athlete> GetAthletesByDevice(string uniqueDeviceId)
        {
            return _context.DeviceAthletes.Where(x => x.Device.UniqueId == uniqueDeviceId).Select(x => x.Athlete);

            //return _context.Devices.Include(d => d.DeviceAthletes).ThenInclude(d => d.Athlete).FirstOrDefault(d => d.UniqueId == uniqueId);
            
        }

        public Athlete GetAthlete(int id)
        {
            return _context.Athletes.FirstOrDefault(a => a.Id == id);
        }

        public bool AthleteExists(int id)
        {
            return _context.Athletes.Any(a => a.Id == id);
        }
    }
}
