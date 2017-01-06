using System.Collections;
using System.Collections.Generic;

namespace GF47RunTime
{
    public class BlackBoard : IEnumerable<KeyValuePair<string, object>>
    {
        private Dictionary<string, object> _items;

        public BlackBoard()
        {
            _items = new Dictionary<string, object>();
        }

        public void SetValue(string key, object v)
        {
            if (_items.ContainsKey(key)) { _items[key] = v; }
            else { _items.Add(key, v); }
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (_items.ContainsKey(key))
            {
                object v = _items[key];
                if (v is T)
                {
                    return (T)v;
                }
            }
            return defaultValue;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //return _items.GetEnumerator();
            return ((IEnumerable)_items).GetEnumerator();
        }
    }
}
