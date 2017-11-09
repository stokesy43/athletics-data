using System;
using System.Collections.Generic;
using System.Text;
using StapeleyDigital.AthelticsData.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StapeleyDigital.AthleticsData.Data
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AthleticsDataContext _context;

        public DeviceRepository(AthleticsDataContext context)
        {
            _context = context;
        }

        public void AddDevice(Device device)
        {
            _context.Devices.Add(device);
        }

        public bool DeviceExists(int id)
        {
            return _context.Devices.Any(d => d.Id == id);
        }

        public bool DeviceExists(string uniqueDeviceId)
        {
            return _context.Devices.Any(d => d.UniqueId == uniqueDeviceId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
        

        public Device GetDevice(int id)
        {
            return _context.Devices.Include(d => d.DeviceAthletes).ThenInclude(d => d.Athlete).FirstOrDefault(d => d.Id == id);
        }

        public Device GetDevice(string uniqueId)
        {
            return _context.Devices.Include(d => d.DeviceAthletes).ThenInclude(d => d.Athlete).FirstOrDefault(d => d.UniqueId == uniqueId);            
        }

        public IEnumerable<Device> GetDevices()
        {
            return _context.Devices.ToList();
        }
        
    }
}
