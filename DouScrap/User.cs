namespace DouScrap
{
    public class User
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public string[] Skills { get; set; }

        public string FacebookProfileUrl { get; set; }

        public string TwitterProfileUrl { get; set; }

        public string LinkedInProfileUrl { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Name = {0}\nPosition = {1}\nLocation = {2}\nSkills = {3}\nFacebookProfileUrl = {4}\nTwitterProfileUrl = {5}\nLinkedInProfileUrl = {6}",
                Name,
                Position,
                Location,
                string.Join(", ", Skills),
                FacebookProfileUrl,
                TwitterProfileUrl,
                LinkedInProfileUrl);
        }
    }
}