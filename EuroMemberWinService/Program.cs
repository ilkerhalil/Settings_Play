using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EuroMemberWinService.Aspects;
using EuroMemberWinService.Interfaces;
using EuroMemberWinService.JobFactory;
using EuroMemberWinService.Jobs;
using EuroMemberWinService.OtherImpl;
using Quartz;
using Quartz.Impl;
using Settings_Play;
using Topshelf;
using Topshelf.CastleWindsor;

namespace EuroMemberWinService {
    class Program {

        private const string ServiceDisplayName = "My Service";
        private const string ServiceName = "MyService";
        private const string ServiceDescription = "Does something interesting";

        static void Main() {

            var container = CastleBootstrapper();
            CreateHost(container);
        }

        private static TopshelfExitCode CreateHost(IWindsorContainer container) {
            var euroMemberService = container.Resolve<IEuroMemberService>();
            var heJobs = container.ResolveAll<IHeJob>();
            var scheduler = container.Resolve<IScheduler>();
            foreach (var heJob in heJobs) {
                ScheduleJob(scheduler, heJob);
            }
            var hostFactory = HostFactory.Run(configurator => {
                configurator.UseWindsorContainer(container);
                configurator.Service<IEuroMemberService>(x => {

                    x.ConstructUsing(c => euroMemberService);
                    configurator.SetServiceName(ServiceName);
                    configurator.SetDescription(ServiceDescription);
                    configurator.SetDisplayName(ServiceDisplayName);
                    x.WhenStarted(service => service.Start());
                    x.WhenStopped(service => {
                        service.Stop();
                        container.Release(euroMemberService);
                    });
                    configurator.RunAsLocalService();
                });

            });
            return hostFactory;
        }

        private static IWindsorContainer CastleBootstrapper() {
            var defaultScheduler = StdSchedulerFactory.GetDefaultScheduler();
            var windsorContainer = new WindsorContainer();
            windsorContainer.Register(Component.For<IFakeDataFactory>().ImplementedBy<FakeDataFactory>(),
                Component.For<ISmsSender>().ImplementedBy<EuroMemberSmsSender>().LifestyleTransient(),
                Component.For<IInterceptor>().ImplementedBy<ReportCronitorAspect>().LifestyleTransient(),
                Component.For<IEuroMemberService>().ImplementedBy<EuroMemberSmsService>().LifestyleTransient(),
                Component.For<IHeJob>().ImplementedBy<EuroMemberScheduledJob>().LifestyleTransient(),
                Component.For<IScheduler>().Instance(defaultScheduler));
            defaultScheduler.JobFactory = new WindsorJobFactory(windsorContainer);
            return windsorContainer;
        }

        private static void ScheduleJob(IScheduler scheduler, IJob job) {
            var jobDetail = JobBuilder.Create(job.GetType()).Build();
            foreach (var jobCronExpression in ((IHeJob)job).CronExpressions) {
                var trigger = TriggerBuilder.Create().WithCronSchedule(jobCronExpression).Build();
                scheduler.ScheduleJob(jobDetail, trigger);
            }
        }



    }
}
