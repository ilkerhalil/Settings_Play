using System.Configuration;

namespace EuroMemberWinService.Jobs.ConfigSections
{
    public class ScheduledJobSection : ConfigurationSection {

        [ConfigurationProperty("CronExpressions")]
        [ConfigurationCollection(typeof(CronExpressionCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public CronExpressionCollection CronExpression {
            get { return this["CronExpressions"] as CronExpressionCollection; }
        }
    }
}