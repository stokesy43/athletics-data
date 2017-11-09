using StapeleyDigital.AthelticsData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StapeleyDigital.AthleticsData.Data
{
    public interface IDeviceRepository
    {
        void AddDevice(Device device);

        IEnumerable<Device> GetDevices();

        Device GetDevice(int id);

        Device GetDevice(string uniqueId);

        bool DeviceExists(int id);

        bool DeviceExists(string uniqueDeviceId);

        bool Save();
    }
}
