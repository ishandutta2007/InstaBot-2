using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.HardwareInfoReceiver
{
    public class NetworkInfo
    {
        public string MacAddress { get; set; } = string.Empty;

        public NetworkInfo()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            var collection = searcher.Get();

            foreach (var item in collection)
            {
                MacAddress = (string)item["MacAddress"];
                
            }
        }
    }
}
