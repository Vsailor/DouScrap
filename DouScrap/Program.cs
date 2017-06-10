using System;

namespace DouScrap
{
    class Program
    {
        private const string TwitterUrl = "http://twitter.com";
        private const string LinkedInUrl = "https://www.linkedin.com";
        private const string FacebookUrl = "https://www.facebook.com";

        static void Main(string[] args)
        {
            Console.WriteLine(GetUserFromDou("ozhurakovskiy"));
            Console.WriteLine("********************");
            Console.WriteLine(GetUserFromDou("anton-demydenko"));
            Console.WriteLine("********************");
            Console.WriteLine(GetUserFromDou("CB"));
            Console.ReadKey();
        }

        private static User GetUserFromDou(string userId)
        {
            var reader = new DouHtmlReader();
            string html = reader.LoadProfileHtml(userId);
            if (string.IsNullOrEmpty(html))
            {
                return new User();
            }

            var parser = new DouHtmlParser();
            return new User
            {
                Name = parser.GetName(html),
                Position = parser.GetPosition(html),
                Location = parser.GetLocation(html),
                Skills = parser.GetSkills(html),
                FacebookProfileUrl = parser.GetProfileUrl(html, FacebookUrl),
                LinkedInProfileUrl = parser.GetProfileUrl(html, LinkedInUrl),
                TwitterProfileUrl = parser.GetProfileUrl(html, TwitterUrl)
            };
        }
    }
} 