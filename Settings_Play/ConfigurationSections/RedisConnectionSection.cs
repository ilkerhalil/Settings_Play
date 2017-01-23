using System.Configuration;

namespace Settings_Play.ConfigurationSections {
    public class RedisConnectionSection : ConfigurationSection {

        [ConfigurationProperty("Host")]

        public string Host {
            get { return this["Host"] as string; }
            set { this["Host"] = value; }
        }
        [ConfigurationProperty("Password")]

        public string Password {
            get { return this["Password"] as string; }
            set { this["Password"] = value; }
        }

        [ConfigurationProperty("Port", DefaultValue = 6379)]

        public int Port {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }
    }
}
