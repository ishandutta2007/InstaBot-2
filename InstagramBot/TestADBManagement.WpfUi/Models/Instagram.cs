using InstaSharp;
using InstaSharp.Endpoints;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TestADBManagement.WpfUi.Models
{
    public class Instagram : IDisposable
    {
        private const string USER_AGENT =
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/45.0.2414.0 Safari/537.36";

        private HttpClientHandler m_Handler;
        private HttpClient m_Client;

        /// <summary>
        /// Аунтификация для InstaSharp по логину/паролю
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="config">конфиг InstaSharp</param>
        /// <param name="scopes">требуемые права</param>
        /// <returns>OAuthResponse, необходимый для дальшего использования InstaSharp</returns>
        public static OAuthResponse AuthByCredentials(string username, string password, InstagramConfig config, List<OAuth.Scope> scopes)
        {
            return AuthByCredentialsAsync(username, password, config, scopes).Result;
        }

        /// <summary>
        /// Аунтификация для InstaSharp по логину/паролю
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="config">конфиг InstaSharp</param>
        /// <param name="scopes">требуемые права</param>
        /// <returns>задача возвращаюая OAuthResponse, необходимый для дальшего использования InstaSharp</returns>
        public static async Task<OAuthResponse> AuthByCredentialsAsync(string username, string password, InstagramConfig config, List<OAuth.Scope> scopes)
        {
            using (var instagram = new Instagram())
            {
                if (await instagram.LoginAsync(username, password))
                {
                    return await instagram.GetOauthResponse(config, scopes);
                }
            }

            throw new Exception("Authentification error");
        }

        /// <summary>
        /// Составляет из токена OAuthResponse
        /// </summary>
        /// <param name="config">Конфиг InstaSharp</param>
        /// <param name="scopes">Список требуемых прав</param>
        /// <returns></returns>
        public async Task<OAuthResponse> GetOauthResponse(InstagramConfig config, List<OAuth.Scope> scopes)
        {
            var token = await GetAccessToken(config.ClientId, config.RedirectUri, BuildScopeForUri(scopes));

            var auth = new OAuthResponse();
            auth.AccessToken = token;
            auth.User = new User();

            var users = new Users(config, auth);
            var self = await users.GetSelf();
            auth.User = self.Data;

            return auth;
        }


        /// <summary>
        /// Составляет строку scope для url
        /// </summary>
        /// <param name="scopes">список требуемых прав.</param>
        /// <returns>список прав, разделенных плюсом.</returns>
        private static string BuildScopeForUri(List<OAuth.Scope> scopes)
        {
            var scope = new StringBuilder();

            foreach (var s in scopes)
            {
                if (scope.Length > 0)
                {
                    scope.Append("+");
                }
                scope.Append(s.ToString().ToLower());
            }

            return scope.ToString();
        }


        /// <summary>
        /// Получает OAuth-токен
        /// </summary>
        /// <param name="clientId">id oauth-клиента</param>
        /// <param name="redirectUri">ссылка-редирект</param>
        /// <param name="scope">требуемые права</param>
        /// <returns>токен доступа</returns>
        public async Task<string> GetAccessToken(string clientId, string redirectUri, string scope)
        {
            // составляем URI для запроса на oauth-авторизацию
            var requestUri = string.Format(
                "/oauth/authorize/?client_id={0}&redirect_uri={1}&scope={2}&response_type=token",
                WebUtility.UrlEncode(clientId),
                WebUtility.UrlEncode(redirectUri),
                WebUtility.UrlEncode(scope)
                );

            // Поля формы авторизации OAuth-запроса
            var fields = new Dictionary<string, string>()
            {
                { "csrfmiddlewaretoken", GetCSRFToken() },
                { "allow", "Authorize" }
            };

            // Требуется рефер
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Referrer = new Uri(m_Client.BaseAddress, requestUri);
            request.Content = new FormUrlEncodedContent(fields);

            // строка под фрагмент-часть URL(то, что находится после #)
            string tokenFragment;

            // Нам нужно временно отключить авторедиректы.
            // К сожалению, текущая реализация HttpClientHandler'а запрещает менять свойства после первого соединения
            // Поэтому приходится создавать новый хендлер и клиент с теми же свойствами.
            using (var temporaryHandler = new HttpClientHandler())
            {
                temporaryHandler.AllowAutoRedirect = false;
                temporaryHandler.CookieContainer = m_Handler.CookieContainer;

                using (var temporaryClient = new HttpClient(temporaryHandler))
                {
                    temporaryClient.BaseAddress = m_Client.BaseAddress;

                    using (var response = await temporaryClient.SendAsync(request))
                    {
                        tokenFragment = response.Headers.Location.Fragment;
                    }
                }
            }

            // возвращаем часть фрагмента после '='
            var position = tokenFragment.IndexOf('=');
            return tokenFragment.Substring(position + 1);
        }


        /// <summary>
        /// Http-клиент, который после аунтификации
        /// можно использовать для выполнения 
        /// различных действий на сайте .
        /// </summary>
        public HttpClient Client
        {
            get { return m_Client; }
        }

        public Instagram()
        {
            m_Handler = new HttpClientHandler();

            m_Client = new HttpClient(m_Handler);
            m_Client.BaseAddress = new Uri("https://instagram.com/");
            m_Client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);
        }

        public void Dispose()
        {
            m_Client.Dispose();
            m_Handler.Dispose();
        }

        /// <summary>
        /// Осуществляет вход в Instagram,
        /// аналогично использованию стандартной формы логина в браузере
        /// </summary>
        /// <param name="username">Логин либо e-mail</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public async Task<bool> LoginAsync(string username, string password)
        {
            // получаем страницу входа, что бы сайт установил Cookie 'csrftoken'
            // содержимое страницы нам не важно
            m_Client.GetAsync("/accounts/login/").Wait();

            // получаем токен из Cookies
            var csrftoken = GetCSRFToken();

            // готовим поля для формы входа
            var fields = new Dictionary<string, string>()
            {
                { "username", username },
                { "password", password }
            };

            // готовим запрос
            var request = new HttpRequestMessage(HttpMethod.Post, "/accounts/login/ajax/");
            request.Content = new FormUrlEncodedContent(fields);

            request.Headers.Referrer = new Uri(m_Client.BaseAddress, "/accounts/login/");

            // Дополнительные заголовки запроса.
            // Кроме X-CSRFToken, остальное в общем-то не обязательно.
            request.Headers.Add("X-CSRFToken", csrftoken);
            request.Headers.Add("X-Instagram-AJAX", "1");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");

            // Авторзуемся через AJAX
            using (var response = await m_Client.SendAsync(request))
            {

                var info = new JavaScriptSerializer().Deserialize<LoginInfo>(await response.Content.ReadAsStringAsync());
                return info.authenticated;
            }

        }
        private string GetCSRFToken()
        {
            var cookies = m_Handler.CookieContainer.GetCookies(m_Client.BaseAddress);
            return cookies["csrftoken"].Value;
        }

        private class LoginInfo
        {
            public string status { get; set; }
            public bool authenticated { get; set; }
        }
    }
}
