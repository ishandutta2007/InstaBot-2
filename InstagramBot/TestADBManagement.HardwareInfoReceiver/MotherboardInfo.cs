using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.HardwareInfoReceiver
{
    public class MotherboardInfo
    {
        public string Manufacturer { get; set; }
        public string Product { get; set; }
        public string SerialNumber { get; set; }
        public MotherboardInfo()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            var collection = searcher.Get();

            foreach (ManagementObject obj in collection)
            {
                Manufacturer = (string)obj["Manufacturer"];
                Product = (string)obj["Product"];
                SerialNumber = (string)obj["SerialNumber"];
            }
        }
    }
}
