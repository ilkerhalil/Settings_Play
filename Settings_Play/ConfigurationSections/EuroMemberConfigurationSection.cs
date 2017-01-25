using System.Configuration;

namespace Settings_Play.ConfigurationSections {
    public class EuroMemberConfigurationSection : ConfigurationSection {

        [ConfigurationProperty("UserName")]
        public string UserName {
            get {
                return this["UserName"] as string;
            }
            set { this["UserName"] = value; }
        }
        [ConfigurationProperty("Password")]
        public string Password {
            get {
                return this["Password"] as string;
            }
            set { this["Password"] = value; }
        }
        [ConfigurationProperty("MsIsdn")]
        public string MsIsdn {
            get {
                return this["MsIsdn"] as string;
            }
            set { this["MsIsdn"] = value; }
        }

    }
}
