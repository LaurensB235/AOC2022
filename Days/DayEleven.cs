using System.Diagnostics;
using System.Numerics;
using System.Transactions;
using System.Xml.Schema;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2022.Days
{
    public class DayEleven
    {

        List<Monkey> monAkeys = new List<Monkey>()
        {
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{79, 98}),
                Operation = old => old * 19,
                Test = current => current % 23 == 0,
                Throws = (2, 3)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{54, 65, 75, 74}),
                Operation = old => old + 6,
                Test = current => current % 19 == 0,
                Throws = (2, 0)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{79, 60, 97}),
                Operation = old => old * old,
                Test = current => current % 13 == 0,
                Throws = (1, 3)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{74}),
                Operation = old => old + 3,
                Test = current => current % 17 == 0,
                Throws = (0, 1)
            },
        };

        public static ulong total2 = 17 * 13 * 19 * 23;
        public static ulong total = 2 * 7 * 3 * 11 * 17 * 5 * 13 * 19;

        private List<Monkey> monkeys = new List<Monkey>()
        {
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{54, 53}),
                Operation = old => old * 3,
                Test = current => current % 2 == 0,
                Throws = (2, 6)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{95, 88, 75, 81, 91, 67, 65, 84}),
                Operation = old => old * 11,
                Test = current => current % 7 == 0,
                Throws = (3, 4)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{76, 81, 50, 93, 96, 81, 83}),
                Operation = old => old + 6,
                Test = current => current % 3 == 0,
                Throws = (5, 1)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{83, 85, 85, 63}),
                Operation = old => old + 4,
                Test = current => current % 11 == 0,
                Throws = (7, 4)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{85, 52, 64}),
                Operation = old => old + 8,
                Test = current => current % 17 == 0,
                Throws = (0, 7)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{57}),
                Operation = old => old + 2,
                Test = current => current % 5 == 0,
                Throws = (1, 3)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{60, 95, 76, 66, 91}),
                Operation = old => old * old,
                Test = current => current % 13 == 0,
                Throws = (2, 5)
            },
            new()
            {
                Items = new Queue<ulong>(new List<ulong>{65, 84, 76, 72, 79, 65}),
                Operation = old => old + 5,
                Test = current => current % 19 == 0,
                Throws = (6, 0)
            },
        };



        [OneTimeSetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            for (ulong i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.ThrowItems(monkeys, 3);
                }
            }

            var ordered = monkeys.OrderByDescending(x => x.ThrowCount);
            
            
            var test = ordered.Select(x => x.ThrowCount).Take(2)
                .Aggregate((curr, next) => curr * next).Should().Be(111210);
        }

        [Test]
        public void Test2()
        {
            for (ulong i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.ThrowItems(monkeys, 1);
                }
                Debug.WriteLine("");
                Debug.Write($"round {i + 1} => ");
                Debug.WriteLine("");
                foreach (var monkey in monkeys)
                {
                    Debug.Write(monkey.ThrowCount);
                    Debug.Write(" ");
                }
                Debug.WriteLine("");
            }

            var ordered = monkeys.OrderByDescending(x => x.ThrowCount);


            var test = ordered.Select(x => x.ThrowCount).Take(2)
                .Aggregate((curr, next) => curr * next).Should().Be(2713310158);
        }
    }

    public class Monkey
    {
        public Queue<ulong> Items;
        public Func<ulong, ulong> Operation;
        public Func<ulong, bool> Test;
        public (int trueMonkey, int falseMonkey) Throws;
        public ulong ThrowCount;

        public void ThrowItems(List<Monkey> monkeys, ulong divider)
        {
            while (Items.Any())
            {
                Throw(monkeys, Items.Dequeue(), divider);
                ThrowCount++;
            }
        }


        public void Throw(List<Monkey> monkeys, ulong item, ulong divider)
        {

            var next = Operation.Invoke(item) % DayEleven.total;

            Debug.WriteLine(next);


            if (Test.Invoke(next))
            {
                monkeys.ElementAt(Throws.trueMonkey).Items.Enqueue(next);
            }
            else
            {
                monkeys.ElementAt(Throws.falseMonkey).Items.Enqueue(next);
            }
        }
    }
}