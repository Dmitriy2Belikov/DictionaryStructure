using System;
using System.Collections.Generic;

namespace MyDictionary
{
    public class MyDictionary : IDictionary
    {
        public List<string> Keys { get; }
        public List<string> Values { get; }

        public MyDictionary()
        {
            Keys = new List<string>();
            Values = new List<string>();
        }

        public void Add(string key, string value)
        {
            if (ContainsKey(key))
            {
                Values[Keys.IndexOf(key)] = value;
            }

            Keys.Add(key);
            Values.Add(value);
        }

        public bool ContainsKey(string key)
        {
            return Keys.Contains(key);
        }

        public void Remove(string key)
        {
            Values.RemoveAt(Keys.IndexOf(key));
            Keys.Remove(key);
        }

        public string this[string key]
        {
            get
            {
                return Values[Keys.IndexOf(key)];
            }

            set => Add(key, value);
        }
    }
}
