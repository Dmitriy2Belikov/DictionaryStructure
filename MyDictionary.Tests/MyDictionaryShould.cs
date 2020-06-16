using System;
using NUnit.Framework;
using System.Diagnostics;

namespace MyDictionary.Tests
{
    public class MyDictionaryShould
    {
        private IDictionary _dictionary;

        [SetUp]
        public void Init()
        {
            _dictionary = new StringDictionary();
        }

        [Test]
        public void AddAndReturnShould()
        {
            _dictionary.Add("TestKey", "TestValue");

            Assert.AreEqual("TestValue", _dictionary["TestKey"]);
        }

        [Test]
        public void ContainsKeyShould()
        {
            _dictionary.Add("TestKey", "TestValue");

            Assert.AreEqual(true, _dictionary.ContainsKey("TestKey"));
        }

        [Test]
        public void ContainsKeyAllShould()
        {
            var count = 130;

            for (var i = 0; i < count; i++)
            {
                _dictionary.Add("Key_" + i, "Value_" + i);
            }

            for (var i = 0; i < count; i++)
            {
                Assert.True(_dictionary.ContainsKey("Key_" + i));
            }
        }

        [Test]
        public void RemoveShould()
        {
            _dictionary.Add("TestKey", "TestValue");

            _dictionary.Remove("TestKey");

            Assert.AreEqual(false, _dictionary.ContainsKey("TestKey"));
        }

        [Test]
        public void RemoveAllShould()
        {
            var count = 65;

            for (var i = 0; i < count; i++)
            {
                _dictionary.Add("Key_" + i, "Value_" + i);
            }

            for (var i = 0; i < count; i++)
            {
                _dictionary.Remove("Key_" + i);
            }

            Assert.AreEqual(0, _dictionary.Count);

            for (var i = 0; i < count; i++)
            {
                Assert.IsFalse(_dictionary.ContainsKey("Key_" + i));
            }
        }

        [Test]
        public void UpdateShould()
        {
            _dictionary.Add("TestKey", "TestValue");

            _dictionary["TestKey"] = "TestedValue";

            Assert.AreEqual("TestedValue", _dictionary["TestKey"]);
        }

        [Test]
        public void NullShould()
        {
            var key = _dictionary["Unknown_key"];
            
            Assert.Catch<ArgumentNullException>(() =>_dictionary.Add(null, "value"));
            Assert.Catch<ArgumentNullException>(() =>_dictionary.ContainsKey(null));
            Assert.Catch<ArgumentNullException>(() =>_dictionary.Remove(null));
            Assert.AreEqual(null, key);
        }

        [Test]
        public void ThirtyThousandValuesSpeedShould()
        {
            var timer = new Stopwatch();
            var timeLimitInSec = 0.1;
            var count = 60000;

            for (var i = 0; i < count; i++)
                _dictionary.Add("Key" + i, "Value" + i);

            timer.Start();
            var key = "Key29000";
            var value = _dictionary[key];
            var contains = _dictionary.ContainsKey(key);
            _dictionary.Remove(key);
            _dictionary[key] = "123";
            timer.Stop();

            Assert.AreEqual(true, timer.Elapsed.TotalSeconds < timeLimitInSec);
            Assert.IsTrue(contains);
            Assert.AreEqual("Value29000", value);
            Assert.AreEqual("123", _dictionary[key]);
            Assert.AreEqual(count, _dictionary.Count);
        }
    }
}