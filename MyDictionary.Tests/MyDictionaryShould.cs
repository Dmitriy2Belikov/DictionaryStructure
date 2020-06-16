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
        public void RemoveShould()
        {
            _dictionary.Add("TestKey", "TestValue");

            _dictionary.Remove("TestKey");

            Assert.AreEqual(false, _dictionary.ContainsKey("TestKey"));
        }

        [Test]
        public void UpdateShould()
        {
            _dictionary.Add("TestKey", "TestValue");

            _dictionary["TestKey"] = "TestedValue";

            Assert.AreEqual("TestedValue", _dictionary["TestKey"]);
        }

        [Test]
        public void ThirtyThousandValuesSpeedShould()
        {
            var timer = new Stopwatch();
            var timeLimitInSec = 0.1;

            for (var i = 0; i < 60000; i++)
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
        }
    }
}