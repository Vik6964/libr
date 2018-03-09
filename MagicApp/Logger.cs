using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;

namespace MagicApp
{
    /// <summary>Authenth class.</summary>
    /// 
    public class AuthToken
    {
        public string token;
    }

    public class Logger
    {
//public string MainUrl { get; }
        private string _suf = "/api/auth/logon";
        private string _mainUrl;

        /// <summary>
        /// Содержит параметры подключения. Default - демо.стенд
        /// Возможные прараметры: string test или prod
        /// </summary>
        public void SetServer()
        {
            _mainUrl = "http://courier-demo.esphere.ru/";
        }

        public void SetServer(string serv)
        {
            _mainUrl = serv == "test" ? "https://courier-test.esphere.ru" : "https://courier-api.esphere.ru/";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Type FormUrlEnccodedContent IEnumerable(KEYVALYEPAIR{string, string})</returns>
        private FormUrlEncodedContent AuthContent()
        {
            string username;
            string password;
            Console.WriteLine("Логин => \n");
            username = Console.ReadLine();
            Console.WriteLine("Пароль => \n");
            password = Console.ReadLine();
            var list = new Dictionary<string, string>
            {
                {"username", username},
                {"password", password}
            };
            var content = new FormUrlEncodedContent(list);
            return content;
        }

        /// <summary>
        /// Метод, возвращающий токен, который в дальнейшем должен оказаться в хедере всех сообщений
        /// </summary>
        public async void GetToken()
        {
            HttpClient _loggerClient = new HttpClient();

            _loggerClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(
                    "application/json")); //Accept header ({"Content-Type":"application/json"})

            HttpResponseMessage responce = await _loggerClient.PostAsync(_mainUrl + _suf, AuthContent());
            byte[] result;


            using (Stream stream = await responce.Content.ReadAsStreamAsync())
            {
                result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int) stream.Length);
            }

            var str = System.Text.Encoding.Default.GetString(result);
            _loggerClient.DefaultRequestHeaders.Add("Auth-Token", str);
            Console.WriteLine("Токен => \n");

            AuthToken t = JsonConvert.DeserializeObject<AuthToken>(str);
            string token = t.token;
            Console.WriteLine(token);

            Console.ReadKey();
            Thread.Sleep(20000);
        }
    }
}