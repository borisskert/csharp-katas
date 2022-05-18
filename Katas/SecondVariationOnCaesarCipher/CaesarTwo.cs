namespace Katas.SecondVariationOnCaesarCipher
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// https://www.codewars.com/kata/55084d3898b323f0aa000546/train/csharp
    /// </summary>
    public class CaesarTwo
    {
        public static List<string> encodeStr(string plaintext, int shift)
        {
            return Cleartext.Of(plaintext)
                .EncryptBy(shift)
                .ToChunks()
                .ToList();
        }

        public static string decode(List<string> s)
        {
            return Chunks.From(s)
                .Join()
                .Decrypt()
                .Plaintext();
        }
    }

    internal class Cleartext
    {
        private readonly string _plaintext;

        private Cleartext(string plaintext)
        {
            _plaintext = plaintext;
        }

        public Ciphertext EncryptBy(int shift)
        {
            var prefix = Prefix.ForEncryption(_plaintext, shift);
            var payload = Caesars.WithShift(shift).Encrypt(_plaintext);

            return Ciphertext.From(prefix, payload);
        }

        public string Plaintext()
        {
            return _plaintext;
        }

        public static Cleartext Of(string s)
        {
            return new Cleartext(s);
        }
    }

    internal class Caesars
    {
        private static readonly int AlphabetSize = 26;
        private readonly int _shift;

        private Caesars(int shift)
        {
            _shift = shift;
        }

        public string Encrypt(string cleartext)
        {
            var encrypted = cleartext.Select(c => Move(c, _shift));
            return string.Join(string.Empty, encrypted);
        }

        public string Decrypt(string cleartext)
        {
            var encrypted = cleartext.Select(c => Move(c, _shift * -1));
            return string.Join(string.Empty, encrypted);
        }

        public static Caesars WithShift(int shift)
        {
            return new Caesars(shift);
        }

        public static char Move(char letter, int shift)
        {
            if (char.IsLower(letter))
            {
                return (char) ((letter - 'a' + shift + AlphabetSize) % AlphabetSize + 'a');
            }

            if (char.IsUpper(letter))
            {
                return (char) ((letter - 'A' + shift + AlphabetSize) % AlphabetSize + 'A');
            }

            return letter;
        }

        public static int ShiftOf(char cleartext, char ciphertext)
        {
            return (ciphertext - cleartext + AlphabetSize) % AlphabetSize;
        }
    }

    internal class Prefix
    {
        private readonly string _representation;

        private Prefix(string representation, int shift)
        {
            _representation = representation;
            Shift = shift;
        }

        public int Shift { get; }

        public override string ToString()
        {
            return _representation;
        }

        public static Prefix ForEncryption(string cleartext, int shift)
        {
            var first = char.ToLower(cleartext.First());
            var second = Caesars.Move(first, shift);

            return new Prefix(new string(new[] {first, second}), shift);
        }

        public static Prefix ForDecryption(string prefix)
        {
            var first = prefix.First();
            var last = prefix.Skip(1).First();
            var shift = Caesars.ShiftOf(first, last);

            return new Prefix(prefix, shift);
        }
    }

    internal class Ciphertext
    {
        private readonly Prefix _prefix;
        private readonly string _payload;

        private Ciphertext(Prefix prefix, string payload)
        {
            _prefix = prefix;
            _payload = payload;
        }

        public Chunks ToChunks()
        {
            string joined = _prefix + _payload;
            return Chunks.Of(joined);
        }

        public Cleartext Decrypt()
        {
            var cleartext = Caesars
                .WithShift(_prefix.Shift)
                .Decrypt(_payload);

            return Cleartext.Of(cleartext);
        }

        public static Ciphertext From(string ciphertext)
        {
            var prefix = Prefix.ForDecryption(ciphertext);
            var payload = string.Join("", ciphertext.Skip(2));

            return new Ciphertext(prefix, payload);
        }

        public static Ciphertext From(Prefix prefix, string payload)
        {
            return new Ciphertext(prefix, payload);
        }
    }

    internal class Chunks
    {
        private static readonly int DefaultChunkAmount = 5;

        private readonly IEnumerable<string> _chunks;

        private Chunks(IEnumerable<string> chunks)
        {
            _chunks = chunks;
        }

        public List<string> ToList()
        {
            return _chunks.ToList();
        }

        public Ciphertext Join()
        {
            var ciphertext = string.Join("", _chunks);
            return Ciphertext.From(ciphertext);
        }

        public static Chunks Of(string joined)
        {
            var chunkSize = (int) Math.Ceiling(joined.Length / (double) DefaultChunkAmount);
            var chunks = CreateChunks(chunkSize, joined);

            return new Chunks(chunks);
        }

        private static IEnumerable<string> CreateChunks(int chunkSize, string stream)
        {
            if (!stream.Any())
            {
                return new List<string>();
            }

            var taken = string.Join("", stream.Take(chunkSize));
            var skipped = string.Join("", stream.Skip(chunkSize));

            return ImmutableList<string>.Empty
                .Add(taken)
                .AddRange(CreateChunks(chunkSize, skipped));
        }

        public static Chunks From(IEnumerable<string> strings)
        {
            return new Chunks(strings.ToList());
        }
    }
}
