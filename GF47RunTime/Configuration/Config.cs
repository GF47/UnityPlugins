using System.Collections.Generic;
using System.Xml;
using GF47RunTime.Data;
using UnityEngine;

namespace GF47RunTime.Configuration
{
    public static class ConstValues
    {
        public const string ROOTNODE = "config";
        public const string NODE = "add";
        public const string NAME = "name";
        public const string TYPE = "type";
        public const string VALUE = "value";
    }

    public static class Config
    {
        private static readonly string ConfigPath = string.Format("{0}/Data/config.xml", Application.dataPath);
        private static Dictionary<string, object> _config;

        public static void Initialize() { Initialize(ConfigPath); }
        public static void Initialize(string configPath)
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

        public static T Get<T>(string name, T defaultValue)
        {
            if (_config.ContainsKey(name))
            {
                return (T)_config[name];
            }
            return defaultValue;
        }

        public static T Get<T>(string name)
        {
            return Get(name, default(T));
        }
    }
}
