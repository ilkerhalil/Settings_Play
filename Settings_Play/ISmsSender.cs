using System;

namespace Settings_Play {
    public interface ISmsSender {
        int Counter { get; set; }
        DateTime? LastRunDate { get; set; }
        string MsIsdn { get; set; }
        string Password { get; set; }
        string UserName { get; set; }

        void SendSms(string number, string content);
    }
}