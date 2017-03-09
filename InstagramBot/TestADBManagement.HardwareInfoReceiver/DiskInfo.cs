using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.HardwareInfoReceiver
{
    public class DiskInfo
    {
        public List<string> SerialNumber { get; set; }
        public List<string> Signature { get; set; }
        public List<string> TotalSectors { get; set; }


        public DiskInfo()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            var collection = searcher.Get();
            SerialNumber = new List<string>();
            Signature = new List<string>();
            TotalSectors = new List<string>();

            foreach(var obj in collection)
            {
                SerialNumber.Add((string)obj["SerialNumber"]);
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            collection = searcher.Get();

            foreach (var obj in collection)
            {
                Signature.Add(((uint)obj["Signature"]).ToString());
                TotalSectors.Add(((uint)obj["Signature"]).ToString());
            }
        }
    }
    public class Drive
    {
        public string SerialNumber { get; set; }
        public string Signature { get; set; }
        public string TotalSectors { get; set; }
    }
}
