using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// https://www.codewars.com/kata/54dc6f5a224c26032800005c/train/csharp
/// </summary>
public class StockList
{
    public static string stockSummary(string[] listOfArt, string[] listOfCat)
    {
        if (listOfArt.Length < 1)
        {
            return string.Empty;
        }

        var stock = listOfArt.Select(StockItem.Read)
            .Aggregate(Stock.Empty(), (stock, item) => stock.Push(item));

        var formattedItems = listOfCat
            .Select(category => stock[category])
            .Select(item => item.FriendlyFormat());

        return string.Join(" - ", formattedItems);
    }
}

internal class StockItem
{
    private static readonly Regex Pattern = new("(?<category>[A-Z])[A-Z]* (?<quantity>[0-9]+)");

    public string Category { get; }
    private readonly int _quantity;

    private StockItem(string category, int quantity)
    {
        Category = category;
        _quantity = quantity;
    }

    public string FriendlyFormat()
    {
        return $"({Category} : {_quantity})";
    }

    public StockItem Merge(StockItem other)
    {
        return new StockItem(Category, _quantity + other._quantity);
    }

    public static StockItem Read(string input)
    {
        var match = Pattern.Match(input);

        if (match.Success)
        {
            string category = match.Groups["category"].Value;
            int quantity = int.Parse(match.Groups["quantity"].Value);

            return new StockItem(category, quantity);
        }

        throw new ArgumentException($"Cannot read from: {input}");
    }

    public static StockItem Empty(string category)
    {
        return new StockItem(category, 0);
    }
}

internal class Stock
{
    private readonly ImmutableDictionary<string, StockItem> _items;

    private Stock(ImmutableDictionary<string, StockItem> items)
    {
        _items = items;
    }

    public static Stock Empty()
    {
        return new Stock(ImmutableDictionary<string, StockItem>.Empty);
    }

    public Stock Push(StockItem item)
    {
        if (_items.ContainsKey(item.Category))
        {
            var found = _items[item.Category];
            return new Stock(_items.SetItem(item.Category, found.Merge(item)));
        }

        return new Stock(_items.Add(item.Category, item));
    }

    public StockItem this[string category] =>
        _items.TryGetValue(category, out var item)
            ? item
            : StockItem.Empty(category);
}
