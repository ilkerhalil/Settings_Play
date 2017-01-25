using System.Collections.Generic;
using EuroMemberWinService.Interfaces;

namespace EuroMemberWinService.OtherImpl
{
    public class FakeDataFactory : IFakeDataFactory {

        public IEnumerable<FakeSmsData> GenerateSmsData() {

            for (var i = 0;i < 1000;i++) {
                var number = Faker.PhoneFaker.Phone();
                var content = Faker.TextFaker.Sentences(144);
                yield return new FakeSmsData() { Number = number, Content = content };
            }
        }
    }
}