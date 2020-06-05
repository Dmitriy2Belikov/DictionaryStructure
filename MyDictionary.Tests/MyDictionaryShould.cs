using NUnit.Framework;
using System.Diagnostics;

namespace MyDictionary.Tests
{
    public class MyDictionaryShould
    {
        [Test]
        public void AddAndReturnShould()
        {
            var dict = new MyDictionary<string, string>();

            dict.Add("TestKey", "TestValue");

            Assert.AreEqual("TestValue", dict["TestKey"]);
        }

        [Test]
        public void ContainsKeyShould()
        {
            var dict = new MyDictionary<string, string>();

            dict.Add("TestKey", "TestValue");

            Assert.AreEqual(true, dict.ContainsKey("TestKey"));
        }

        [Test]
        public void RemoveShould()
        {
            var dict = new MyDictionary<string, string>();

            dict.Add("TestKey", "TestValue");

            dict.Remove("TestKey");

            Assert.AreEqual(false, dict.ContainsKey("TestKey"));
        }

        [Test]
        public void UpdateShould()
        {
            var dict = new MyDictionary<string, string>();

            dict.Add("TestKey", "TestValue");

            dict["TestKey"] = "TestedValue";

            Assert.AreEqual("TestedValue", dict["TestKey"]);
        }

        [Test]
        public void ThirtyThousandValuesSpeedShould()
        {
            var dict = new MyDictionary<string, string>();
            var timer = new Stopwatch();
            var timeLimitInSec = 0.1;

            for (int i = 0; i < 30000; i++)
                dict.Add("Key" + i.ToString(), "Value" + i.ToString());

            timer.Start();
            var randomKey = dict["Key29000"];
            timer.Stop();

            Assert.AreEqual(true, timer.Elapsed.TotalSeconds < timeLimitInSec);
        }
    }
}