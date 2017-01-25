using System.Configuration;

namespace EuroMemberWinService.Jobs.ConfigSections
{
    public class CronExpressionCollection : ConfigurationElementCollection {
        protected override ConfigurationElement CreateNewElement() {
            return new CronExpressionElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((CronExpressionElement)element).Key;
        }
    }
}