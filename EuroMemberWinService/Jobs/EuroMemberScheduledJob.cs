using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EuroMemberWinService.Interfaces;
using EuroMemberWinService.Jobs.ConfigSections;
using Quartz;
using Settings_Play;

namespace EuroMemberWinService.Jobs {
    public class EuroMemberScheduledJob : IHeJob {
        private readonly IFakeDataFactory _dataFactory;
        private readonly ISmsSender _smsSender;


        public IEnumerable<string> CronExpressions { get; set; }

        public EuroMemberScheduledJob(IFakeDataFactory dataFactory, ISmsSender smsSender) {
            _dataFactory = dataFactory;
            _smsSender = smsSender;
            Init();
        }

        void Init()
        {
            var scheduledJobSection = ConfigurationManager.GetSection("ScheduledJobSection") as ScheduledJobSection;
            if (scheduledJobSection == null) return;
            var cronExpressionCollections = scheduledJobSection.CronExpression.Cast<CronExpressionElement>();
            var keys = cronExpressionCollections.Where(w => w.Key == "EuroMemberScheduledJob").ToList();
            if (keys.Any())
            {
                CronExpressions = keys.Select(s => s.CronExpression);
            }
        }

        public void Execute(IJobExecutionContext context) {
            var smsStack = _dataFactory.GenerateSmsData();
            foreach (var fakeSmsData in smsStack) {
                _smsSender.SendSms(fakeSmsData.Number, fakeSmsData.Content);
            }
        }


    }
}