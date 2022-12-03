using FluentAssertions;
using NUnit.Framework;

namespace AOC2022.Days;

public class DayTwo
{
    private readonly Dictionary<string, int> _points = new()
    {
        { "A", 1 },
        { "B", 2 },
        { "C", 3 },
        { "X", 1 },
        { "Y", 2 },
        { "Z", 3 },
    };

    private readonly Dictionary<int, (int, int)> _winnerLoser = new()
    {
        { 1, (2, 3) },
        { 2, (3, 1) },
        { 3, (1, 2) },
    };

    private readonly List<(int, int)> _rolls = new();

    [OneTimeSetUp]
    public void Setup()
    {
        foreach (var roll in File.ReadAllLines("Input/DayTwo.txt"))
        {
            _rolls.Add((_points[roll[..1]], _points[roll[2..3]]));
        }
    }

    [Test]
    public void Test1()
    {
        _rolls.ConvertAll(roll => GetScore(roll.Item1, roll.Item2)).Sum().Should().Be(14264);
    }

    [Test]
    public void Test2()
    {
        _rolls.ConvertAll(roll => GetScoreByMethod(roll.Item1, roll.Item2)).Sum().Should().Be(12382);
    }

    private int GetScore(int input, int response)
    {
        if (input == response) return response + 3;
        return _winnerLoser[response].Item2 == input ? response + 6 : response;
    }

    private int GetScoreByMethod(int input, int method)
    {
        return method switch
        {
            1 => GetScore(input, _winnerLoser[input].Item2),
            2 => GetScore(input, input),
            3 => GetScore(input, _winnerLoser[input].Item1),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }
}