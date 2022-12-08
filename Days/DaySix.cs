using System.Collections.ObjectModel;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2022.Days
{
    public class DaySix
    {

        string Message { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Message = File.ReadAllText("Input/DaySix.txt");
        }

        [TestCase(4, 1757)]
        [TestCase(14, 2950)]
        public void Test1(int count, int expected)
        {
            GetSequenceIndex(Message, count).Should().Be(expected);
        }

        private int GetSequenceIndex(string value, int count)
        {
            var index = 0;
            HashSet<char> current = new();

            while (current.Count < count)
            {
                if (!current.Add(value[index]))
                {
                    index -= current.Count - 1;
                    current.Clear();
                }
                else
                {
                    index++;
                }
            }

            return index;
        }
    }
}
