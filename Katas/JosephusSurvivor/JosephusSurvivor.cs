namespace Katas.JosephusSurvivor
{
    using System.Linq;

    /**
     * https://www.codewars.com/kata/555624b601231dc7a400017a/train/csharp
     */
    public class JosephusSurvivor
    {
        public static int JosSurvivor(int n, int k)
        {
            return Enumerable.Range(1, n)
                .Aggregate(
                    1,
                    (x, i) => (x + k - 1) % i + 1
                );
        }
    }
}
