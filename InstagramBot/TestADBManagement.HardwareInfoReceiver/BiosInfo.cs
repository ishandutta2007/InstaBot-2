using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.HardwareInfoReceiver
{
    public class BiosInfo
    {
        public string Manufacturer { get; set; }
        public string IdentificationCode { get; set; }
        public string SerialNumber { get; set; }
        public string ReleaseDate { get; set; }
        public string Version { get; set; }

        public BiosInfo()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            var collection = searcher.Get();

            foreach (ManagementObject obj in collection)
            {
                Manufacturer = (string)obj["Manufacturer"];
                IdentificationCode = (string)obj["IdentificationCode"];
                SerialNumber = (string)obj["SerialNumber"];
                ReleaseDate = (string)obj["ReleaseDate"];
                Version = (string)obj["Version"];
            }
        }
    }
}

/*Console.WriteLine(item.SerialNumber);
                Console.WriteLine(item.ReleaseDate);
                Console.WriteLine(item.Version);*/
