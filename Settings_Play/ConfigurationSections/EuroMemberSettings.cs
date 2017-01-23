using System.ComponentModel;
using System.Configuration;
 

namespace Settings_Play.ConfigurationSections {

    [SettingsProvider(typeof(RedisSettingsProvider))]
    public class EuroMemberSettings : ApplicationSettingsBase {
        //private readonly EuroMemberSettings _defaultInstance = new EuroMemberSettings();
        private static readonly EuroMemberSettings defaultInstance = ((EuroMemberSettings)(Synchronized(new EuroMemberSettings())));

        [ApplicationScopedSetting]
        [DefaultValue(0)]
        public int Counter {
            get {
                return (int)this["Counter"];
            }
            set { this["Counter"] = value; }
        }

        public static EuroMemberSettings DefaultInstance {
            get { return defaultInstance; }
        }

    }
}
