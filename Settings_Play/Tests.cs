using Xunit;

namespace Settings_Play {
    public class Tests {

        [Fact]
        public void SendSms_Test()
        {
            var euroMemberSmsSender = new EuroMemberSmsSender("Ilker","Halil","+905422476935");
            euroMemberSmsSender.SendSms("+905354095406","Merhaba");



        }
    }
}
