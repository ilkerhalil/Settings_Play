using System.Collections.Generic;
using Quartz;

namespace EuroMemberWinService.Interfaces
{
    public interface IHeJob : IJob
    {
        IEnumerable<string> CronExpressions { get; set; }

    }
}