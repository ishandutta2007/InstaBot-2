//using Newtonsoft.Json;
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
    public class ActivateKey
    {
        private string licenseKey { get; set; }
        public ActivateKey(string key = null)
        {
            licenseKey = key;
        }

        public async Task<ActivationKeyResponseModel> Activate(string key = null)
        {
            ActivationKeyResponseModel obj;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://smmplus.cs23401.tmweb.ru/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(string.Format("api/activate_key?programHash={0}&programKey={1}", "", key));

                var result = "";
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                obj = new JavaScriptSerializer().Deserialize<ActivationKeyResponseModel>(result);
            }
            return obj;
        }
    }

    public class ActivationKeyResponseModel
    {
        public bool result { get; set; }
        public string userId { get; set; }
        public string error { get; set; }
    }

}
