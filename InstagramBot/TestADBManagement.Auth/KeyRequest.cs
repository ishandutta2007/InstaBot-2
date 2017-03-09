using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TestADBManagement.Auth
{
    public class KeyRequest
    {
        public async Task<KeyResponseModel> Request(KeyRequestModel reqData)
        {
            var result = "";
            var obj = new KeyResponseModel();
            var json = new JavaScriptSerializer().Serialize(reqData);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://smmplus.cs23401.tmweb.ru/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/get_new_key", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                obj = new JavaScriptSerializer().Deserialize<KeyResponseModel>(result);

            }
            return obj;
        }
    }

    public class KeyResponseModel
    {
        public bool result { get; set; }
        public string error { get; set; }
    }

    public class KeyRequestModel
    {
        public string programHash { get; set; }
        public string sessionKey { get; set; }
    }
}
