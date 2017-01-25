using EuroMemberWinService.Interfaces;
using Quartz;
using Settings_Play;

namespace EuroMemberWinService {
    public class EuroMemberSmsService : IEuroMemberService {
        private readonly IScheduler _scheduler;

        public EuroMemberSmsService(IScheduler scheduler) {
            _scheduler = scheduler;
        }

        public void Start() {
            _scheduler.Start();
        }

        public void Pause() {
            _scheduler.Standby();
        }

        public void Stop() {
            _scheduler.Shutdown();
        }
    }
}