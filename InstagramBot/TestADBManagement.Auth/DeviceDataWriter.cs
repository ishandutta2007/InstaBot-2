//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TestADBManagement.HardwareInfoReceiver;

namespace TestADBManagement.Auth
{
    public class DeviceDataWriter
    {
        private BiosInfo biosInfo = new BiosInfo();
        private DiskInfo diskInfo = new DiskInfo();
        private MotherboardInfo motherboardInfo = new MotherboardInfo();
        private NetworkInfo networkInfo = new NetworkInfo();
        private string osVersion = Environment.OSVersion.ToString();

        public async Task<DeviceResponseModel> WriteData(string programHash, string sessionKey)
        {
            var result = "";
            DeviceResponseModel obj;

            var data = new DeviceRequest
            {
                programHash = programHash,
                sessionKey = sessionKey,
                macAddress = networkInfo.MacAddress,
                os = osVersion,
                id_mother_board = motherboardInfo.SerialNumber,
                id_HDD = diskInfo.SerialNumber.First(),
                otherInfo = OtherInfoBuilder()
            };

            var json = new JavaScriptSerializer().Serialize(data);
            

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://smmplus.cs23401.tmweb.ru/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/writeDeviceData", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                obj = new JavaScriptSerializer().Deserialize<DeviceResponseModel>(result);
            }
            return obj;
        }

        private string OtherInfoBuilder()
        {
            var otherInfo = biosInfo.IdentificationCode + "_" + biosInfo.Manufacturer + "_" + biosInfo.ReleaseDate + "_"
                + biosInfo.SerialNumber + "_" + biosInfo.Version + "_";
            foreach (var item in diskInfo.Signature)
            {
                otherInfo += item + "_";
            }
            foreach(var item in diskInfo.TotalSectors)
            {
                otherInfo += item + "_";
            }
            otherInfo += motherboardInfo.Manufacturer + "_" + motherboardInfo.Product;
            return otherInfo;
        }
    }

    public class DeviceResponseModel
    {
        public bool result { get; set; }
        public string error { get; set; }

    }
    public class DeviceRequest
    {
        public string programHash { get; set; }
        public string sessionKey { get; set; }
        public string macAddress { get; set; }
        public string os { get; set; }
        public string id_mother_board { get; set; }
        public string id_HDD { get; set; }
        public string otherInfo { get; set; }
    }
}
