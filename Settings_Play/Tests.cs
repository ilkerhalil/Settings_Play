using System;
using System.Diagnostics;
using Xunit;

namespace Settings_Play {
    public class Tests {

        [Fact]
        public void SendSms_Test() {
            //var euroMemberSmsSender = new EuroMemberSmsSender("Ilker", "Halil", "+905422476935");
            var euroMemberSmsSender = new EuroMemberSmsSender();
            euroMemberSmsSender.SendSms("+905354095406", "Merhaba");
            Assert.NotEqual(euroMemberSmsSender.LastRunDate, default(DateTime));
            Assert.True(euroMemberSmsSender.Counter > 0);
      
        }
    }
}
