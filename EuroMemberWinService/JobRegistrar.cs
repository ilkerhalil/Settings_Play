using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EuroMemberWinService.Aspects;
using EuroMemberWinService.Interfaces;
using Quartz;

namespace EuroMemberWinService
{
    public class JobRegistrar {
        private readonly IWindsorContainer _container;

        public JobRegistrar(IWindsorContainer container) {
            _container = container;
        }

        private static IEnumerable<Type> GetJobTypes() {
            return AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IJob).IsAssignableFrom(p) && !p.IsInterface);
        }

        public void RegisterJobs() {
            var jobTypes = GetJobTypes();
            foreach (var jobType in jobTypes) {
                _container.Register(Component.For(jobType)
                    .ImplementedBy(jobType)
                    .Interceptors<ReportCronitorAspect>()
                    .LifeStyle.Transient);
            }
        }
    }
}