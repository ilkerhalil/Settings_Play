using System;
using System.ComponentModel;
using System.Configuration;


namespace Settings_Play.ConfigurationSections {

    [SettingsProvider(typeof(RedisSettingsProvider))]
    public class EuroMemberSettings : ApplicationSettingsBase {
        public static EuroMemberSettings DefaultInstance { get; } = ((EuroMemberSettings)(Synchronized(new EuroMemberSettings())));

        [ApplicationScopedSetting]
        [DefaultValue(0)]
        public int Counter {
            get {
                return (int)this["Counter"];
            }
            set { this["Counter"] = value; }
        }

        [ApplicationScopedSetting]
        public DateTime? LastRunDate {
            get { return (DateTime)this["LastRunDate"]; }
            set { this["LastRunDate"] = value; }
        }
    }
}
