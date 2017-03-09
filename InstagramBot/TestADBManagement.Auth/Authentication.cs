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
    public class Authentication
    {
        public async Task<UserModel> AuthRequest(string login, string password)
        {
            var result = "";
            UserModel obj;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://smmplus.cs23401.tmweb.ru/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(string.Format("api/auth?email={0}&pass={1}", login, password));
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                obj = new JavaScriptSerializer().Deserialize<UserModel>(result);
                
            }
            //if ((bool)obj.result)
            //{
            //    return new UserModel { Result = true, UserId = (string)obj.userId, UserName = (string)obj.userName };
            //}
            //else
            //{
            //    return new UserModel { Result = false };
            //}
            return obj;
        }
    }
    public class UserModel
    {
        public bool result { get; set; }
        public string error { get; set; }
        public string sessionKey { get; set; }
    }
}
