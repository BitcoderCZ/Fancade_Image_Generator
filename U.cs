using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancadeImageGenerator
{
    internal static class U
    {
        /// <summary>
        /// Shows press any key to exit, then waits for key press
        /// </summary>
        /// <param name="message"></param>
        public static void PAKE(string message = "")
        {
            Console.CursorVisible = true;

            if (string.IsNullOrWhiteSpace(message))
                Console.WriteLine("Press any key to exit...");
            else
                Console.WriteLine(message + ", press any key to exit...");

            Console.ReadKey(true);
        }

        public static int ClosestColor(Argb32[] colors, Argb32 color)
        {
            int minDiff = int.MaxValue;
            int index = -1;

            for (int i = 0; i < colors.Length; i++)
            {
                int diff = ColorDiff(color, colors[i]);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    index = i;
                }
            }

            return index;
        }

        public static int ColorDiff(Argb32 c1, Argb32 c2)
            => (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                 + (c1.G - c2.G) * (c1.G - c2.G)
                                 + (c1.B - c2.B) * (c1.B - c2.B));

        public static string RandomGameId()
        {
            Random rng = new Random(DateTime.UtcNow.Second * DateTime.UtcNow.Millisecond);

            char[] allowedChars = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            const int length = 16;

            char[] id = new char[length];
            for (int i = 0; i < length; i++)
                id[i] = allowedChars[rng.Next(0, allowedChars.Length)];

            return new string(id);
        }
    }
}
