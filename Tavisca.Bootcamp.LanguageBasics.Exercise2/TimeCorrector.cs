using System;
using System.Text.RegularExpressions;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class TimeCorrector{
         internal static Tuple<Time, int> GetCorrectionInfo(string showPost)
        {
            var regex = new Regex(@"(\b\w*\b) (seconds|minutes|hours) (\b\w*\b)", RegexOptions.Compiled);
            var parts = regex.Matches(showPost);
            if (parts != null && parts[0].Success)
            {
                var groups = parts[0].Groups;
                var toBeAdded = groups[1].Value;
                var changingPart = groups[2].Value;
                if(string.IsNullOrEmpty(toBeAdded) == false && string.IsNullOrEmpty(changingPart) == false)
                {
                    var needsToBeCorrected = GetPartOfTimeWhichNeedsToBeCorrected(changingPart);
                    var partToBeAdded = GetValueToBeAddedToCurrentTime(toBeAdded);
                    return new Tuple<Time, int>(needsToBeCorrected, partToBeAdded);
                }
            }
            return null;
        }
        private static Time GetPartOfTimeWhichNeedsToBeCorrected(string changingPart){
            if(string.IsNullOrEmpty(changingPart)==false){
                if(changingPart.Equals("hours"))
                    return Time.Hour;
                if(changingPart.Equals("minutes"))
                    return Time.Minute;
                if(changingPart.Equals("seconds"))
                    return Time.Second;                
            }
            return Time.Invalid;
        }
        private static int GetValueToBeAddedToCurrentTime(string toBeAdded){
            if(string.IsNullOrEmpty(toBeAdded)==false)
                if(int.TryParse(toBeAdded,out int value))
                    return value;
            return 0;
        }
    }
}