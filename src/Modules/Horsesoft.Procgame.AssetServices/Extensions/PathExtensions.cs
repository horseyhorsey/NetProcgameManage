using System.Text.RegularExpressions;

namespace Horsesoft.Procgame.AssetServices
{
    public static class StringExtensions
    {
        public static string MatchAndReplaceFileNameWithDigits(this string filePath)
        {
            var match = Regex.Match(filePath, @"%0");
            if (!match.Success) return filePath;

            var index = match.Index;
            var digit = filePath[index + 2];

            int dig;
            int.TryParse(digit.ToString(), out dig);
            var numString = "";
            for (int i = 0; i < dig -1; i++)
            {
                numString += "0";
            }

            numString = numString.Insert(numString.Length, "1");

            return filePath.Replace(@"%0" + digit + "d", numString);
        }
    }
}
