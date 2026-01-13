using System.Globalization;
using System.Text.RegularExpressions;

namespace Web.Helpers
{
    public class TextHelper
    {
        public static List<string> SplitIntoLines(string src) => Regex.Split(src, "\r\n|\r|\n").ToList();

        public static List<string> MatchingSegments(List<string> oSegments, string sPattern)
        {
            return oSegments.Where(x => Regex.Matches(x, sPattern).Count > 0).ToList();
        }



    }
}
