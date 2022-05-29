using System.Linq;
using System.Text.RegularExpressions;

namespace StringFinder.Models
{
    public class RegexMatcher
    {
        public static string[] GetMatches(string pattern, string input)
        {
            Regex rg = new Regex(pattern);
            return rg.Matches(input)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();
  
        }
    }
}
