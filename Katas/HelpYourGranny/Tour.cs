namespace Katas.HelpYourGranny
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// https://www.codewars.com/kata/5536a85b6ed4ee5a78000035/train/csharp
    /// Implementing this solution by following these rules:
    ///   - No (for|foreach|while) loops
    ///   - Write domain classes to make the code "speaking"
    ///   - Avoid if-else statements
    ///   - Make all classes immutable
    /// </summary>
    public class Tour
    {
        /// <summary>
        /// Help your granny and give her approximately the distance to travel
        /// </summary>
        /// <param name="arrFriends">array of friends, for example: <code>{"A1", "A2", "A3", "A4", "A5"}</code></param>
        /// <param name="fTowns">Friends inhabit towns,
        /// for example: <code>[["A1", "X1"], ["A2", "X2"], ["A3", "X3"], ["A4", "X4"]]</code> which means A1 is in town X1 and so on
        /// </param>
        /// <param name="townDistancesTable">table of town distances, for example <code>{{"X1", 100.0}, {"X2", 200.0}, {"X3", 250.0}, {"X4", 300.0}}</code></param>
        /// <returns>granny's approximately distance to travel</returns>
        public static int tour(string[] arrFriends, string[][] fTowns, Hashtable townDistancesTable)
        {
            var friends = Friends.Of(arrFriends);
            var friendTowns = FriendTowns.Of(fTowns);
            var towns = friendTowns.FilterBy(friends);
            var townDistances = TownDistances.Of(townDistancesTable);
            var distances = townDistances.ExtractBy(towns);
            var pairs = distances.ToPairs();

            var distance = pairs.AsEnumerable()
                .Select(pair => Pythagoras.OppositeLeg(pair.First, pair.Second))
                .Sum();

            return (int) (distances.First() + distance + distances.Last());
        }
    }

    internal static class Pythagoras
    {
        /// <summary>
        /// Calculates the length of the opposite leg (b) by Pythagorean theorem
        /// </summary>
        /// <param name="a">the adjacent leg [MATH.]</param>
        /// <param name="c">the hypotenuse [MATH.]</param>
        /// <returns>opposite leg [MATH.]</returns>
        public static double OppositeLeg(double a, double c)
        {
            return Math.Sqrt(c * c - a * a);
        }
    }

    internal class Pairs
    {
        public static readonly Pairs Empty = new Pairs(new List<Pair>());

        private readonly IEnumerable<Pair> _pairs;

        private Pairs(IEnumerable<Pair> pairs)
        {
            _pairs = pairs;
        }

        [Pure]
        public Pairs Append(Pair pair)
        {
            return new Pairs(_pairs.Append(pair));
        }

        [Pure]
        public IEnumerable<Pair> AsEnumerable()
        {
            return _pairs;
        }
    }

    internal class Pair
    {
        public double First { get; private set; }
        public double Second { get; private set; }

        public static Pair Of(double first, double second)
        {
            var pair = new Pair();
            pair.First = first;
            pair.Second = second;

            return pair;
        }
    }

    internal class Distances
    {
        private readonly IEnumerable<double> _distances;

        private Distances(IEnumerable<double> distances)
        {
            _distances = distances;
        }

        [Pure]
        public Pairs ToPairs()
        {
            return ToPairs(_distances.ToList(), Pairs.Empty);
        }

        [Pure]
        public double First()
        {
            return _distances.First();
        }

        [Pure]
        public double Last()
        {
            return _distances.Last();
        }

        private static Pairs ToPairs(IList<double> distances, Pairs collected)
        {
            if (distances.Count() < 2)
            {
                return collected;
            }

            var first = distances.First();
            var remainingDistances = distances.Skip(1).ToList();
            var second = remainingDistances.First();

            var pair = Pair.Of(first, second);

            return ToPairs(remainingDistances, collected.Append(pair));
        }

        public static Distances Of(IEnumerable<double> distances)
        {
            return new Distances(distances);
        }
    }

    internal class TownDistances
    {
        private readonly IDictionary<string, double> _townDistances;

        private TownDistances(IDictionary<string, double> townDistances)
        {
            _townDistances = townDistances;
        }

        [Pure]
        public Distances ExtractBy(Towns towns)
        {
            var distances = towns.AsEnumerable().Select(town => _townDistances[town]);
            return Distances.Of(distances);
        }

        public static TownDistances Of(Hashtable table)
        {
            var townDistances = table
                .Cast<DictionaryEntry>()
                .ToDictionary(
                    entry => (string) entry.Key,
                    entry => (double) entry.Value
                );

            return new TownDistances(townDistances);
        }
    }

    internal class Towns
    {
        private readonly IEnumerable<string> _towns;

        private Towns(IEnumerable<string> towns)
        {
            _towns = towns;
        }

        [Pure]
        public IEnumerable<string> AsEnumerable()
        {
            return _towns;
        }

        public static Towns Of(IEnumerable<string> towns)
        {
            return new Towns(towns);
        }
    }

    internal class FriendTowns
    {
        private readonly IDictionary<string, string> _friendTowns;

        private FriendTowns(IDictionary<string, string> friendTowns)
        {
            _friendTowns = friendTowns;
        }

        [Pure]
        public Towns FilterBy(Friends friends)
        {
            var towns = _friendTowns
                .Where(pair => friends.Contains(pair.Key))
                .Select(pair => pair.Value).ToList();

            return Towns.Of(towns);
        }

        public static FriendTowns Of(IEnumerable<string[]> fTowns)
        {
            var friendTowns = fTowns.ToDictionary(
                strings => strings[0],
                strings => strings[1]
            );

            return new FriendTowns(friendTowns);
        }
    }

    internal class Friends
    {
        private readonly IEnumerable<string> _friends;

        private Friends(IEnumerable<string> friends)
        {
            _friends = friends;
        }

        [Pure]
        public bool Contains(string friend)
        {
            return _friends.Contains(friend);
        }

        public static Friends Of(IEnumerable<string> friends)
        {
            return new Friends(friends);
        }
    }
}
