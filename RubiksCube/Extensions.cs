using System.Text.Json;

namespace RubiksCube
{
    public static class Extensions
    {
        // T

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                JsonSerializer.Serialize(ms, obj);
                ms.Position = 0;

                return JsonSerializer.Deserialize<T>(ms);
            }
        }

        // INTEGERS

        /// <summary>
        /// Handles negative integers in a more intuitive way, ie: m.Mod(-1) is equivalent to  m % (m-1)
        /// </summary>
        /// <param name="m">dividend</param>
        /// <param name="n">remainer</param>
        /// <returns></returns>
        public static int Mod(this int m, int n)
        {
            return ( m % n + n) % n;
        }
    }
}
