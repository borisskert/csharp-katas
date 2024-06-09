namespace Katas.SettingPlacesForTheDead;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

/// <summary>
/// https://www.codewars.com/kata/6646c0c08b97085ca216d346/train/csharp
/// </summary>
public static class GreatHall
{
    public static string[] SetTable(string[] theDead)
    {
        return theDead.Aggregate(
            Seats.Empty(),
            (current, name) => current.TakeSeat(name)
        ).ToArray();
    }
}

internal interface ICulture
{
    bool IsBelonging(string name);

    IEnumerable<Seat> SeatOrder { get; }

    private static IEnumerable<ICulture> All { get; } = new List<ICulture>
            { new Earthenware(), new Waterfall(), new Fireplace(), new Windowsill() }
        .AsReadOnly();

    static ICulture FindCulture(string name)
    {
        return All.First(culture => culture.IsBelonging(name));
    }
}

internal class Earthenware : ICulture
{
    private const string Letters = "QUTHCRDMZ";

    public bool IsBelonging(string name)
    {
        return Letters.Contains(name[0]);
    }

    public IEnumerable<Seat> SeatOrder =>
        new List<Seat>
        {
            Seat.One, Seat.Twelve, Seat.Two, Seat.Eleven, Seat.Three, Seat.Ten, Seat.Four,
            Seat.Nine, Seat.Five, Seat.Eight, Seat.Six, Seat.Seven
        }.AsReadOnly();
}

internal class Waterfall : ICulture
{
    private const string Letters = "WEVOXING";

    public bool IsBelonging(string name)
    {
        return Letters.Contains(name[0]);
    }

    public IEnumerable<Seat> SeatOrder =>
        new List<Seat>
        {
            Seat.Four, Seat.Three, Seat.Five, Seat.Two, Seat.Six, Seat.One, Seat.Seven, Seat.Twelve, Seat.Eight,
            Seat.Eleven, Seat.Nine, Seat.Ten
        }.AsReadOnly();
}

internal class Fireplace : ICulture
{
    private const string Letters = "JFABKPLY";

    public bool IsBelonging(string name)
    {
        return Letters.Contains(name[0]);
    }

    public IEnumerable<Seat> SeatOrder =>
        new List<Seat>
        {
            Seat.Seven, Seat.Six, Seat.Eight, Seat.Five, Seat.Nine, Seat.Four, Seat.Ten, Seat.Three, Seat.Eleven,
            Seat.Two, Seat.Twelve, Seat.One
        }.AsReadOnly();
}

internal class Windowsill : ICulture
{
    public bool IsBelonging(string name)
    {
        return name.StartsWith('S');
    }

    public IEnumerable<Seat> SeatOrder =>
        new List<Seat>
        {
            Seat.Ten, Seat.Nine, Seat.Eleven, Seat.Eight, Seat.Twelve, Seat.Seven, Seat.One, Seat.Six, Seat.Two,
            Seat.Five, Seat.Three, Seat.Four
        }.AsReadOnly();
}

internal class Seats
{
    private readonly ISet<Seat> _availableSeats;
    private readonly ImmutableDictionary<Seat, string> _takenSeats;

    private Seats(ISet<Seat> availableSeats, ImmutableDictionary<Seat, string> takenSeats)
    {
        _availableSeats = availableSeats;
        _takenSeats = takenSeats;
    }

    public Seats TakeSeat(string name)
    {
        if (_availableSeats.Count == 0)
        {
            return this;
        }

        var culture = ICulture.FindCulture(name);

        var foundSeat = culture.SeatOrder
            .AsEnumerable()
            .First(seat => _availableSeats.Contains(seat));

        var availableSeats = _availableSeats.AsEnumerable()
            .Where(s => !Equals(s, foundSeat))
            .ToHashSet();

        return new Seats(availableSeats, _takenSeats.SetItem(foundSeat, name));
    }

    public string[] ToArray()
    {
        return Seat.All
            .Select(seat => _takenSeats.GetValueOrDefault(seat, "_____"))
            .ToArray();
    }

    public static Seats Empty()
    {
        return new Seats(
            new HashSet<Seat>(Seat.All),
            ImmutableDictionary<Seat, string>.Empty
        );
    }
}

internal class Seat
{
    public static readonly Seat One = new(1);
    public static readonly Seat Two = new(2);
    public static readonly Seat Three = new(3);
    public static readonly Seat Four = new(4);
    public static readonly Seat Five = new(5);
    public static readonly Seat Six = new(6);
    public static readonly Seat Seven = new(7);
    public static readonly Seat Eight = new(8);
    public static readonly Seat Nine = new(9);
    public static readonly Seat Ten = new(10);
    public static readonly Seat Eleven = new(11);
    public static readonly Seat Twelve = new(12);

    private readonly ushort _id;

    private Seat(ushort id)
    {
        _id = id;
    }

    public override bool Equals(object obj)
    {
        return obj is Seat seat && _id == seat._id;
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    public static IEnumerable<Seat> All { get; } = new List<Seat>
            { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Eleven, Twelve }
        .AsReadOnly();
}
