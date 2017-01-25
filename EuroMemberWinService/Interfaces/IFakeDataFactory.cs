using System.Collections.Generic;
using EuroMemberWinService.OtherImpl;

namespace EuroMemberWinService.Interfaces {
    public interface IFakeDataFactory {
        IEnumerable<FakeSmsData> GenerateSmsData();
    }
}