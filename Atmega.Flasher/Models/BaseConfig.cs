using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using Atmega.Flasher.Annotations;

namespace Atmega.Flasher.Models {
    public abstract class BaseConfig : INotifyPropertyChanged {
        private readonly string _keyPrefix;

        protected BaseConfig(string keyPrefix) {
            _keyPrefix = keyPrefix;
        }

        protected string KeyPrefix { get { return _keyPrefix; } }

        protected string GetKey(string key) {
            if (!string.IsNullOrWhiteSpace(KeyPrefix)) {
                key = KeyPrefix + key;
            }
            return key;
        }

        protected void UpdateConfig(string value, string key) {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");
            key = GetKey(key);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var set = config.AppSettings.Settings[key];
            if (set != null) {
                config.AppSettings.Settings[key].Value = value;
            } else {
                config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private string GetConfig(string key) {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");
            key = GetKey(key);
            return ConfigurationManager.AppSettings[key];
        }

        protected string GetConfigString(string defaultValue, string key) {
            return GetConfig(key) ?? defaultValue;
        }

        protected int GetConfigInt(int defaultValue, string key) {
            var raw = GetConfig(key);
            int res;
            return int.TryParse(raw, out res) ? res : defaultValue;
        }

        protected TEnum GetConfigEnum<TEnum>(TEnum defaultValue, string key) where TEnum : struct {
            var raw = GetConfig(key);
            TEnum res;
            return Enum.TryParse(raw, out res) ? res : defaultValue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void Save();

        public abstract void ReadFromConfig();
    }
}
