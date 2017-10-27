using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace VKPersonLogger
{
    /// <summary>
    /// Useful tools for work.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Timestamp as unix-time.
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTime()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Timestamp as normal form.
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString();
        }

        /// <summary>
        /// Get int-value from user.
        /// </summary>
        /// <param name="name">Variable identificator</param>
        public static int GetInt(string name)
        {
            int result;
            Console.Write($"Enter [{name}]: ");
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Incorrect! Try again: ");
            }
            return result;
        }

        /// <summary>
        /// Typical get request.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Return responce from server as HTML/JSON/ETC.</returns>
        public static string TypicalGetRequest(string url)
        {
            HttpRequest Request = new HttpRequest();
            Request.UserAgent = Http.FirefoxUserAgent();
            HttpResponse Responce = Request.Get(url);
            string result = Responce.ToString();
            Request.Close();
            Request.Dispose();

            return result;
        }

        public static string GetToken()
        {
            Process.Start("https://oauth.vk.com/authorize?client_id=5881849&scope=65536&redirect_uri=http://api.vk.com/blank.html&response_type=token&display=wap");
            Console.WriteLine("Enter your access token here:");
            return Console.ReadLine();
        }
    }
}
