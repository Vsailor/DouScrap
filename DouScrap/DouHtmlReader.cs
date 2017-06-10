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

        // TODO Move cookies to configuration file
        private CookieContainer GetCookieContainer()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie("_ym_uid", "148450156163488382", "/", Host));
            cookieContainer.Add(new Cookie("topinfo-52", "1", "/", Host));
            cookieContainer.Add(new Cookie("max-header-247", "1", "/", Host));
            cookieContainer.Add(new Cookie("sessionid", "vs1zbioz60mry6xvneuyl67pg8bs8qcz", "/", Host));
            cookieContainer.Add(new Cookie("csrftoken", "gFur4zm5oItCdOgAy1a4TXNboRAI6VTJmf0fwuCOJxUM6cFkoFlSrerAyGpFHm95", "/", Host));
            cookieContainer.Add(new Cookie("_ga", "GA1.2.285289182.1484501560", "/", Host));
            cookieContainer.Add(new Cookie("_gid", "GA1.2.2085413531.1497033352", "/", Host));

            return cookieContainer;
        }
    }
}
