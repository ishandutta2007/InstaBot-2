using InstaSharp;
using InstaSharp.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.WpfUi.Models
{
    public class InstagramApiController
    {
        private static string clientId = "b68377a3d2794d2c8cc2a63a29209ada";
        private static string clientSecret = "aa455874beb24fffbb4b1b35baeaf887";
        private static string redirectUrl = "http://localhost/";

        private InstagramConfig config = new InstagramConfig(clientId, clientSecret, redirectUrl);
        private List<OAuth.Scope> scopes = new List<OAuth.Scope> { OAuth.Scope.Basic };

        public void Autheticate(string user, string password)
        {
            var auth = Instagram.AuthByCredentials(user, password, config, scopes);

            var users = new Users(config, auth);


        }
    }
}
