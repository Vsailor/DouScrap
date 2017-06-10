using System;
using System.Linq;

namespace DouScrap
{
    public class DouHtmlParser
    {
        public string GetName(string html)
        {
            const string h1Tag = "<h1>";
            const string divTag = "<div class=\"head\">";
            const string aTag = "<a href";

            int indexOfDiv = html.IndexOf(divTag, StringComparison.InvariantCultureIgnoreCase);

            if (indexOfDiv == -1)
            {
                return string.Empty;
            }

            int indexOfH1 = html.IndexOf(
                h1Tag, 
                indexOfDiv, 
                StringComparison.InvariantCultureIgnoreCase) + h1Tag.Length;

            int indexOfHref = html.IndexOf(
                aTag, 
                indexOfH1, 
                StringComparison.InvariantCultureIgnoreCase);

            string name = html.Substring(indexOfH1, indexOfHref - indexOfH1);
            return RemoveRedundantTabs(name);
        }

        public string GetPosition(string html)
        {
            const string divTag = "<div class=\"descr\">";
            const string closedDiv = "</div>";

            int indexOfDiv = html.IndexOf(
                divTag, 
                StringComparison.InvariantCultureIgnoreCase) + divTag.Length;

            if (indexOfDiv == -1)
            {
                return string.Empty;
            }

            int indexOfClosedDiv = html.IndexOf(
                closedDiv,
                indexOfDiv,
                StringComparison.InvariantCultureIgnoreCase);

            return RemoveRedundantPositionUrl(html.Substring(indexOfDiv, indexOfClosedDiv - indexOfDiv));
        }

        public string GetLocation(string html)
        {
            const string divTag = "<div class=\"city\">";
            const string closedDiv = "</div>";
            const string closedEm = "</em>";

            int indexOfDiv = html.IndexOf(
                divTag,
                StringComparison.InvariantCultureIgnoreCase);

            if (indexOfDiv == -1)
            {
                return string.Empty;
            }

            int indexOfEm = html.IndexOf(
                closedEm,
                indexOfDiv,
                StringComparison.InvariantCultureIgnoreCase) + closedEm.Length;

            int indexOfClosedDiv = html.IndexOf(
                closedDiv,
                indexOfEm,
                StringComparison.InvariantCultureIgnoreCase);

            string location = html.Substring(indexOfEm, indexOfClosedDiv - indexOfEm);
            return RemoveRedundantTabs(location);
        }

        public string[] GetSkills(string html)
        {
            const string divTag = "<div class=\"user_skills\">";
            const string openedParagraphTag = "<p>";
            const string closedParagraphTag = "</p>";

            int indexOfDiv = html.IndexOf(
               divTag,
               StringComparison.InvariantCultureIgnoreCase);

            if (indexOfDiv == -1)
            {
                return new string[0];
            }

            int indexOfOpenedPTag = html.IndexOf(
                openedParagraphTag,
                indexOfDiv,
                StringComparison.InvariantCultureIgnoreCase) + openedParagraphTag.Length;

            int indexOfClosedPTag = html.IndexOf(
                closedParagraphTag,
                indexOfDiv,
                StringComparison.InvariantCultureIgnoreCase);

            string allSkills = html.Substring(indexOfOpenedPTag, indexOfClosedPTag - indexOfOpenedPTag);
            allSkills = RemoveRedundantTabs(allSkills);
            return allSkills.Split(new [] { ", " }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string GetProfileUrl(string html, string baseUrl)
        {
            const string divTag = "<div class=\"links\">";
            const string closedDiv = "</div>";
            const string aTag = "<a href=\"";
            string url = aTag + baseUrl;

            int indexOfDiv = html.IndexOf(
             divTag,
             StringComparison.InvariantCultureIgnoreCase);

            if (indexOfDiv == -1)
            {
                return string.Empty;
            }

            int indexOfClosedDiv = html.IndexOf(
                closedDiv,
                indexOfDiv,
                StringComparison.InvariantCultureIgnoreCase
                );

            int indexOfStartUrl = html.IndexOf(
                url,
                indexOfDiv,
                StringComparison.InvariantCultureIgnoreCase);

            if (indexOfStartUrl == -1 || indexOfStartUrl > indexOfClosedDiv)
            {
                return string.Empty;
            }

            indexOfStartUrl += aTag.Length;

            int indexOfEndUrl = html.IndexOf(
                '\"',
                indexOfStartUrl);

            return html.Substring(indexOfStartUrl, indexOfEndUrl - indexOfStartUrl);
        }

        private string RemoveRedundantTabs(string str)
        {
            return str.Split('\t', '\n').Single(s => !string.IsNullOrEmpty(s));
        }

        private string RemoveRedundantPositionUrl(string position)
        {
            const string leftBorder = "<a class=\"company";
            const string rightBorder = ">";
            const string closedSpan = "</span>";
            int indexOfLeftBorder = position.IndexOf(leftBorder, StringComparison.InvariantCultureIgnoreCase);
            if (indexOfLeftBorder == -1)
            {
                return position;
            }

            int indexOfRightBorder = position.IndexOf(
                rightBorder,
                position.IndexOf(rightBorder, indexOfLeftBorder, StringComparison.InvariantCultureIgnoreCase) + rightBorder.Length, StringComparison.InvariantCultureIgnoreCase);

            position = position.Remove(indexOfLeftBorder, indexOfRightBorder + rightBorder.Length - indexOfLeftBorder);
            return position.Remove(position.IndexOf(closedSpan));
        }
    }
}