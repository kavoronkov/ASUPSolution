using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Utility
{
    public static class UtilityClass
    {
        public static int StringToInt32FromDictionary(Dictionary<string, string> dictionary, string stringKey)
        {
            try
            {
                int intValue = -1;
                string stringValue = "";

                if (dictionary.TryGetValue(stringKey, out stringValue))
                {
                    Int32.TryParse(stringValue, out intValue);
                }

                return intValue;
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public static long StringToInt64FromDictionary(Dictionary<string, string> dictionary, string stringKey)
        {
            try
            {
                long longValue = -1;
                string stringValue = "";

                if (dictionary.TryGetValue(stringKey, out stringValue))
                {
                    Int64.TryParse(stringValue, out longValue);
                }

                return longValue;
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public static string StringToStringFromDictionary(Dictionary<string, string> dictionary, string stringKey)
        {
            try
            {
                string stringValue = "";
                dictionary.TryGetValue(stringKey, out stringValue);
                return stringValue;
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
