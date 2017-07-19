using System.Collections.Generic;
using System.Xml;
using GF47RunTime.Data;
using UnityEngine;

namespace GF47RunTime.Configuration
{
    public class Config
    {
        private Dictionary<string, object> _config;

        public void Initialize(string configPath)
        {
            _config = new Dictionary<string, object>();

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            XmlNode root = doc.LastChild;

            if (root != null)
            {
                XmlNodeList list = root.SelectNodes(ConstValues.NODE);
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string name = string.Empty;
                        if (!list[i].HasAttribute(ConstValues.NAME, ref name)) { continue; }

                        string type = string.Empty;
                        if (!list[i].HasAttribute(ConstValues.TYPE, ref type)) { continue; }

                        string valueStr = string.Empty;
                        if (!list[i].HasAttribute(ConstValues.VALUE, ref valueStr)) { continue; }
                        object value = Convert.ConvertTo(type, valueStr);

                        _config.Add(name, value);
                    }
                }
            }
        }

        public T Get<T>(string name, T defaultValue)
        {
            if (_config == null)
            {
                Debug.LogWarningFormat("{0} is not found, use default value {1}", name, defaultValue);
                return defaultValue;
            }
            if (_config.ContainsKey(name))
            {
                return (T)_config[name];
            }
            return defaultValue;
        }

        public T Get<T>(string name)
        {
            return Get(name, default(T));
        }
    }
}
