using FluentAssertions;
using NUnit.Framework;

namespace AOC2022.Days
{
    public class DayFive
    {
        private readonly Stack<char>[] _stacks = 
        {
            new(new[] {'F', 'H', 'B', 'V', 'R', 'Q', 'D', 'P'}),
            new(new[] {'L', 'D', 'Z', 'Q', 'W', 'V'}),
            new(new[] {'H', 'L', 'Z', 'Q', 'G', 'R', 'P', 'C'}),
            new(new[] {'R', 'D', 'H', 'F', 'J', 'V', 'B'}),
            new(new[] {'Z', 'W', 'L', 'C'}),
            new(new[] {'J', 'R', 'P', 'N', 'T', 'G', 'V', 'M'}),
            new(new[] {'J', 'R', 'L', 'V', 'M', 'B', 'S'}),
            new(new[] {'D', 'P', 'J'}),
            new(new[] {'D', 'C', 'N', 'W', 'V'}),
        };

        private readonly List<(int amount, int from, int to)> _moves = new();

        [OneTimeSetUp]
        public void Setup()
        {
            foreach (var split in File.ReadAllLines("Input/DayFive.txt").Select(x => x.Split(" ")))
            {
                _moves.Add((int.Parse(split[1]), int.Parse(split[3]) - 1, int.Parse(split[5]) - 1));
            }
        }

        [Test]
        public void Test1()
        {
            foreach (var move in _moves)
            {
                for (int i = 0; i < move.amount; i++)
                {
                    _stacks[move.to].Push(_stacks[move.from].Pop());
                }
            }

            GetTopLevel().Should().Be("JDTMRWCQJ");
        }

        [Test]
        public void Test2()
        {
            foreach (var move in _moves)
            {
                Stack<char> tempStack = new();
                for (int i = 0; i < move.amount; i++)
                {
                    tempStack.Push(_stacks[move.from].Pop());
                }

                while (tempStack.Count > 0)
                {
                    _stacks[move.to].Push(tempStack.Pop());
                }
            }
            GetTopLevel().Should().Be("VHJDDCWRD");
        }

        public string GetTopLevel() => _stacks.Aggregate("", (total, current) => total + current.Peek());
    }
}
