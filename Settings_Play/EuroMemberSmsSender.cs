using System;
using System.Configuration;
using System.Text;
using Settings_Play.ConfigurationSections;

namespace Settings_Play {
    public class EuroMemberSmsSender {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string MsIsdn { get; set; }

        public int Counter {
            get { return EuroMemberSettings.DefaultInstance.Counter; }
            set { EuroMemberSettings.DefaultInstance.Counter = value; }
        }

        public DateTime? LastRunDate {
            get { return EuroMemberSettings.DefaultInstance.LastRunDate; }
            set { EuroMemberSettings.DefaultInstance.LastRunDate = value; }
        }

        public EuroMemberSmsSender()
        {
            Init();
        }
        public EuroMemberSmsSender(string userName, string password, string msIsdn) {
            UserName = userName;
            Password = password;
            MsIsdn = msIsdn;
        }

        void Init() {
            var section = ConfigurationManager.GetSection("EuroMemberSmsSender") as EuroMemberConfigurationSection;
            if (section != null) {
                UserName = section.UserName;
                Password = section.Password;
                MsIsdn = section.MsIsdn;
            }
        }

        public void SendSms(string number, string content) {
            if (string.IsNullOrWhiteSpace(UserName)) throw new ArgumentNullException(nameof(UserName));
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentNullException(nameof(Password));
            if (string.IsNullOrWhiteSpace(MsIsdn)) throw new ArgumentNullException(nameof(MsIsdn));
            Console.WriteLine("Sms atıldı");
            Counter = Counter + 1;
            LastRunDate = DateTime.Now;
            EuroMemberSettings.DefaultInstance.Save();

        }






    }
}
