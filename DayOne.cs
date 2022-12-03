using FluentAssertions;
using NUnit.Framework;

namespace AOC2022;

public class DayOne
{
    private List<int> Elves { get; set; }

    [OneTimeSetUp]
    public void Setup()
    {
        Elves = new List<int>();
        var current = 0;
        foreach (var line in File.ReadAllLines("Input/DayOne.txt"))
        {
            if (line == "")
            {
                Elves.Add(current);
                current = 0;
            }
            else
            {
                current += Convert.ToInt32(line);
            }
        }
    }

    [Test]
    public void Test1()
    {
        Elves.Max().Should().Be(67622);
    }

    [Test]
    public void Test2()
    {
        Elves.OrderByDescending(elf => elf).Take(3).Sum().Should().Be(201491);
    }
}