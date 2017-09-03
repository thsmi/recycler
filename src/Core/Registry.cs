using Microsoft.Win32;
using System;

namespace Recycler.Core
{
    public class RegKey
    {
        private RegistryHive hive;
        private string key;

        public RegKey(RegistryHive hive, string key)
        {
            this.hive = hive;
            this.key = key;
        }

        private RegistryKey GetBaseKey()
        {
            RegistryView view = RegistryView.Registry32;

            if (Environment.Is64BitOperatingSystem)
                view = RegistryView.Registry64;

            return RegistryKey.OpenBaseKey(this.hive, view);
        }

        private RegistryKey GetKey(string subKey = "")
        {
            return GetBaseKey().OpenSubKey(this.key + subKey);
        }

        private RegistryKey GetOrCreateKey(string subKey = "")
        {
            return GetBaseKey().CreateSubKey(this.key + subKey);
        }

        public bool HasKey(string subkey)
        {
            return (this.GetKey(subkey) == null ? false : true);
        }

        public string[] GetSubKeyNames(string subKey = "")
        {
            return this.GetKey(subKey).GetSubKeyNames();
        }

        public string[] GetValueNames(string subKey = "")
        {
            return GetKey(subKey).GetValueNames();
        }

        private object GetValue(string subKey, string name)
        {
            return GetKey(subKey).GetValue(name);
        }

        public string GetString(string subKey, string name, string defaultValue = "")
        {
            object value = GetValue(subKey, name);

            if ((value != null) && (value is string))
                return (string)value;

            return defaultValue;
        }
    
        public void SetString(string subKey, string name, string value)
        {
            GetOrCreateKey(subKey).SetValue(name, value, RegistryValueKind.String);
        }

        public int GetInt(string subKey, string name, int defaultValue = -1)
        {
            object value = GetValue(subKey, name);

            if ((value != null) && (value is int))
                return (int)value;

            return defaultValue;
        }

        public void SetInt(string subKey, string name, int value)
        {
            GetOrCreateKey(subKey).SetValue(name, value, RegistryValueKind.DWord);
        }

        public void RemoveKey(string subkey)
        {
            if (HasKey(subkey) == false)
                return;

            GetBaseKey().DeleteSubKeyTree(key + subkey);
        }
    }
}
