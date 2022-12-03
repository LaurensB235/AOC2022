using FluentAssertions;
using NUnit.Framework;

namespace AOC2022.Days;

public class DayThree
{
    private string _values = "abcdefghijklmnopqrstuvwxyz";
    private readonly List<(string, string, string)> _bags = new();

    [OneTimeSetUp]
    public void Setup()
    {
        _values += _values.ToUpper();
        foreach (var roll in File.ReadAllLines("Input/DayThree.txt"))
        {
            var mid = roll.Length / 2;
            _bags.Add((roll[..mid], roll[mid..], roll));
        }
    }

    [Test]
    public void Test1()
    {
        _bags.ConvertAll(bag => GetScore(bag.Item1.ToList().Find(item => bag.Item2.Contains(item))))
            .Sum()
            .Should()
            .Be(7763);
    }

    [Test]
    public void Test2()
    {
        var groups = _bags.Select((x, idx) => new { x, idx })
            .GroupBy(x => x.idx / 3)
            .Select(g => g.Select(a => a.x))
            .Select(item => item.ToList())
            .Select(group => GetScore(group[0].Item3.ToList().Find(item => group[1].Item3.Contains(item) && group[2].Item3.Contains(item))))
            .Sum()
            .Should()
            .Be(2569);
    }

    private int GetScore(char value)
    {
        return _values.IndexOf(value) + 1;
    }
}