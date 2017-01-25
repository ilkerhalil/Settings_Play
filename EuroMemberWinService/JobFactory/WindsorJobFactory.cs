using Castle.Windsor;
using EuroMemberWinService.Interfaces;
using Quartz;
using Quartz.Spi;

namespace EuroMemberWinService.JobFactory {
    public class WindsorJobFactory : IJobFactory {
        private readonly IWindsorContainer _container;

        public WindsorJobFactory(IWindsorContainer container) {
            _container = container;
        }

        

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IHeJob)_container.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            //throw new NotImplementedException();
        }
    }
}
