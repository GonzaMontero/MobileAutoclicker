using System.Collections.Generic;
using Unity.VisualScripting;

namespace Autoclicker.Scripts.Utils.Localization
{
    public class Loc
    {
        public enum Language
        {
            English,
            Spanish
        }

        public static Language CurrentLanguage = Language.English;
        public static bool IsInit = false;

        private static Dictionary<string, string> localisedEN;
        private static Dictionary<string, string> localisedES;

        public static void Init()
        {
            CSVLoader CSVLoader = new CSVLoader();
            CSVLoader.LoadCSV("Localization/localization");

            localisedEN = CSVLoader.GetDictionaryValues("en");
            localisedES = CSVLoader.GetDictionaryValues("spa");

            IsInit = true;
        }

        public static string ReplaceKey(string key)
        {
            if (!IsInit)
                Init();

            string value = key;

            switch (CurrentLanguage)
            {
                case Language.English:
                    localisedEN.TryGetValue(key, out value);
                    break;
                case Language.Spanish:
                    localisedES.TryGetValue(key, out value);
                    break;                                 
                default:
                    break;
            }

            return value;
        }
    }
}