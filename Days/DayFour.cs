using FluentAssertions;
using NUnit.Framework;

namespace AOC2022.Days
{
    public class DayFour
    {

        public List<((int low, int high) first, (int low, int high) second)> Pairs = new();

        [OneTimeSetUp]
        public void Setup()
        {
            foreach (var line in File.ReadAllLines("Input/DayFour.txt"))
            {
                ((int low, int high) first, (int low, int high) second) pair = new();


                var split = line.Split(',');
                var first = split[0].Split('-');
                var second = split[1].Split('-');
                
                pair.first.low = int.Parse(first[0]);
                pair.first.high = int.Parse(first[1]);

                pair.second.low = int.Parse(second[0]);
                pair.second.high = int.Parse(second[1]);

                Pairs.Add(pair);

            }
        }

        [Test]
        public void Test1()
        {
            Pairs.FindAll(pair => Between(pair.first, pair.second) || Between(pair.second, pair.first)).Count.Should().Be(569);
        }

        [Test]
        public void Test2()
        {
            Pairs.FindAll(pair => OverLaps(pair.first, pair.second) || OverLaps(pair.second, pair.first)).Count.Should().Be(936);
        }

        private static bool Between((int, int) first, (int, int) second)
        {
            return first.Item1 >= second.Item1 && first.Item2 <= second.Item2;
        }

        private static bool OverLaps((int, int) first, (int, int) second)
        {
            return (first.Item1 >= second.Item1 && first.Item1 <= second.Item2)
                || (first.Item2 <= second.Item2 && first.Item2 >= second.Item1);
        }
    }
}
