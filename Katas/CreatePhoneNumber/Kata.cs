namespace Katas.CreatePhoneNumber
{
    /// <summary>
    /// https://www.codewars.com/kata/525f50e3b73515a6db000b83/train/csharp
    /// </summary>
    public class Kata
    {
        public static string CreatePhoneNumber(int[] numbers)
        {
            return
                $"({numbers[0]}{numbers[1]}{numbers[2]}) {numbers[3]}{numbers[4]}{numbers[5]}-{numbers[6]}{numbers[7]}{numbers[8]}{numbers[9]}";
        }
    }
}

// Again what learned:
// long.Parse(string.Concat(numbers)).ToString("(000) 000-0000");
// or:
// string.Format("({0}{1}{2}) {3}{4}{5}-{6}{7}{8}{9}",numbers.Select(x=>x.ToString()).ToArray());
