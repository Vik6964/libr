using System;
using System.Net;

namespace MagicApp
{
    class CourierApiToken
    {
        public static HttpWebRequest WebRequest { get; set; }
        public string header;

        public class Program
        {
            private static void Main(string[] args)
            {
                Logger MAIN = new Logger();
                MAIN.SetServer();
                MAIN.GetToken();
                Console.ReadKey();
            }
        }
    }
}





/*using System;
using System.IO;
using System.Net;
using System.Text;
using Atlassian.Jira;

namespace ConsoleApplication3
{
    public class ExampleProgram
    {
        public static string RunQuery(string argument = null, string data = null, string method = "GET")
        {
            try
            {
                //m_BaseUrl = query;
                HttpWebRequest newRequest = WebRequest.Create("https://jira.esphere.ru") as HttpWebRequest;
                newRequest.ContentType = "application/json";
                newRequest.Method = method;

                if (data != null)
                {
                    using (StreamWriter writer = new StreamWriter(newRequest.GetRequestStream()))
                    {
                        writer.Write(data);
                    }
                }

                //string base64Credentials = GetEncodedCredentials();
                newRequest.Headers.Add("Authorization", "Basic " + LoginClass());

                HttpWebResponse response = newRequest.GetResponse() as HttpWebResponse;

                string result = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }

                newRequest = null;
                response = null;

                return result;
            }
            catch (Exception)
            {
                //MessageBox.Show(@"There is a problem getting data from Jira :" + "\n\n" + query, "Jira Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // private static string GetEncodedCredentials()
         {
             string mergedCredentials = string.Format("{0}:{1}", m_Username, m_Password);
             byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
             return Convert.ToBase64String(byteCredentials);
         }
        protected static JiraCredentials LoginClass()
        {
            string _login = "";
            string _password = "";
            JiraCredentials _logOn = new JiraCredentials(_login, _password);
            return _logOn;
        }

        static void Main()
        {
            System.Console.WriteLine(RunQuery());
        }
    }
}*/
