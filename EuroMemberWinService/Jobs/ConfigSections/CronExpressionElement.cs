using System.Configuration;
using Quartz;

namespace EuroMemberWinService.Jobs.ConfigSections {
    public class CronExpressionElement : ConfigurationElement {
        [ConfigurationProperty("key")]
        public string Key {
            get { return this["key"] as string; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("cronExpression")]
        public string CronExpression {
            get { return this["cronExpression"] as string; }
            set { this["cronExpression"] = value; }
        }
    }
}
