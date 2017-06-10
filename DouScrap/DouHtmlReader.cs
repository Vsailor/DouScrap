using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace DouScrap
{
    public class DouHtmlReader
    {
        private const string Host = "dou.ua";
        private const string UserProfileFormat = "https://{0}/users/{1}/";

        public string LoadProfileHtml(string userName)
        {
            var request = (HttpWebRequest) WebRequest.Create(string.Format(UserProfileFormat, Host, userName));
            request.Method = WebRequestMethods.Http.Get;
            request.CookieContainer = GetCookieContainer();
            WebResponse response = request.GetResponse();
            var encoding = Encoding.UTF8;
            string responseText;

            Stream responseStream = response.GetResponseStream();
            if (responseStream == null)
            {
                return string.Empty;
            }

            using (var reader = new StreamReader(responseStream, encoding))
            {
                responseText = reader.ReadToEnd();
            }

            return responseText;
        }

        private CookieContainer GetCookieContainer()
        {
            const string csrftokenName = "csrftoken";
            const string sessionidName = "sessionid";

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie(csrftokenName, ConfigurationManager.AppSettings[csrftokenName], "/", Host));
            cookieContainer.Add(new Cookie(sessionidName, ConfigurationManager.AppSettings[sessionidName], "/", Host));

            return cookieContainer;
        }
    }
}
