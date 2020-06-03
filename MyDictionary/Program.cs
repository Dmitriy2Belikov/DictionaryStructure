using System;
using System.Collections.Generic;
using System.Linq;

namespace MyDictionary
{
    class MyDictionary<TKey,TValue>
    {
        public List<TKey> Keys { get; private set; }
        public List<TValue> Values { get; private set; }

        public MyDictionary()
        {
            Keys = new List<TKey>();
            Values = new List<TValue>();
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new Exception("Данный ключ уже существует");

            Keys.Add(key);
            Values.Add(value);
        }

        public bool ContainsKey(TKey key)
        {
            return Keys.Contains(key);
        }

        public void Remove(TKey key)
        {
            Keys.Remove(key);
            Values.RemoveAt(Keys.IndexOf(key));
        }

        public TValue this[TKey key]
        {
            get
            {
                return Values[Keys.IndexOf(key)];
            }
            set
            {
                Values[Keys.IndexOf(key)] = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var myDict = new MyDictionary<int,string>();

            var count = 20000;

            for (int i = 0; i < count; i++)
                myDict.Add(i, "Dmitry" + i.ToString());

            Console.WriteLine(myDict[19000]);
        }
    }
}
