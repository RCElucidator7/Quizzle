using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace QuizGame.Settings
{
    public class Settings
    {
        public static int highScore, speed;

        public static void saveSetting(string key, string contents)
        {
            ApplicationData.Current.LocalSettings.Values[key] = contents;
        }

        public static string loadSettings(string key)
        {
            var setting = ApplicationData.Current.LocalSettings;

            return setting.Values.ContainsKey(key) ? setting.Values[key] as string : String.Empty;
        }
    }
}
