namespace Katas.ValidParentheses
{
    using System.Collections.Generic;
    using System.Linq;

    /**
     * https://www.codewars.com/kata/52774a314c2333f0a7000688/train/csharp
     * I solved this kata using a recursive algorithm without any (for|foreach|while) loop
     */
    public class Parentheses
    {
        public static bool ValidParentheses(string input)
        {
            return From(input).IsValid();
        }

        private readonly IEnumerable<char> _chars;
        private readonly int _balance;

        private Parentheses(IEnumerable<char> chars, int balance)
        {
            _chars = chars;
            _balance = balance;
        }

        private bool IsValid()
        {
            if (!_chars.Any())
            {
                return _balance == 0;
            }

            if (_balance < 0)
            {
                return false;
            }

            return _chars.First() switch
            {
                '(' => Opening().IsValid(),
                ')' => Closing().IsValid(),
                _ => AnyOther().IsValid()
            };
        }

        private Parentheses Opening()
        {
            return new Parentheses(_chars.Skip(1), _balance + 1);
        }

        private Parentheses Closing()
        {
            return new Parentheses(_chars.Skip(1), _balance - 1);
        }

        private Parentheses AnyOther()
        {
            return new Parentheses(_chars.Skip(1), _balance);
        }

        private static Parentheses From(string input)
        {
            return new Parentheses(input.ToCharArray(), 0);
        }
    }
}
