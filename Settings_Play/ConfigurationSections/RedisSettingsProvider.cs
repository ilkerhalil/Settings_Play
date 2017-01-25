using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using CSRedis;

namespace Settings_Play.ConfigurationSections {
    public class RedisSettingsProvider : SettingsProvider, IDisposable {
        public override string Name { get; } = nameof(RedisSettingsProvider);
        private RedisClient _redisClient;
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        void Connect() {
            if (Port == 0)
                Port = 6379;
            _redisClient = new RedisClient(Host, Port);
            if (!string.IsNullOrWhiteSpace(Password)) {
                _redisClient.Auth(Password);
            }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection) {

            var hGetAll = _redisClient.HGetAll(ApplicationName);
            var values = new SettingsPropertyValueCollection();
            foreach (SettingsProperty settingsProperty in collection) {
                string stringValue;
                if (hGetAll.TryGetValue(settingsProperty.Name, out stringValue)) {
                    values.Add(new SettingsPropertyValue(settingsProperty) { IsDirty = false, SerializedValue = stringValue });
                    continue;
                }
                var defaultAttributeValue = (DefaultValueAttribute)settingsProperty.Attributes[typeof(DefaultValueAttribute)];
                if (defaultAttributeValue == null) {
                    values.Add(new SettingsPropertyValue(settingsProperty) { IsDirty = false, PropertyValue = GetDefault(settingsProperty.PropertyType) });
                    continue;
                }
                values.Add(new SettingsPropertyValue(settingsProperty) { IsDirty = false, PropertyValue = defaultAttributeValue.Value });
            }
            return values;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection) {
            var dicStore = collection.Cast<SettingsPropertyValue>().ToDictionary(settingsPropertyValue => settingsPropertyValue.Name, settingsPropertyValue => settingsPropertyValue.SerializedValue.ToString());

            _redisClient.HMSet(ApplicationName, dicStore);

        }
        public override void Initialize(string name, NameValueCollection config) {
            var redisConnectionSection = ConfigurationManager.GetSection("RedisSettingsProvider") as RedisConnectionSection;
            if (redisConnectionSection != null) {
                Host = redisConnectionSection.Host;
                Password = redisConnectionSection.Password;
                Port = redisConnectionSection.Port;

            }
            Connect();
        }

        public object GetDefault(Type type) {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
        public override string ApplicationName { get; set; }

        public void Dispose() {

            _redisClient?.Dispose();
        }
    }
}