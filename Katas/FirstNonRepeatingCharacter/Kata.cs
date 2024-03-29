namespace Katas.FirstNonRepeatingCharacter;

using System.Linq;

/// <summary>
/// https://www.codewars.com/kata/52bc74d4ac05d0945d00054e/train/csharp
/// </summary>
public static class Kata
{
    public static string FirstNonRepeatingLetter(string text) =>
        text
            .AsEnumerable()
            .Distinct()
            .Select(character => CharInfo.Create(character, text))
            .Where(info => info.IsUnique())
            .Select(info => info.ToString())
            .FirstOrDefault(string.Empty);
}

internal readonly struct CharInfo
{
    private CharInfo(char character, int firstIndex, int lastIndex)
    {
        Character = character;
        FirstIndex = firstIndex;
        LastIndex = lastIndex;
    }

    private char Character { get; }
    private int FirstIndex { get; }
    private int LastIndex { get; }

    public bool IsUnique() => FirstIndex == LastIndex;

    public override string ToString() => Character.ToString();

    public static CharInfo Create(char character, string text)
    {
        var lowercasedCharacter = char.ToLower(character);
        var lowercasedString = text.ToLower();
        var firstIndex = lowercasedString.IndexOf(lowercasedCharacter);
        var lastIndex = lowercasedString.LastIndexOf(lowercasedCharacter);

        return new CharInfo(character, firstIndex, lastIndex);
    }
}
